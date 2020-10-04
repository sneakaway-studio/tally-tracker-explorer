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


    // PLAYBACK

    public bool playbackActive = false; // if the playback is currently active 
    public int feedIndex;               // index of current feed item we are currently displaying

    public DateTime previousTime;       // previous time displayed
    public int timeDiff;                // difference between current time (feed.createdAt) and previousTime
    public float timeDiffScaled;        // difference - adjusted for playback


    [Range (0f, 1f)]
    public float timeDiffScalar = 0.01f;  // timeDiff * scalar = how much faster time is replayed 


    // min and max allowed between events
    public float minTimeDiff = 1;
    public float maxTimeDiff = 10;



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
            timeDiff = (int)(feed.createdAt - previousTime).TotalSeconds;

            // adjust difference (speed it up)
            timeDiffScaled = timeDiff * timeDiffScalar;

            // log feed item
            var eventString =

                feedIndex + ". " +
                timeDiff + " (" + timeDiffScaled + ") " +

                " (" + feed.createdAt + ") " +
               //" = (" + previousTime + " - " + feed.createdAt + ") " +
               feed.username + ", " + feed.eventType +

               "";

            feedText.text += eventString + "<br>";



            // TEMP

            // pick random player and random event
            PlayerManager.Instance.PlayEvent (feed);

            // trigger data updated event
            //EventManager.TriggerEvent ("DataUpdated");


            UpdateScroll ();


            Debug.Log (eventString);


            // set previous time for next loop
            previousTime = feed.createdAt;
            // display in feed console

            // if there are more feed items
            if (++feedIndex < DataManager.feeds.Count) {
                // wait for difference befor next loop
                yield return new WaitForSeconds (timeDiffScaled);
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
