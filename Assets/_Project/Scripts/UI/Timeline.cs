using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timeline : MonoBehaviour
{
    // whether the playback is currently active 
    public bool playbackActive = false;
    // the feed item we are currently displaying
    public int feedIndex;
    // current time to display
    public DateTime currentTime;
    // previous time displayed
    public DateTime previousTime;
    // difference between currentTime and previousTime
    public int diffInSeconds;
    // difference - adjusted for playback
    public float diffInSecondsAdj;


    // UI
    public TMP_Text TmText;
    public ScrollRect scrollRect;



    public Slider speedSlider;
    public float speedMin = 0;
    public float speedMax = 100;



    void Start()
    {
        speedSlider.minValue = speedMin;
        speedSlider.maxValue = speedMax;
        speedSlider.value = speedMax / 2;
    }


    // empty playback queue and restart
    public void StartPlayBack()
    {
        // if currently playing
        if (playbackActive)
        {
            // then stop before starting
            StopCoroutine("Play");
        }
        // start
        StartCoroutine("Play");
        playbackActive = true;
    }
    public void StopPlayBack()
    {
        // only if active
        if (playbackActive) return;
        StopCoroutine("Play");
        playbackActive = false;
    }

    // play all the events
    IEnumerator Play()
    {
        feedIndex = 0;

        foreach (var feed in DataManager.feeds)
        {
            // current time
            currentTime = feed.time;
            // on first run 
            if (previousTime == null)
            {
                // use current time
                previousTime = currentTime;
            }
            // difference in seconds
            diffInSeconds = (int)(previousTime - currentTime).TotalSeconds;
            // difference adjusted
            diffInSecondsAdj = diffInSeconds * 0.0001f;

            // log feed item
            var log = feedIndex + ". " +
                diffInSeconds + " (" + diffInSecondsAdj + ") " +
                " (" + currentTime + ") " +
               //" = (" + previousTime + " - " + currentTime + ") " +
               feed.username + ", " + feed.item;
            TmText.text += log + "<br>";
            UpdateScroll();
            //Debug.Log(log);


            // set previous time for next loop
            previousTime = currentTime;
            // display in feed console

            // if there are more feed items
            if (++feedIndex < DataManager.feeds.Count)
            {
                // wait for difference befor next loop
                yield return new WaitForSeconds(diffInSecondsAdj);
            }
            else
            {
                // reset
            }

        }

    }


    public void UpdateScroll()
    {
        Canvas.ForceUpdateCanvases();
        scrollRect.verticalNormalizedPosition = 0f;
    }

}
