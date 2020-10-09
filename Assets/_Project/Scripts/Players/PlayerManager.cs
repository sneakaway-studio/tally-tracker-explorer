
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
        EventManager.StartListening ("DataDownloaded", ResetPlayers);
    }
    void OnDisable ()
    {
        EventManager.StopListening ("DataDownloaded", ResetPlayers);
    }

    [Space (10)]
    [Header ("Object References")]

    // bounds, prefab, dict for instantiating players
    public Collider worldContainerCollider;
    public GameObject playerPrefab;
    public Dictionary<string, GameObject> playerDict;

    // temp sprites for assigning avatars
    public Sprite [] avatars;



    [Space (10)]
    [Header ("Current Player Event")]

    // player currently showing an event
    public GameObject currentPlayerObj;
    public Player currentPlayerScript;

    // number of players
    public int playerCount;



    [Space (10)]
    [Header ("Animations")]

    // animations to play
    public GameObject attackSpriteAnim;
    public GameObject battleSpriteAnimFront;
    public GameObject battleSpriteAnimBack;
    public GameObject rippleAnim;
    public GameObject triangleTrailsAnim;



    private void Awake ()
    {
        //Instance = this;
        playerDict = new Dictionary<string, GameObject> ();
    }


    /**
     *  Remove all players from stage, reset dict
     */
    public void ResetPlayers ()
    {
        // clear the dictionary 
        playerDict.Clear ();

        // loop through the feed and add players
        foreach (var feed in DataManager.feeds) {
            CreateNewPlayer (feed.username, feed.avatarPath);
        }

        // update player count
        playerCount = playerDict.Count;


        // trigger data updated event
        EventManager.TriggerEvent ("PlayersUpdated");
    }

    /**
     *  Create a new player
     */
    public void CreateNewPlayer (string username, string avatarPath)
    {
        // make sure the player doesn't already exist
        if (playerDict.ContainsKey (username)) return;

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
        }
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


        // EFFECTS

        // BATTLES ARE MORE COMPLEX
        if (feed.eventType == "monster") {
            StartCoroutine (PlayBattleEffects (feed));
        } else {




            // STREAM (CLICK or LIKE)
            if (feed.eventType == "stream") {
                AttachDetachAnimation (rippleAnim, false, 1f, 2.5f);
                // play the timeline animation
                currentPlayerScript.animControllerScript.animName = "Pop_Shake_md";
            }

            // ATTACK 
            else if (feed.eventType == "attack") {
                AttachDetachAnimation (attackSpriteAnim, true, 2.3f, -1);
                // play the timeline animation
                currentPlayerScript.animControllerScript.animName = "Swirl_r_sm";
            }

            // BADGE 
            else if (feed.eventType == "badge") {
                // PLACEHOLDER
                AttachDetachAnimation (triangleTrailsAnim, false, 1f, 2.5f);
                // play the timeline animation
                currentPlayerScript.animControllerScript.animName = "Swirl_r_md";
            }

            // CONSUMABLE 
            else if (feed.eventType == "consumable") {
                // PLACEHOLDER
                AttachDetachAnimation (triangleTrailsAnim, false, 1f, 2.5f);
                // play the timeline animation
                currentPlayerScript.animControllerScript.animName = "Pop_sm";
            }

            // DISGUISE 
            else if (feed.eventType == "disguise") {
                // PLACEHOLDER
                AttachDetachAnimation (triangleTrailsAnim, false, 1f, 2.5f);
                // play the timeline animation
                currentPlayerScript.animControllerScript.animName = "Rotate_Pop_sm";
            }

            // TRACKER 
            else if (feed.eventType == "tracker") {
                // PLACEHOLDER
                AttachDetachAnimation (triangleTrailsAnim, false, 1f, 2.5f);
                // play the timeline animation
                currentPlayerScript.animControllerScript.animName = "Rotate_md";
            }

            // LEADERBOARD - not currently storing / sending with API
            else if (feed.eventType == "leaderboard") {
                // PLACEHOLDER
                AttachDetachAnimation (triangleTrailsAnim, false, 1f, 2.5f);
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
        Vector3 spawnPosition = new Vector3 ();
        float startTime = Time.realtimeSinceStartup;
        bool positionClear = false;
        int layerMask = (1 << 8); // only Layer 8 "Players"
        while (positionClear == false) {
            // get random position
            Vector3 spawnPositionRaw = RandomPointInBounds (worldContainerCollider.bounds);
            // store for test
            spawnPosition = new Vector3 (spawnPositionRaw.x, spawnPositionRaw.y, spawnPositionRaw.z);
            // get all the overlaping colliders
            Collider [] hitColliders = Physics.OverlapSphere (spawnPosition, 0.75f, layerMask);
            // if collider isn't touching another player collider then set true to stop loop
            if (hitColliders.Length <= 0) positionClear = true;
            // else continue until time has run out
            if (Time.realtimeSinceStartup - startTime > 0.5f) {
                Debug.Log ("Time out placing GameObject!");
                return Vector3.zero;
            }
        }
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
        Debug.Log ("AttachDetachAnimation() prefab.name = " + prefab.name);


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
        Debug.Log ("PlayBattle() feed = " + feed.ToString ());


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
