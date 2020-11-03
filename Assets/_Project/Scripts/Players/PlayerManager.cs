
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : Singleton<PlayerManager> {
    // singleton
    protected PlayerManager () { }
    //public static new PlayerManager Instance;

    //listeners
    void OnEnable ()
    {
        EventManager.StartListening ("AddAllPlayers", AddAllPlayers);
        EventManager.StartListening ("RemoveAllPlayers", RemoveAllPlayers);
        EventManager.StartListening ("CheckUpdatePlayers", CheckUpdatePlayers);
    }
    void OnDisable ()
    {
        EventManager.StopListening ("AddAllPlayers", AddAllPlayers);
        EventManager.StopListening ("RemoveAllPlayers", RemoveAllPlayers);
        EventManager.StopListening ("CheckUpdatePlayers", CheckUpdatePlayers);
    }


    [Space (10)]
    [Header ("DETAILS")]

    // number of players
    public int playerCount;
    public int playerToRemoveCount;
    // min/max allowed at one time
    public int minPlayersAllowed;
    public int maxPlayersAllowed;


    [Space (10)]
    [Header ("OBJECTS")]

    // bounds, prefab, dict for instantiating players
    public BoxCollider worldContainerCollider;
    public GameObject playerPrefab;
    public Dictionary<string, GameObject> playerDict;
    public Dictionary<string, GameObject> playersToRemoveDict;
    public CameraManager cameraManager;

    // temp sprites for assigning avatars
    public Sprite [] avatars;



    [Space (10)]
    [Header ("CURRENT PLAYER")]

    // player currently showing an event
    public GameObject currentPlayerObj;
    public Player currentPlayerScript;
    public string currentEventType;





    [Space (10)]
    [Header ("ANIMATIONS")]

    // animations to play
    public GameObject attackSpriteAnim;
    public GameObject battleSpriteAnimFront;
    public GameObject battleSpriteAnimBack;
    public GameObject rippleAnim;
    public GameObject triangleTrailsAnim;
    public GameObject attackAnim;
    public GameObject badgeAnim;
    public GameObject consumableAnim;
    public GameObject disguiseAnim;
    public GameObject trackerAnim;
    public GameObject leaderboardAnim;
    public GameObject selectionParticle;



    private void Awake ()
    {
        //Instance = this;
        playerDict = new Dictionary<string, GameObject> ();
        playersToRemoveDict = new Dictionary<string, GameObject> ();
    }













    /**
     *  Add all players to screen and dict - called at start or after data reset
     *  - called when Timeline.status == TimelineStatus.newDataReceived
     */
    void AddAllPlayers ()
    {
        Debug.Log ("PlayerManager.AddAllPlayers()");

        // loop through the buffer and add players to scene and dict
        foreach (var feed in Timeline.Instance.buffer) {
            // max hasn't been reached
            if (playerDict.Count > maxPlayersAllowed) break;
            // check if player exists and add if not 
            CreateNewPlayer (feed.username, feed.avatarPath);
        }
        foreach (var feed in Timeline.Instance.history) {
            // max hasn't been reached
            if (playerDict.Count > maxPlayersAllowed) break;
            // check if player exists and add if not 
            CreateNewPlayer (feed.username, feed.avatarPath);
        }

        UpdateCounts ();

    }


    /**
     *  Remove all players from screen and dict
     *  - called from Timeline.OnStartBtnClick() to stop timeline / reset data
     */
    void RemoveAllPlayers ()
    {
        Debug.Log ("PlayerManager.RemoveAllPlayers()");

        // for each in playerDict, remove from scene
        foreach (KeyValuePair<string, GameObject> kvp in playerDict) {
            Destroy (kvp.Value);
        }

        // clear the dictionary 
        playerDict.Clear ();
        // update the count
        UpdateCounts ();
    }


    /**
     *  - called when Timeline.status == TimelineStatus.newDataReceived
     */
    void CheckUpdatePlayers ()
    {
        Debug.Log ("PlayerManager.CheckUpdatePlayers()");

        // make a copy of the current player dict
        playersToRemoveDict = new Dictionary<string, GameObject> (playerDict);

        GameObject player;

        // loop through the buffer
        foreach (var feed in Timeline.Instance.buffer) {
            // if player still in playerDict
            playerDict.TryGetValue (feed.username, out player);
            if (player != null) {
                // remove from playersToRemoveDict
                playersToRemoveDict.Remove (feed.username);
            } else {
                // max hasn't been reached
                if (playerDict.Count > maxPlayersAllowed) break;
                // check if player exists and add
                CreateNewPlayer (feed.username, feed.avatarPath);
            }
        }
        foreach (var feed in Timeline.Instance.history) {
            // if player still in playerDict
            playerDict.TryGetValue (feed.username, out player);
            if (player != null) {
                // remove from playersToRemoveDict
                playersToRemoveDict.Remove (feed.username);
            } else {
                // max hasn't been reached
                if (playerDict.Count > maxPlayersAllowed) break;
                // check if player exists and add
                CreateNewPlayer (feed.username, feed.avatarPath);
            }
        }




        // if players still left in playersToRemoveDict
        if (playersToRemoveDict.Count > 0) {
            // remove those players from scene and clear dict
            // for each in playerDict, remove from scene
            foreach (KeyValuePair<string, GameObject> kvp in playersToRemoveDict) {
                Destroy (kvp.Value);
            }
            // clear the playersToRemoveDict dictionary 
            playersToRemoveDict.Clear ();
        }



        // update counts
        UpdateCounts ();
    }




    void UpdateCounts ()
    {
        // update player count
        playerCount = playerDict.Count;
        playerToRemoveCount = playersToRemoveDict.Count;
        // trigger data updated event
        EventManager.TriggerEvent ("PlayersUpdated");
    }









    /**
     *  Create a new player
     */
    public bool CreateNewPlayer (string username, string avatarPath)
    {
        // make sure the player doesn't already exist
        if (playerDict.ContainsKey (username)) return false;

        // get a position that doesn't contain any other colliders
        Vector3 spawnPosition = GetClearSpawnPosition ();

        // get the spawn rotation
        Quaternion spawnRotation = new Quaternion ();
        // random rotation - on local x only
        //spawnRotation.eulerAngles = new Vector3 (Random.Range (0.0f, 360.0f), 90f, 0f);
        // no random rotation
        spawnRotation.eulerAngles = new Vector3 (0f, 0f, 0f);

        // if clear spawn position
        if (spawnPosition != Vector3.zero) {
            // instantiate prefab @ spawn position
            GameObject obj = (GameObject)Instantiate (playerPrefab, spawnPosition, spawnRotation);
            // call Init() on Player
            obj.GetComponent<Player> ().Init (username, avatarPath);
            // set name in Unity Editor
            obj.name = username;
            // parent under PlayerManger
            obj.transform.parent = gameObject.transform;
            // finaly, add to dict
            playerDict.Add (username, obj);
            // sets a reference to the cameraManager
            obj.GetComponent<Player> ().cameraManager = cameraManager;

            // Allow the player to be selected by the camera
            cameraManager.AddPlayer (username);
        }
        return true;
    }


    /**
     *  Play a player event 
     *  - The logic that determines effects. 
     *  - For example, whether a GO with sprite animation or particle effect animation is attached, and what timeline animation (when tally twirls, etc.) is played. 
     */
    public void PlayEvent (FeedData feed)
    {
        //Debug.Log (DebugManager.GetSymbol ("smilingFace") + " PlayerManager.PlayEvent() [1] feed = " + feed.username.ToString ());


        // PLAYER OBJECT REFERENCES

        // get the player from the dict
        playerDict.TryGetValue (feed.username, out currentPlayerObj);
        if (!currentPlayerObj) return;

        // reference to script (contains all the other references we need)
        currentPlayerScript = currentPlayerObj.GetComponent<Player> ();

        // show event in public var
        currentEventType = feed.eventType;





        // EFFECTS

        // BATTLES ARE MORE COMPLEX
        if (feed.eventType == "monster") {
            StartCoroutine (PlayBattleEffects (feed));
        } else {




            // STREAM (CLICK or LIKE)
            if (feed.eventType == "stream") {
                AttachDetachAnimation (rippleAnim, false, 1f, 3.5f);
                // play the timeline animation
                currentPlayerScript.animControllerScript.animName = "Pop_Shake_md";

                // check to see if there are monsters following the player
                if (feed.monsters != "") {
                    Debug.Log ("PlayerManager.PlayEvent() monsters = " + feed.monsters);
                }
            }

            // ATTACK 
            else if (feed.eventType == "attack") {
                AttachDetachAnimation (attackAnim, false, 1f, 3f);
                // play the timeline animation
                currentPlayerScript.animControllerScript.animName = "Swirl_r_sm";
            }

            // BADGE 
            else if (feed.eventType == "badge") {
                AttachDetachAnimation (badgeAnim, false, 1f, 3.5f);
                // play the timeline animation
                currentPlayerScript.animControllerScript.animName = "Swirl_r_sm";
            }

            // CONSUMABLE 
            else if (feed.eventType == "consumable") {
                AttachDetachAnimation (consumableAnim, false, 1f, 2.5f);
                // play the timeline animation
                currentPlayerScript.animControllerScript.animName = "Pop_sm";
            }

            // DISGUISE 
            else if (feed.eventType == "disguise") {
                AttachDetachAnimation (disguiseAnim, false, 1f, 4f);
                // play the timeline animation
                currentPlayerScript.animControllerScript.animName = "Rotate_Pop_sm";
            }

            // TRACKER 
            else if (feed.eventType == "tracker") {
                AttachDetachAnimation (trackerAnim, false, 1f, 5f);
                // play the timeline animation
                currentPlayerScript.animControllerScript.animName = "Rotate_md";
            }

            // LEADERBOARD - not currently storing / sending with API
            else if (feed.eventType == "leaderboard") {
                AttachDetachAnimation (leaderboardAnim, false, 1f, 3f);
                // play the timeline animation
                currentPlayerScript.animControllerScript.animName = "Pop_Shake_sm";
            }


            // play matching sound
            AudioManager.Instance.Play (feed.eventType);

        }

        // test
        //StartCoroutine (PlayBattle (feed));


    }





    /**
     *  Return random Vector3 position that doesn't already have a GameObject in it
     */
    Vector3 GetClearSpawnPosition ()
    {
        Vector3 spawnPosition = Vector3.zero;
        int safety = 0;
        bool positionClear = false;
        int layerMask = (1 << 8); // only Layer 8 "Players"
        //Debug.Log ("PlayerManager.GetClearSpawnPosition() bounds = " + worldContainerCollider.bounds.ToString ());
        while (positionClear == false) {
            // get random position
            Vector3 spawnPositionRaw = RandomPointInBounds (worldContainerCollider.bounds);
            // store for test
            spawnPosition = new Vector3 (spawnPositionRaw.x, spawnPositionRaw.y, spawnPositionRaw.z);
            // get all the overlaping colliders
            Collider [] hitColliders = Physics.OverlapSphere (spawnPosition, 0.75f, layerMask);
            // if collider isn't touching another player collider then set true to stop loop
            if (hitColliders.Length <= 0) positionClear = true;
            // safety
            if (++safety > 10) {
                Debug.Log ("PlayerManager.GetClearSpawnPosition() - Safety first!");
                return spawnPosition;
            }
        }
        //Debug.Log ("PlayerManager.GetClearSpawnPosition() spawnPosition = " + spawnPosition.ToString ());
        return spawnPosition;
    }

    /**
     *  Return random Vector3 position inside bounds
     */
    public static Vector3 RandomPointInBounds (Bounds bounds)
    {
        return new Vector3 (
            Random.Range (bounds.min.x, bounds.max.x),
            Random.Range (bounds.min.y, bounds.max.y),
            0 //Random.Range (bounds.min.z, bounds.max.z)
        );
    }




    /// <summary>Attach and detach a game object with an animation</summary>
    /// <returns>null</returns>
    /// <param name="prefab">Prefab to instantiate</param>
    /// <param name="randomPosition">Whether or not position will be randomly offset</param>
    /// <param name="scaleMultiplier">Scale multiplier</param>
    /// <param name="destroyDelay">Destroy delay</param>
    void AttachDetachAnimation (GameObject prefab, bool randomPosition, float scaleMultiplier, float destroyDelay = -1f)
    {
        //Debug.Log ("AttachDetachAnimation() prefab.name = " + prefab.name);


        // ATTACH THE GAME OBJECT WITH ANIMATION

        // instantiate prefab, parent under the Player obj transform, position is local space
        GameObject obj = (GameObject)Instantiate (prefab, currentPlayerScript.effects.transform, false);

        // set slightly random position 
        if (randomPosition) obj.transform.localPosition = new Vector3 (Random.Range (-2, 2), Random.Range (-2, 2), 0);
        // or a default position
        else obj.transform.localPosition = Vector3.zero;

        // set scale
        obj.transform.localScale = Vector3.one * scaleMultiplier;

        // set state - not using this yet, but may need it
        currentPlayerScript.effectIsPlaying = true;


        // DETACH

        // should the animation destroy itself?
        if (destroyDelay < -0.1f) {
            // let the animation component destroy the gameobject after the last frame
            obj.GetComponent<FrameAnimation> ().playOnceAndDestroy = true;
            StartCoroutine (ResetEffectPlayingState (1f));
        }
        // should this function destroy the gameobject with the animation after n seconds?
        else if (destroyDelay > 0.1f) {
            // destroy after n seconds
            Destroy (obj, destroyDelay);
            StartCoroutine (ResetEffectPlayingState (destroyDelay));
        }


    }


    IEnumerator ResetEffectPlayingState (float wait)
    {
        // after a moment
        yield return new WaitForSeconds (wait);
        // reset state
        currentPlayerScript.effectIsPlaying = false;
    }



    IEnumerator PlayBattleEffects (FeedData feed)
    {
        //Debug.Log ("PlayBattle() feed = " + feed.ToString ());


        // start battle

        AttachDetachAnimation (battleSpriteAnimFront, false, 2.5f, 2f);
        AttachDetachAnimation (attackSpriteAnim, true, 2.5f, -1);
        AttachDetachAnimation (battleSpriteAnimBack, false, 2.5f, 2.1f);

        yield return new WaitForSeconds (1f);

        // play another attack

        AttachDetachAnimation (attackSpriteAnim, true, 2.5f, -1);

        yield return new WaitForSeconds (1f);

        // remove battle


        AudioManager.Instance.Play ("battle-won");
    }




}
