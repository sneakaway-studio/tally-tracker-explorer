using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


/**
 *  Each mid and corresponding trail and monster animation for each monster found in feed data
 */
public class TrailingMonster {
    // index
    public int mid;
    public int passes = 0;
    // scene obj references
    //public Trail trail;
    //public Monster monster;
    // constructor
    public TrailingMonster (int mid)
    {
        this.mid = mid;
        //this.trail = ReturnTrail ();
    }
    // increment each update unless this monster still exists in feed data
    public void Touch ()
    {
        passes++;
    }
    public void ResetPasses ()
    {
        passes = 0;
    }
    // Remove monster and trail from scene
    public void Remove ()
    {
        // trailManager.remove(mid) - something like this
    }
}


/**
 *  Attached to Player GameObject 
 *  - Stores, adds, removes collection of monsters provided by feed data
 *  - Calls add / remove on TrailManager and MonsterManager 
 */
public class TrailingMonstersManager : MonoBehaviour {

    // min, max, count of trailing monsters
    public int min = 3;
    public int max = 8;
    public int count;

    // dictionary: mid->TrailingMonster 
    public Dictionary<int, TrailingMonster> trailingMonstersDict;
    // mids only
    public List<int> mids = new List<int> ();
    public List<string> midsPasses = new List<string> ();
    // the highest number of passes in each loop
    public List<int> highestPassesToDelete = new List<int> ();

    // managers
    public MonsterManager monsterManager;
    public TrailManager trailManager;

    private void Awake ()
    {
        trailingMonstersDict = new Dictionary<int, TrailingMonster> ();
        count = trailingMonstersDict.Count;

        StartCoroutine (UpdateTrailingMonsters (1f, true));
    }

    public void Start ()
    {
        // placeholder to turn on/off in inspector
    }

    int safety = 0;

    /**
     *  Check and update dictionary
     */
    IEnumerator UpdateTrailingMonsters (float wait, bool random)
    {
        // wait a second 
        yield return new WaitForSeconds (wait);

        // testing
        if (random) {
            // use random number for length
            count = (int)Random.Range (min, max);
        } else {
            // use mids in Player feedData
            // ... count = ...
        }

        // if there are trailing monsters already
        if (trailingMonstersDict.Count > 0) {
            // touch all existing 
            foreach (KeyValuePair<int, TrailingMonster> t in trailingMonstersDict) {
                trailingMonstersDict [t.Key].Touch ();
            }
        }

        //PrintDict ();



        // create and add (or update) each new trailing monster
        for (int i = 0; i < count; i++) {

            // random 
            int mid = i;

            // random mid
            mid = MonsterIndex.Instance.GetRandomMid (0, 20);

            // if new mid already exists in dict
            if (trailingMonstersDict.ContainsKey (mid)) {
                // touch to reset passes
                trailingMonstersDict [mid].ResetPasses ();
            } else {
                // add to dict
                trailingMonstersDict.Add (mid, new TrailingMonster (mid));
                // add monster
                //monsterManager.AddMonster (mid);
                //trailManager.AddTrail (mid);
            }

        }

        // prune old monsters
        PruneTrailingMonsters ();

        // TESTING
        //monsterManager.RemoveAllMonsters ();
        //StartCoroutine (DeleteAllThenAdd ());
        //StartCoroutine (trailManager.UpdateTrails (0));


        // tests so we can watch in the inspector
        if (trailingMonstersDict.Count > 0) {
            // update mids 
            mids.Clear ();
            mids = trailingMonstersDict.Keys.ToList ();

            // clear list
            midsPasses.Clear ();
            // update strings
            foreach (KeyValuePair<int, TrailingMonster> t in trailingMonstersDict) {
                midsPasses.Add (t.Key.ToString () + "-" + trailingMonstersDict [t.Key].passes.ToString ());
            }
        }


        count = trailingMonstersDict.Count;



        //if (++safety < 20)
        // test (calls itself)
        StartCoroutine (UpdateTrailingMonsters (5f, true));
    }


    void PrintDict (string header = "*** trailingMonstersDict ***")
    {
        Debug.Log (header);
        foreach (KeyValuePair<int, TrailingMonster> t in trailingMonstersDict) {
            Debug.Log (trailingMonstersDict [t.Key].mid + " = " + trailingMonstersDict [t.Key].passes);
        }
    }

    /**
     *  Remove old monsters
     */
    void PruneTrailingMonsters ()
    {
        // check that there are monsters to remove
        if (trailingMonstersDict.Count < 0 || trailingMonstersDict.Count < max) return;


        // 1. GET HIGHEST # PASSES

        // starting with first
        KeyValuePair<int, TrailingMonster> highestPasses = trailingMonstersDict.First ();
        // loop and get highest # passes
        foreach (KeyValuePair<int, TrailingMonster> t in trailingMonstersDict) {
            if (t.Value.passes > highestPasses.Value.passes) highestPasses = t;
        }

        // 2. UPDATE DELETE COLLECTION

        // sort dict by # passes ascending so we keep the newest ones (descending use: OrderByDescending())
        trailingMonstersDict = trailingMonstersDict.OrderBy (x => x.Value.passes).ToDictionary (x => x.Key, x => x.Value);
        // reset list
        highestPassesToDelete.Clear ();
        int i = 0;
        // loop and 
        foreach (KeyValuePair<int, TrailingMonster> t in trailingMonstersDict) {
            i++;
            // add those with passes >= highestPasses
            if (trailingMonstersDict [t.Key].passes >= highestPasses.Value.passes || i >= max) {
                //Debug.Log (transform.parent.name + " REMOVE " + t.Key);
                highestPassesToDelete.Add (t.Key);
            }
        }


        // 3. REMOVE

        // delete loop
        foreach (int mid in highestPassesToDelete) {
            trailingMonstersDict.Remove (mid);

            // remove corresponding monster and trail
            monsterManager.RemoveMonster (mid);
            trailManager.RemoveTrail (mid);
        }

        // 4. ADD NEW

        foreach (KeyValuePair<int, TrailingMonster> t in trailingMonstersDict) {
            monsterManager.AddMonster (t.Key);
            trailManager.AddTrail (t.Key);
            StartCoroutine (trailManager.UpdateTrails (0));
        }


    }



    // TEST - delete all and add only current back - automatically sorts them
    IEnumerator DeleteAllThenAdd ()
    {

        monsterManager.RemoveAllMonsters ();
        trailManager.RemoveAllTrails ();

        yield return new WaitForSeconds (0);

        foreach (KeyValuePair<int, TrailingMonster> t in trailingMonstersDict) {
            monsterManager.AddMonster (t.Key);
            trailManager.AddTrail (t.Key);
            StartCoroutine (trailManager.UpdateTrails (0));
        }

    }



}
