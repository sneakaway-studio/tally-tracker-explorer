using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailsManager : MonoBehaviour {

    public int minTrailCount = 3;
    public int maxTrailCount = 10;
    public int numberTrails = 0;
    public GameObject trailPrefab;
    public Dictionary<string, GameObject> trailDict;

    private void Awake ()
    {
        trailDict = new Dictionary<string, GameObject> ();

        // TEMP ADD RANDOM COUNT
        numberTrails = (int)Random.Range (minTrailCount, maxTrailCount);
        for (int i = 0; i < numberTrails; i++) {
            AddTrail (i, false);
        }
        // update after a second
        StartCoroutine (UpdateTrails (1f));
    }


    /**
     *  Adds the trail at index
     */
    void AddTrail (int index, bool updateAll)
    {
        // add player to scene
        GameObject obj = (GameObject)Instantiate (trailPrefab);
        // create name for trail obj
        string name = "trail-" + (index + 1).ToString ();
        // set name in Unity Editor
        obj.name = name;
        // parent under this manger
        obj.transform.parent = gameObject.transform;
        // if it already exists
        if (trailDict.ContainsKey (name)) {
            // update
            //trailDict.Add (name, obj);
        } else {
            // add to dict
            trailDict.Add (name, obj);
        }


        if (updateAll) StartCoroutine (UpdateTrails (1f));
    }


    /**
     *  Loop through all the trails to update their positions after trails added or removed
     */
    IEnumerator UpdateTrails (float wait)
    {
        yield return new WaitForSeconds (wait);

        foreach (string t in trailDict.Keys) {
            trailDict [t].GetComponent<TrailController> ().Init ();
        }

    }



}
