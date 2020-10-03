using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimelineManager : Singleton<TimelineManager> {
    // singleton
    protected TimelineManager () { }
    //public static new PlayerManager Instance;

    // whether the playback is currently active 
    public bool playbackActive = false;
    // the feed item we are currently displaying
    public int feedIndex;
    // previous time displayed
    public DateTime previousTime;
    // difference between current time (feed.createdAt) and previousTime
    public int diffInSeconds;
    // difference - adjusted for playback
    public float diffInSecondsAdj;


    // UI
    public TMP_Text feedText;
    public ScrollRect scrollRect;



    //public Slider speedSlider;
    //public float speedMin = 0;
    //public float speedMax = 100;



    void Start ()
    {
        //speedSlider.minValue = speedMin;
        //speedSlider.maxValue = speedMax;
        //speedSlider.value = speedMax / 2;
    }


    // empty playback queue and restart
    public void StartPlayBack ()
    {
        // if currently playing
        if (playbackActive) {
            // then stop before starting
            StopCoroutine ("Play");
        }
        // start
        StartCoroutine ("Play");
        playbackActive = true;
    }
    public void StopPlayBack ()
    {
        // only if active
        if (playbackActive) return;
        StopCoroutine ("Play");
        playbackActive = false;
    }

    // play all the events
    IEnumerator Play ()
    {
        feedIndex = 0;

        foreach (var feed in DataManager.feeds) {
            // on first run only
            if (previousTime == null)
                // use current time as previous time
                previousTime = feed.createdAt;


            // find difference in seconds between createdAt and previousTime 
            diffInSeconds = (int)(feed.createdAt - previousTime).TotalSeconds;

            // adjust difference (speed it up)
            diffInSecondsAdj = diffInSeconds * 0.0015f;

            // log feed item
            var log =

                feedIndex + ". " +
                diffInSeconds + " (" + diffInSecondsAdj + ") " +

                " (" + feed.createdAt + ") " +
               //" = (" + previousTime + " - " + feed.createdAt + ") " +
               feed.username + ", " + feed.eventType +

               "";
            feedText.text += log + "<br>";



            // TEMP

            // pick random player and random event
            PlayerManager.Instance.PlayRandomEvent ();

            // trigger data updated event
            //EventManager.TriggerEvent ("DataUpdated");


            UpdateScroll ();


            //Debug.Log(log);


            // set previous time for next loop
            previousTime = feed.createdAt;
            // display in feed console

            // if there are more feed items
            if (++feedIndex < DataManager.feeds.Count) {
                // wait for difference befor next loop
                yield return new WaitForSeconds (diffInSecondsAdj);
            } else {
                // reset
            }

        }

    }


    public void UpdateScroll ()
    {
        Canvas.ForceUpdateCanvases ();
        scrollRect.verticalNormalizedPosition = 0f;
    }

}
