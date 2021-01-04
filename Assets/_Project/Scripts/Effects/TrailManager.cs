using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TrailManager : MonoBehaviour {

    // trails following the leader
    private List<Transform> trails = new List<Transform> ();
    // new list of trail mids in order
    public List<int> newTrailMids = new List<int> ();

    public int minTrailCount = 3;
    public int maxTrailCount = 8;
    public int numberTrails = 0;
    public GameObject trailPrefab;



    void Awake ()
    {
    }


    /**
     *  Adds random number of test trails
     */
    IEnumerator AddTestTrails (float wait)
    {
        yield return new WaitForSeconds (wait);

        // random num
        numberTrails = (int)Random.Range (minTrailCount, maxTrailCount);
        // add trail
        for (int i = 0; i < numberTrails; i++) {
            AddTrail (i);
        }
        // update
        StartCoroutine (UpdateTrails (0));
    }



    /**
     *  Adds a trail
     */
    public void AddTrail (int _mid = -1)
    {
        // if at max trails
        if (trails.Count >= 8) return;

        if (_mid < 1) {
            // get a random mid from those in the game
            _mid = MonsterIndex.Instance.GetRandomMid ();
        }

        // create name for obj
        string name = _mid.ToString ();

        // if already exists then exit
        var exists = trails.Find (item => item.name.Equals (name));
        if (exists != null) {
            //Debug.Log ("TRAIL ALREADY EXISTS IN LIST");
            return;
        }

        // add trail to scene
        GameObject obj = (GameObject)Instantiate (trailPrefab);
        // set name in Unity Editor
        obj.name = name;
        // parent under this manger
        obj.transform.parent = gameObject.transform;
        // add to list
        trails.Add (obj.transform);
    }

    // Removes a trail
    public void RemoveTrail (int _mid = -1)
    {
        if (_mid < 1 || trails.Count < 1) return;

        for (int i = 0; i < trails.Count; i++) {
            if (trails [i].name == _mid.ToString ()) {
                // remove from game
                Destroy (trails [i].gameObject);
                // remove from list
                trails.RemoveAt (i);
                break;
            }
        }
    }

    public void RemoveAllTrails ()
    {
        for (int i = 0; i < trails.Count; i++) {
            // remove from game
            Destroy (trails [i].gameObject);
        }
        trails.Clear ();
    }


    /**
     *  Update positions after trails added / removed
     */
    public IEnumerator UpdateTrails (float wait)
    {
        yield return new WaitForSeconds (wait);

        // sort trails by name (mid)
        trails = trails.OrderBy (o => o.name).ToList ();

        for (int i = 0; i < trails.Count; i++) {
            trails [i].GetComponent<Trail> ().UpdatePositions ();
        }
    }

    /**
     *  Update positions
     */
    public IEnumerator UpdateTrailPositions (float wait)
    {
        yield return new WaitForSeconds (wait);

        // sort trails by name (mid)
        //trails = trails.OrderBy (o => o.name).ToList ();

        for (int i = 0; i < trails.Count; i++) {
            // get index of trail in new list
            int newIndex = newTrailMids.IndexOf (trails [i].GetComponent<Trail> ().mid);
            // set sibling index to updated index 
            trails [i].transform.SetSiblingIndex (newIndex);
        }

        for (int i = 0; i < trails.Count; i++) {
            trails [i].GetComponent<Trail> ().UpdatePositions ();
        }

    }



}

