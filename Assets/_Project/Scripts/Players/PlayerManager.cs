
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
     *  Play an event for a player
     */
    public void PlayEvent (FeedData feed)
    {
        //Debug.Log (DebugManager.GetSymbol ("smilingFace") + " PlayerManager.PlayEvent() [1] feed = " + feed.username.ToString ());


        // GET PLAYER OBJ REFERENCE

        // get the player from the dict
        playerDict.TryGetValue (feed.username, out currentPlayerObj);
        if (!currentPlayerObj) return;

        // reference to script (contains all the other references we need)
        currentPlayerScript = currentPlayerObj.GetComponent<Player> ();


        // MARKED FOR DELETION - ALL OBJECTS CAN NOW BE ACCESSED THROUGH currentPlayerScript
        // get the animation controller - fairly fast since this is on next child
        //currentPlayerScript.animControllerScript = currentPlayerObj.GetComponentInChildren<AnimController> ();


        // EFFECTS
        // AttachDetachAnimation prefab, randomPosition, scale, destroyDelay, playOnceAndDestroy


        // BATTLES ARE MORE COMPLEXT
        if (feed.eventType == "monster") {
            StartCoroutine (PlayBattle (feed));
        } else {


            // ATTACK 
            if (feed.eventType == "attack") {
                AttachDetachAnimation (attackSpriteAnim, true, 2.3f, -1, true);
            }
            // BADGE 
            else if (feed.eventType == "badge") {
                // PLACEHOLDER
                AttachDetachAnimation (triangleTrailsAnim, false, 1f, 2.5f, false);
            }
            // CONSUMABLE 
            else if (feed.eventType == "consumable") {
                // PLACEHOLDER
                AttachDetachAnimation (triangleTrailsAnim, false, 1f, 2.5f, false);
            }
            // DISGUISE 
            else if (feed.eventType == "disguise") {
                // PLACEHOLDER
                AttachDetachAnimation (triangleTrailsAnim, false, 1f, 2.5f, false);
            }
            // STREAM (CLICK or LIKE)
            else if (feed.eventType == "stream") {
                AttachDetachAnimation (rippleAnim, false, 1f, 2.5f, false);
            }

            // play matching sound
            AudioManager.Instance.Play (feed.eventType);

        }

        // test
        //StartCoroutine (PlayBattle (feed));

        // play the timeline animation (loops, swirls, pops, etc.)
        currentPlayerScript.animControllerScript.animEvent = feed.eventType;
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





    /**
     *  Attach and detach a game object with an animation
     */



    void AttachDetachAnimation (GameObject prefab, bool randomPosition, float scale, float destroyDelay = -1, bool playOnceAndDestroy = true)
    {
        Debug.Log ("AttachDetachAnimation() prefab.name = " + prefab.name);

        // ATTACH

        // instantiate prefab 
        GameObject obj = (GameObject)Instantiate (prefab);

        obj.SetActive (false);

        // parent under the player obj
        obj.transform.parent = currentPlayerScript.effects.transform;

        // set slightly random position 
        if (randomPosition)
            obj.transform.localPosition = new Vector3 (Random.Range (-2, 2), Random.Range (-2, 2), 0);
        else
            // default position
            obj.transform.localPosition = Vector3.zero;

        // set scale
        obj.transform.localScale = Vector3.one * scale;

        obj.SetActive (true);

        // set state
        currentPlayerScript.effectIsPlaying = true;


        // DETACH

        // destroy after one play?
        if (playOnceAndDestroy)
            // let the animation component destroy the gameobject
            obj.GetComponent<FrameAnimation> ().playOnceAndDestroy = true;
        else if (destroyDelay > 0) {
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



    IEnumerator PlayBattle (FeedData feed)
    {
        Debug.Log ("PlayBattle() feed = " + feed.ToString ());


        // start battle

        AttachDetachAnimation (battleSpriteAnimFront, false, 2.3f, 2f, false);
        AttachDetachAnimation (attackSpriteAnim, true, 2.3f, -1, true);
        AttachDetachAnimation (battleSpriteAnimBack, false, 2.3f, 2.25f, false);

        yield return new WaitForSeconds (1f);

        // play another attack

        AttachDetachAnimation (attackSpriteAnim, true, 2.3f, -1, true);

        yield return new WaitForSeconds (1f);

        // remove battle


        //AttachDetachAnimation (battleSpriteAnimBack, false, 2.3f, 2.25f, false);

        AudioManager.Instance.Play ("battle-won");
    }




}
