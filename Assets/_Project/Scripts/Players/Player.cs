using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    // META

    public FeedData feedData;
    public string username;
    public string avatarPath;
    public bool effectIsPlaying;
    public DateTime lastActive;


    // OBJECT & SCRIPT REFERENCES

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



}
