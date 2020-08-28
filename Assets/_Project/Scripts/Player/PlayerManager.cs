using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : Singleton<PlayerManager>
{
    // singleton
    protected PlayerManager() { }
    public static new PlayerManager Instance;

    // listeners 
    //void OnEnable()
    //{
    //    EventManager.StartListening("DataUpdated", ResetPlayers);
    //}
    //void OnDisable()
    //{
    //    EventManager.StopListening("DataUpdated", ResetPlayers);
    //}

    public Collider worldContainerCollider;
    public GameObject playerPrefab;
    public Dictionary<string, GameObject> playerDictionary;

    private void Awake()
    {
        playerDictionary = new Dictionary<string, GameObject>();
    }

    // remove all players from stage, reset
    public void ResetPlayers()
    {

        playerDictionary.Clear();

        // loop through the feed and add players
        foreach (var feed in DataManager.feeds)
        {
            CreateNewPlayer(feed.username);
        }
    }

    public void CreateNewPlayer(string username)
    {
        if (playerDictionary.ContainsKey(username)) return;


        Vector3 spawnPosition = GetClearSpawnPosition();

        Quaternion spawnRotation = new Quaternion();
        spawnRotation.eulerAngles = new Vector3(0.0f, Random.Range(0.0f, 360.0f));
        if (spawnPosition != Vector3.zero)
        {
            GameObject obj = (GameObject)Instantiate(playerPrefab, spawnPosition, spawnRotation);


            obj.GetComponent<Player>().Init(username);
            // add to dict
            playerDictionary.Add(username, obj);

        }
    }


    /**
     *  Return random Vector3 position that doesn't already have a GameObject in it
     */
    Vector3 GetClearSpawnPosition()
    {
        Vector3 spawnPosition = new Vector3();
        float startTime = Time.realtimeSinceStartup;
        bool positionClear = false;
        int layerMask = (1 << 8); // only Layer 8 "Players"
        while (positionClear == false)
        {
            // get random position
            Vector3 spawnPositionRaw = RandomPointInBounds(worldContainerCollider.bounds);
            // store for test
            spawnPosition = new Vector3(spawnPositionRaw.x, spawnPositionRaw.y, spawnPositionRaw.z);
            // get all the overlaping colliders
            Collider[] hitColliders = Physics.OverlapSphere(spawnPosition, 0.75f, layerMask);
            // if collider isn't touching another player collider then set true to stop loop
            if (hitColliders.Length <= 0) positionClear = true;
            // else continue until time has run out
            if (Time.realtimeSinceStartup - startTime > 0.5f)
            {
                Debug.Log("Time out placing GameObject!");
                return Vector3.zero;
            }
        }
        return spawnPosition;
    }

    /**
     *  Return random Vector3 position inside bounds
     */
    public static Vector3 RandomPointInBounds(Bounds bounds)
    {
        return new Vector3(
            Random.Range(bounds.min.x, bounds.max.x),
            Random.Range(bounds.min.y, bounds.max.y),
            Random.Range(bounds.min.z, bounds.max.z)
        );
    }



}
