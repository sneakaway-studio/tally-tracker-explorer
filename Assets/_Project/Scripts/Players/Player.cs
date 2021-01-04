using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json.Linq;

public class Player : MonoBehaviour {

    // META

    public FeedData feedData;                   // the current / most recent feed data 
    public List<int> tagMatchesMids = new List<int> ();
    public string username;
    public string avatarPath;
    public bool effectIsPlaying;
    public DateTime lastActive;


    // OBJECT & SCRIPT REFERENCES

    public TrailingMonstersManager trailingMonstersManager;
    public GameObject playerCharacter;          // player character obj
    public GameObject playerObject;             // effect obj - for attaching animations
    public GameObject effects;                  // effect obj - for attaching animations
    public AnimController animControllerScript; // animation controller script


    // Camera manager
    public CameraManager cameraManager;

    private void Awake ()
    {
        StartCoroutine (StartChecks ());
    }


    public void Init (FeedData _feedData)
    {
        //Debug.Log ("Player.Init() _username = " + _username);
        Debug.Log ("Player.Init() _feedData.tagMatches = " + _feedData.tagMatches);

        // test
        //SaveTagMatchesFromStream (_feedData.tagMatches);

        feedData = _feedData;
        username = _feedData.username;
        avatarPath = _feedData.avatarPath;
        lastActive = DateTime.Now;
    }

    /**
     *  Hide if no username
     */
    IEnumerator StartChecks ()
    {
        yield return new WaitForSeconds (1f);

        // you left your toys out
        if (username == null || username == "")
            gameObject.SetActive (false);
    }

    /**
     *  Parse the tagMatches (monsters) string from the stream and update trails and monster sprites
     */
    public void SaveTagMatchesFromStream (string tagMatchesStr)
    {
        // clear list
        tagMatchesMids.Clear ();

        // parse JSON array 
        if (tagMatchesStr != "") {

            JObject o = JObject.Parse (tagMatchesStr);
            //Debug.Log (o);
            //Debug.Log (o.SelectToken ("s0") [0]);

            if (o.Count > 0) {

                // start from the most current 

                if (o.SelectToken ("s3") != null) {
                    foreach (int mid in o.SelectToken ("s3")) {
                        //Debug.Log ("s3 - " + mid);
                        tagMatchesMids.Add (mid);
                    }
                }
                if (o.SelectToken ("s2") != null) {
                    foreach (int mid in o.SelectToken ("s2")) {
                        //Debug.Log ("s2 - " + mid);
                        tagMatchesMids.Add (mid);
                    }
                }
                if (o.SelectToken ("s1") != null) {
                    foreach (int mid in o.SelectToken ("s1")) {
                        //Debug.Log ("s1 - " + mid);
                        tagMatchesMids.Add (mid);
                    }
                }
                if (o.SelectToken ("s0") != null) {
                    foreach (int mid in o.SelectToken ("s0")) {
                        //Debug.Log ("s0 - " + mid);
                        tagMatchesMids.Add (mid);
                    }
                }
                // test
                DebugManager.Instance.PrintList ("\n\ntagMatchesMids " + username + " ", tagMatchesMids);
                // update trailing monsters, with random set to false
                StartCoroutine (trailingMonstersManager.UpdateTrailingMonsters (1f, false));
            }
        }
    }



    // update trails

    // update monsters 

}
