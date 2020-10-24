using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using System;
using UnityEngine.UI;
using TMPro;



/**
 *  Timeline base class
 *   - new data from server or filesystem is placed in the buffer
 *   - on play, each event is taken from the buffer, visualized, then placed in the history
 *   - if buffer gets low then we attempt to fill it either with new data or by repeating events in history
 */
public class Timeline : Singleton<Timeline> {
    // singleton
    protected Timeline () { }
    //public static new Timeline Instance;



    // BUFFER

    [Space (10)]
    [Header ("BUFFER")]

    public List<FeedData> buffer = new List<FeedData> ();

    [Tooltip ("Current number of feed events in buffer")]
    public int bufferCount;

    [Tooltip ("Max items allowed in buffer")]
    [Range (1, 20)]
    public int bufferCountMin;
    [Tooltip ("Min items allowed in buffer")]
    [Range (10, 100)]
    public int bufferCountMax;

    [Tooltip ("How often the buffer is checked (in seconds)")]
    public int bufferCheckFrequency = 2;


    public TMP_Text bufferText;
    public ScrollRect bufferScrollRect;
    public TMP_Text bufferTitleText;




    [Tooltip ("Time since a data request made active")]
    public int dataRequestProgress;

    [Tooltip ("Is the timeline is currently active?")]
    public bool active = false;



    // HISTORY 

    [Space (10)]
    [Header ("HISTORY")]

    public List<FeedData> history = new List<FeedData> ();

    [Tooltip ("Current number of feed events in history")]
    public int historyCount;

    public TMP_Text historyText;
    public ScrollRect historyScrollRect;
    public TMP_Text historyTitleText;




    // TIME

    [Space (10)]
    [Header ("TIME")]

    [Tooltip ("DateTime of previously displayed event")]
    [SerializeField]
    DateTime previousTime;

    [Tooltip ("Difference (seconds) between current time (feed.createdAt) and previousTime")]
    public int timeDiff;

    [Tooltip ("Difference (seconds) adjusted for loop")]
    public float timeDiffScaled;

    [Tooltip ("How much faster time is replayed (timeDiff * scalar)")]
    [Range (0f, 1f)]
    public float timeDiffScalar = 0.01f;

    [Tooltip ("Min time allowed between events")]
    public float minTimeDiff = 1;
    [Tooltip ("Max time allowed between events")]
    public float maxTimeDiff = 10;


    public SettingsManager settingsManager;



    // listeners
    void OnEnable ()
    {
        EventManager.StartListening ("StartTimeline", StartTimeline);
    }
    void OnDisable ()
    {
        EventManager.StopListening ("StartTimeline", StartTimeline);
    }

    private void Awake ()
    {
        //
    }

    // empty loop queue and restart
    public void StartTimeline ()
    {
        // stop and restart if currently playing
        if (active)
            StopTimeline ();
        // start
        StartCoroutine ("CheckBuffer");
        StartCoroutine ("Loop");
        active = true;
    }
    public void StopTimeline ()
    {
        // if active
        if (!active) return;
        // stop coroutines
        StopCoroutine ("CheckBuffer");
        StopCoroutine ("Loop");
        // set false
        active = false;
        // reset previous time
        previousTime = DateTime.MinValue;
    }






    /////////////////////////////////////////////////////////////
    //////////////////////// BUFFER /////////////////////////////
    /////////////////////////////////////////////////////////////


    /**
     *  Check the buffer, add data
     */
    IEnumerator CheckBuffer ()
    {
        while (true) {

            //Debug.Log ("Timeline.CheckBuffer()");

            // update count
            UpdateCounts ();

            DebugManager.Instance.UpdateDisplay ("Timeline.CheckBuffer() count = " + bufferCount.ToString ());

            // is the bufferCount > max?
            if (bufferCount > bufferCountMax) {
                // skip this time
                continue;
            }
            // is the bufferCount < min OR the bufferCount empty?
            else if (bufferCount <= bufferCountMin || bufferCount < 1) {

                // if we haven't started a new data request already
                if (dataRequestProgress < 1) {
                    // attempt to get new data from server
                    EventManager.TriggerEvent ("GetNewData");
                    // start tracking time since request
                    dataRequestProgress++;
                }
                // maybe a problem with server
                else if (++dataRequestProgress > 10) {
                    // or if no data then copy from history
                    MoveListRange (bufferCountMax, history, buffer);
                }
            }

            UpdateCounts ();

            // after 
            if (bufferCount > 0) {
                // after update, sort ascending
                buffer.Sort ((x, y) => x.createdAt.CompareTo (y.createdAt));
                // display
                UpdateTimelineDisplay ();
                // if we have data in the buffer or history but no players then add them
                if (PlayerManager.Instance.playerCount < 1) {
                    EventManager.TriggerEvent ("ResetPlayers");
                }
            }

            yield return new WaitForSeconds (bufferCheckFrequency);
        }
    }



    /////////////////////////////////////////////////////////////
    //////////////////////// LOOP ///////////////////////////////
    /////////////////////////////////////////////////////////////


    /**
     *  Play events from buffer and move to history
     */
    IEnumerator Loop ()
    {
        UpdateCounts ();

        while (true) {

            //Debug.Log ("Timeline.Loop()");

            // if buffer has items
            if (bufferCount > 0) {

                // get first feed in buffer
                FeedData feed = buffer [0];


                // HANDLE TIME
                //Debug.Log ("previousTime = " + previousTime);
                // on first run only, use current time as previous time
                if (previousTime == null) previousTime = feed.createdAt;


                // find difference in seconds between createdAt and previousTime 
                timeDiff = (int)(feed.createdAt - previousTime).TotalSeconds;

                // scaled difference (speed it up)
                timeDiffScaled = Mathf.Clamp (timeDiff * timeDiffScalar, minTimeDiff, maxTimeDiff);

                // set previous time for next loop
                previousTime = feed.createdAt;



                // PLAY EVENT

                // let PlayerManager find player and visualize event
                PlayerManager.Instance.PlayEvent (feed);


                // MANAGE EVENT

                // move event to history
                MoveListRange (1, buffer, history);



                UpdateCounts ();

                // after 
                if (historyCount > 0) {
                    // after update, sort ascending
                    history.Sort ((x, y) => x.createdAt.CompareTo (y.createdAt));
                    // display
                    UpdateTimelineDisplay ();
                }


                // log feed item
                var eventString = timeDiff + " (" + timeDiffScaled + ") " +
                    " (" + feed.createdAt + ") " +
                    //" = (" + previousTime + " - " + feed.createdAt + ") " +
                    feed.username + ", " + feed.eventType + "";

                DebugManager.Instance.UpdateDisplay ("Timeline.Loop() " + eventString);


            } else {
                // safety
                timeDiffScaled = 1;
            }

            // time difference to next event (or safety)
            yield return new WaitForSeconds (timeDiffScaled);
        }
    }



    /////////////////////////////////////////////////////////////
    ////////////////////// GENERAL //////////////////////////////
    /////////////////////////////////////////////////////////////


    /**
     *  Update display for both
     */
    void UpdateTimelineDisplay ()
    {
        string bufferString = "";
        string historyString = "";
        int safety = 0;

        foreach (var feed in buffer) {
            bufferString += feed.eventType + ". " + feed.createdAt + " - " + feed.username + "<br>";
            if (++safety > 100) {
                Debug.Log ("Safety first!");
                break;
            }
        }
        safety = 0;
        foreach (var feed in history) {
            historyString += feed.eventType + ". " + feed.createdAt + " - " + feed.username + "<br>";
            if (++safety > 100) {
                Debug.Log ("Safety first!");
                break;
            }
        }

        bufferText.text = bufferString;
        historyText.text = historyString;

        UpdateCounts ();
        UpdateScroll ();

        bufferTitleText.text = "Buffer [ " + bufferCount + " ] ";
        historyTitleText.text = "History [ " + historyCount + " ] ";

        // trigger timeline updated event
        EventManager.TriggerEvent ("TimelineUpdated");
    }

    /**
     *  Update counts of both history and buffer
     */
    void UpdateCounts ()
    {
        bufferCount = buffer.Count;
        historyCount = history.Count;
    }

    public void UpdateScroll ()
    {
        Canvas.ForceUpdateCanvases ();
        bufferScrollRect.verticalNormalizedPosition = 0f;
        historyScrollRect.verticalNormalizedPosition = 0f;
    }

    /**
     *  Moves n items from list1 to list2
     */
    void MoveListRange (int count, List<FeedData> list1, List<FeedData> list2)
    {
        //Debug.Log ("MoveListRange() [1] " + list1.ToString () + " > " + list2.ToString ());

        // return if no events in list1 to move
        if (list1.Count < 1) return;

        // update count if it exceeds number items in list1
        if (count > list1.Count) count = list1.Count;

        // copy range from list1 > list2
        list2.AddRange (list1.GetRange (0, count));

        // delete the range in list1
        list1.RemoveRange (0, count);
    }



}





