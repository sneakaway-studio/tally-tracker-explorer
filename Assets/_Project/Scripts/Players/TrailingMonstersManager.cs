using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


/**
 *  For each monster found in feed data
 */
public class TrailingMonster {
    // index
    public int mid;
    // increment each update unless this monster still exists in feed data
    public int passes = -1;
    // constructor
    public TrailingMonster (int mid)
    {
        this.mid = mid;
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

    // all the new MIDs just received from the feed
    public List<int> newMidsFromFeed = new List<int> ();
    // dictionary: mid->TrailingMonster 
    public Dictionary<int, TrailingMonster> trailingMonstersDict;
    // list of mids saved after each update
    public List<int> trailingMonstersList = new List<int> ();

    // mids only
    public List<int> mids = new List<int> ();
    public List<string> midsPasses = new List<string> ();
    // the highest number of passes in each loop
    public List<int> highestPassesToDelete = new List<int> ();

    // managers
    public Player player;
    public MonsterManager monsterManager;
    public TrailManager trailManager;


    private void Awake ()
    {
        trailingMonstersDict = new Dictionary<int, TrailingMonster> ();
        count = trailingMonstersDict.Count;

        // test
        //StartCoroutine (UpdateTrailingMonsters (1f, true));
    }
    public void Start ()
    {
        // placeholder to turn on/off in inspector
    }



    /**
     *  Check and update dictionary
     */
    public IEnumerator UpdateTrailingMonsters (float wait, bool random)
    {
        // wait a second 
        yield return new WaitForSeconds (wait);


        //Debug.Log ("TrailingMonsterManager.UpdateTrailingMonsters()");


        // 0. GET NEW LIST OF MIDs

        // testing
        if (random) {
            //create list of new monsters to add
            newMidsFromFeed = DebugManager.Instance.GetListOfRandomInts (min, max, 0, 12);
        } else {
            // use mids in Player feedData
            newMidsFromFeed = player.tagMatchesMids;
        }
        // safety
        if (newMidsFromFeed.Count < 1) {
            DebugManager.Instance.PrintList ("XX newMidsFromFeed", newMidsFromFeed);

            // return early?
            yield break;

            // add first party trackers?
            //MonsterIndex.Instance.GetRandomFirstPartyMid
        }
        DebugManager.Instance.PrintList ("newMidsFromFeed", newMidsFromFeed);





        // 1. TOUCH EXISTING MIDs IN DICT, SORT TRAILS BY MID

        // if there are trailing monsters already
        if (trailingMonstersDict.Count > 0) {
            // touch all existing 
            foreach (KeyValuePair<int, TrailingMonster> t in trailingMonstersDict) {
                trailingMonstersDict [t.Key].passes++;
            }

            // NOTE: Sorting the trails by MID is the "least ugly" method to remove/add new trails

            // sorted by passes
            //trailingMonstersList = trailingMonstersDict.Keys.ToList ();
            // sorted by mids
            trailingMonstersList = trailingMonstersDict.OrderBy (x => x.Value.mid).Select (kvp => kvp.Key).ToList ();
            if (trailingMonstersList.Count > 0) {
                // then update the lists inside the managers 
                monsterManager.newTrailMids = trailingMonstersList;
                trailManager.newTrailMids = trailingMonstersList;
                // then tell them to all reset their order
                StartCoroutine (trailManager.UpdateTrailPositions (0));
                monsterManager.UpdateMonsterPositions ();

            }
        }
        DebugManager.Instance.PrintDict ("trailingMonstersDict [1] (after passes++)", trailingMonstersDict);




        // 2. RESET PASSES ON EXISTING MIDs / ADD NEW MIDs FROM FEED

        // sort dict by # passes ascending, then mid, so we keep the newest ones (descending use: OrderByDescending())
        trailingMonstersDict = trailingMonstersDict
            .OrderBy (x => x.Value.passes)
            .ThenBy (x => x.Value.mid)
            .ToDictionary (x => x.Key, x => x.Value);

        // loop through list of new mids
        for (int i = 0; i < newMidsFromFeed.Count; i++) {

            // get mid 
            int mid = newMidsFromFeed [i];

            // if new mid already exists in dict
            if (trailingMonstersDict.ContainsKey (mid)) {
                // reset passes
                trailingMonstersDict [mid].passes = 0;
            } else {
                // make sure MID is in the list of MIDs in the visualization
                int gameMidsIndex = MonsterIndex.Instance.GetGameMidIndex (mid);
                if (gameMidsIndex > -1) {
                    // if we are at max
                    if (trailingMonstersDict.Count >= max) {
                        // remove an old one
                        int midToRemove = trailingMonstersDict.Last ().Key;
                        //Debug.Log ("REMOVE midToRemove = " + midToRemove);
                        trailingMonstersDict.Remove (midToRemove);
                        monsterManager.RemoveMonster (midToRemove);
                        trailManager.RemoveTrail (midToRemove);
                    }
                    // add new to dict
                    trailingMonstersDict.Add (mid, new TrailingMonster (mid));
                    monsterManager.AddMonster (mid);
                    trailManager.AddTrail (mid);
                } else {
                    // otherwise skip
                    continue;
                }
            }
        }
        DebugManager.Instance.PrintDict ("trailingMonstersDict [2] (after additions)", trailingMonstersDict);




        // 3. SORT DICT BY # PASSES 

        // sort dict by # passes ascending, then mid, so we keep the newest ones (descending use: OrderByDescending())
        trailingMonstersDict = trailingMonstersDict
            .OrderBy (x => x.Value.passes)
            .ThenBy (x => x.Value.mid)
            .ToDictionary (x => x.Key, x => x.Value);

        DebugManager.Instance.PrintDict ("trailingMonstersDict [3] (after sort)", trailingMonstersDict);






        // 4. REMOVE OLD MONSTERS FROM DICT

        // make sure there are some
        if (trailingMonstersDict.Count > 0) {

            // now that it is sorted ascending, the last index has the highest value
            int highestPasses = Mathf.Max (trailingMonstersDict.Last ().Value.passes, 1);

            int j = 0;

            // loop through copy of list of dict to avoid error when removing from dict
            foreach (var t in trailingMonstersDict.ToList ()) {
                //Debug.Log ("-- t.Key = " + t.Key + ", j = " + j + ", highestPasses = " + highestPasses);
                // if >= max OR has high passes
                if (j >= max || (t.Value.passes > 1 && t.Value.passes >= highestPasses)) {
                    //Debug.Log ("REMOVE t.Key = " + t.Key + ", j = " + j);
                    trailingMonstersDict.Remove (t.Key);
                    // remove corresponding monster and trail
                    monsterManager.RemoveMonster (t.Key);
                    trailManager.RemoveTrail (t.Key);
                }
                j++;
            }
            DebugManager.Instance.PrintDict ("trailingMonstersDict [4] (after prune)", trailingMonstersDict);
        }


        // 4. ADD NEW

        //foreach (KeyValuePair<int, TrailingMonster> t in trailingMonstersDict) {
        //    // it is new
        //    if (t.Value.passes == -1) {
        //        monsterManager.AddMonster (t.Key);
        //        trailManager.AddTrail (t.Key);
        //        trailingMonstersDict [t.Key].passes = 0;
        //    }
        //}




        // update all monster positions after new have been added
        monsterManager.UpdateMonsterPositions ();
        StartCoroutine (trailManager.UpdateTrailPositions (0));




        // TESTS

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




        // test (calls itself)
        //StartCoroutine (UpdateTrailingMonsters (5f, true));
    }









    //// TEST - delete all and add only current back - automatically sorts them
    //IEnumerator DeleteAllThenAdd ()
    //{

    //    monsterManager.RemoveAllMonsters ();
    //    trailManager.RemoveAllTrails ();

    //    yield return new WaitForSeconds (0);

    //    foreach (KeyValuePair<int, TrailingMonster> t in trailingMonstersDict) {
    //        monsterManager.AddMonster (t.Key);
    //        trailManager.AddTrail (t.Key);
    //        //StartCoroutine (trailManager.UpdateTrails (0));
    //    }
    //}



}
