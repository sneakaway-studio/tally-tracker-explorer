using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

/**
*  Timeline class
*   - new data from server or filesystem is placed in the buffer
*   - on play, each event is taken from the buffer, visualized, then placed in the history
*   - if buffer gets low then we attempt to fill it either with new data or by repeating events in history
*/
public class Timeline : Singleton<Timeline> {
    // singleton
    protected Timeline () { }
    //public static new Timeline Instance;


    // listeners 
    void OnEnable ()
    {
        EventManager.StartListening ("DataRequestFinished", OnDataRequestFinished);
    }
    void OnDisable ()
    {
        EventManager.StopListening ("DataRequestFinished", OnDataRequestFinished);
    }




    // REFERENCES INSIDE PREFABS

    [Space (10)]
    [Header ("REFERENCES IN PREFABS")]


    public TMP_Text waitingForDataProgressText;
    public TMP_Text timelineStatusText;
    public TMP_Text timelineVizDateTimeText;
    public Button startButton;
    public TMP_Text startButtonText;


    // MAIN / CONTROLS

    [Space (10)]
    [Header ("MAIN")]


    [Tooltip ("Timeline status")]
    public TimelineStatus status;

    [Serializable]
    public enum TimelineStatus {
        init,           // firstrun
        start,          // start everything from stopped
        stop,           // stop everything
        active,         // display / logic only - everything is running, loops managing their own data
        inactive,       // display / logic only - everything is off
        getNewData,     // display / logic only - get new data from server
        refreshData,    // display / logic only - refresh data from server
        waitingForData, // display / logic only - holding pattern, waiting for data
        noDataReceived, // display / logic only - was waiting for data, but none received
        newDataReceived,// display / logic only - called after data received or updated
        moveHistory,    // display / logic only - moving history to buffer
    }

    [Tooltip ("Set true after done waiting")]
    public bool waitingForDataFinished;

    [Tooltip ("Time since a data request made active")]
    public int waitingForDataProgress;

    [Tooltip ("Time to wait for data request")]
    public int waitingForDataStartTime = 20;

    public int totalEventCount;

    public string debugLogStr;


    // BUFFER

    [Space (10)]
    [Header ("BUFFER")]

    public List<FeedData> buffer = new List<FeedData> ();

    Coroutine bufferCoroutine;

    [Tooltip ("Current number of feed events in buffer")]
    public int bufferCount;

    [Tooltip ("Min items allowed in buffer")]
    [Range (1, 20)]
    public int bufferCountMin;
    [Tooltip ("Max items allowed in buffer")]
    [Range (10, 10000)]
    public int bufferCountMax;

    [Tooltip ("How often the buffer is checked (in seconds)")]
    public int bufferCheckFrequency = 2;

    // these are in the feed (now disabled)
    public TMP_Text bufferText;
    public ScrollRect bufferScrollRect;
    public TMP_Text bufferTitleText;





    // HISTORY 

    [Space (10)]
    [Header ("HISTORY")]

    Coroutine historyCoroutine;

    public List<FeedData> history = new List<FeedData> ();

    [Tooltip ("Current number of feed events in history")]
    public int historyCount;

    [Tooltip ("Max items allowed in history")]
    [Range (10, 10000)]
    public int historyCountMax;

    // these are in the feed (now disabled)
    public TMP_Text historyTitleText;
    public TMP_Text historyText;
    public ScrollRect historyScrollRect;




    // TIME

    [Space (10)]
    [Header ("TIME")]

    [Tooltip ("DateTime of previously displayed event")]
    [SerializeField]
    DateTime previousTime;

    [Tooltip ("Difference (seconds) between current time (feed.createdAt) and previousTime")]
    public int timeDiff;

    [Tooltip ("Difference (seconds) adjusted")]
    public float timeDiffScaled;

    [Tooltip ("How much faster time is replayed (timeDiff * scalar)")]
    [Range (0f, 1f)]
    public float timeDiffScalar = 0.01f;

    [Tooltip ("Min time allowed between events")]
    public float minTimeDiff = 1;
    [Tooltip ("Max time allowed between events")]
    public float maxTimeDiff = 10;





    private void Start ()
    {
        StartBufferLoop ();
    }



    /////////////////////////////////////////////////////////////
    /////////////////////////// UI //////////////////////////////
    /////////////////////////////////////////////////////////////




    /**
     *  Called from the UI button to start / stop
     */
    public void OnStartBtnClick ()
    {
        // immediately disable both 
        SetStartBtnText (" --- ", false);
        DataManager.Instance.EnableEndpointDropdown (false);

        // if currently active (user has clicked "stop")
        if (status == TimelineStatus.active) {
            SetStartBtnText (" --- ", false);
            // stop everything
            StopBufferLoop ();
            StopHistoryLoop ();
            // set status
            SetTimelineStatus (TimelineStatus.inactive);
            // update btn text and make interactable
            SetStartBtnText ("Start", true);
            // should let them select
            DataManager.Instance.EnableEndpointDropdown (true);
            // if there are players active
            if (PlayerManager.Instance.playerCount > 0) {
                // then add them
                EventManager.TriggerEvent ("RemoveAllPlayers");
            }
        }
        // if not active (user clicked "Start")
        else {
            // set status
            SetTimelineStatus (TimelineStatus.start);
            // start loop
            StartBufferLoop ();
        }
        UpdateCounts ();
        UpdateTimelineLogs ();
    }

    void SetStartBtnText (string txt, bool interact)
    {
        startButtonText.text = txt;
        startButton.interactable = interact;
    }




    /////////////////////////////////////////////////////////////
    //////////////////////// CONTROLS ///////////////////////////
    /////////////////////////////////////////////////////////////


    /**
     *  Called from game and buttons to update the status 
     */
    public void SetTimelineStatus (TimelineStatus _status, bool fromUI = false)
    {
        //Debug.Log ("Timeline.SetTimelineStatus() status = " + status + ", _status = " + _status);

        // update status var
        status = _status;

        // if the call (note, original status) came from within the game then show in UI
        if (!fromUI) timelineStatusText.text = _status.ToString ();
    }

    /**
     *  (stops and then) starts the buffer loop
     */
    public void StartBufferLoop ()
    {
        //Debug.Log ("Timeline.StartBufferLoop()");

        // if coroutine running
        if (bufferCoroutine != null) StopCoroutine (bufferCoroutine);
        // start buffer, attempt to get new data
        bufferCoroutine = StartCoroutine (BufferLoop ());
    }
    /**
     *  Stop fetching new data
     */
    public void StopBufferLoop ()
    {
        //Debug.Log ("Timeline.StopBufferLoop()");

        // if coroutine running
        if (bufferCoroutine != null) StopCoroutine (bufferCoroutine);

        // clear and reset capacity
        buffer.Clear ();
        buffer.TrimExcess ();
    }

    /**
     *  (stops and then) starts the history loop
     */
    public void StartHistoryLoop ()
    {
        //Debug.Log ("Timeline.StartHistoryLoop()");

        // if coroutine running
        if (historyCoroutine != null) StopCoroutine (historyCoroutine);
        // start buffer, attempt to get new data
        historyCoroutine = StartCoroutine (HistoryLoop ());
    }
    /**
     *  Stop displaying events
     */
    public void StopHistoryLoop ()
    {
        //Debug.Log ("Timeline.StopHistoryLoop()");

        // if coroutine running
        if (historyCoroutine != null) StopCoroutine (historyCoroutine);

        // reset previous time
        previousTime = DateTime.MinValue;

        // clear and reset capacity
        history.Clear ();
        history.TrimExcess ();
    }

    /**
     *  Called from DataManager once request is finished
     */
    void OnDataRequestFinished ()
    {
        // was new data found?
        if (DataManager.Instance.receivedNew > 0) {

        }
        waitingForDataFinished = true;
    }

    void UpdateWaitingProgress (int _status)
    {
        //Debug.Log ("UpdateWaitingProgress() _status = " + _status);

        // restart
        if (_status == 0) {
            // mark finished flag false
            waitingForDataFinished = false;
            // reset counter
            waitingForDataProgress = waitingForDataStartTime;
            // show status
            waitingForDataProgressText.text = " -- ";
            // disable button until data arrives
            SetStartBtnText (" --- ", false);
            // disable
            DataManager.Instance.EnableEndpointDropdown (false);
        }
        // still waiting 
        else if (_status == 1) {
            // time since request
            waitingForDataProgress--;
        }
        // finished - new data arrived 
        else if (_status == 2) {
            // mark finished flag true
            waitingForDataFinished = true;

            //// reset counter
            //waitingForDataProgress = waitingForDataStartTime;
            //// show status
            //waitingForDataProgressText.text = " -- ";

            UpdateCounts ();
            // update btn text and disable
            SetStartBtnText ("Stop", true);
            // disable
            DataManager.Instance.EnableEndpointDropdown (false);

        }
        // finished - fail - NO DATA
        else if (_status == 3) {
            // mark finished flag true
            waitingForDataFinished = true;
            UpdateCounts ();
            // update btn text and make interactable
            SetStartBtnText ("START", true);
            // should let them select
            DataManager.Instance.EnableEndpointDropdown (true);
        }


        // display progress
        waitingForDataProgressText.text = waitingForDataProgress.ToString ();

    }




    /////////////////////////////////////////////////////////////
    ///////////////////// BUFFER LOOP ///////////////////////////
    /////////////////////////////////////////////////////////////


    /**
     *  Check the buffer, add data
     */
    IEnumerator BufferLoop ()
    {
        while (true) {

            // update count
            UpdateCounts ();

            //Debug.Log ("Timeline.BufferLoop()");
            debugLogStr = "Timeline.BufferLoop() status = " + status + ", bufferCount = " + bufferCount.ToString ();



            // ---- MAIN CONTROL ----


            // INIT - firstrun -> start

            if (status == TimelineStatus.init) {
                SetTimelineStatus (TimelineStatus.start);
            }

            // STOP - not actually called - placeholder

            else if (status == TimelineStatus.stop) {
                // clear buffer
                buffer.Clear ();
                buffer.TrimExcess ();
                // set to waiting
                SetTimelineStatus (TimelineStatus.inactive);
            }

            // START - called from init and button - let active handle logic -> active

            if (status == TimelineStatus.start) {
                SetTimelineStatus (TimelineStatus.active);
            }





            // ---- MAIN LOGIC ----


            // ACTIVE

            else if (status == TimelineStatus.active) {

                // set by start on first run or stop and start - buffer and history are both empty

                if (bufferCount <= 0 && historyCount <= 0) {
                    // get new data
                    SetTimelineStatus (TimelineStatus.getNewData);
                }

                // buffer empty, history not empty

                else if (bufferCount <= 0 && historyCount > 0) {
                    //Debug.Log ("bufferCount <= 0 && historyCount > 0");

                    // LIVE - we should have received new data by now
                    if (DataManager.Instance.selectedMode == DataManager.ModeType.remoteLive) {

                        // increase size of requests
                        //DataManager.Instance.ScaleSizeOfDataRequests (1); // going to let app user control this now

                        // attempt to get new data 
                        SetTimelineStatus (TimelineStatus.refreshData);
                    }
                    // ARCHIVE (remote or local data) 
                    else {
                        // we are using prepackaged data archive so move history back to buffer
                        SetTimelineStatus (TimelineStatus.moveHistory);
                    }
                }


                // LIVE ONLY


                // buffer almost empty

                else if (bufferCount <= bufferCountMin) {
                    //Debug.Log ("bufferCount <= bufferCountMin");

                    // LIVE
                    if (DataManager.Instance.selectedMode == DataManager.ModeType.remoteLive) {
                        // attempt to get new data 
                        SetTimelineStatus (TimelineStatus.refreshData);
                    }
                    // ARCHIVE (remote or local data) - do nothing - this is handled above
                }

                // buffer too full

                else if (bufferCount > bufferCountMax) {
                    //Debug.Log ("bufferCount > bufferCountMax");

                    // LIVE
                    if (DataManager.Instance.selectedMode == DataManager.ModeType.remoteLive) {
                        // scale down size of requests
                    }
                    // ARCHIVE (remote or local data) - do nothing - we can handle large files (?)
                }

                // history too full

                else if (historyCount > historyCountMax) {
                    //Debug.Log ("historyCount > historyCountMax");

                    // LIVE
                    if (DataManager.Instance.selectedMode == DataManager.ModeType.remoteLive) {
                        // scale down size of history
                    }
                    // ARCHIVE (remote or local data) - do nothing - we can handle large files (?)

                }

            }






            // ---- DATA CALLBACKS ----


            // GET NEW DATA - first time

            else if (status == TimelineStatus.getNewData) {

                // set to waiting 
                UpdateWaitingProgress (0);

                // attempt to get new data from server,
                EventManager.TriggerEvent ("GetNewData");

                // start waiting
                SetTimelineStatus (TimelineStatus.waitingForData);
            }

            // REFRESH DATA - if buffer is low or empty

            else if (status == TimelineStatus.refreshData) {

                // set to waiting
                UpdateWaitingProgress (0);

                // LIVE DATA - prompt DataManager
                if (DataManager.Instance.selectedMode == DataManager.ModeType.remoteLive) {

                    // attempt to get new data from server,
                    EventManager.TriggerEvent ("GetNewData");

                    // start waiting
                    SetTimelineStatus (TimelineStatus.waitingForData);

                } else {
                    // we are using prepackaged data archive so move history back to buffer
                    SetTimelineStatus (TimelineStatus.moveHistory);
                }

            }

            // WAITING FOR DATA

            else if (status == TimelineStatus.waitingForData) {

                // continue countdown
                UpdateWaitingProgress (1);

                debugLogStr = "Timeline.BufferLoop() status = " + status + ", waitingForDataProgress = " + waitingForDataProgress.ToString ();

                // buffer WAS waiting, now has data
                if (bufferCount > 0 && waitingForDataFinished) {
                    // set status
                    SetTimelineStatus (TimelineStatus.newDataReceived);
                }
                // countdown exceeded
                else if (waitingForDataProgress <= 0) {
                    // set status
                    SetTimelineStatus (TimelineStatus.noDataReceived);
                }
            }

            // NO DATA RECEIVED

            else if (status == TimelineStatus.noDataReceived) {

                // buffer WAS waiting, no data returned - increase size of requests
                //DataManager.Instance.ScaleSizeOfDataRequests (1); // going to let app user control this now 

                // set finished - fail 
                UpdateWaitingProgress (3);

                //debugLogStr = "Timeline.BufferLoop() status = " + status + ", waitingForDataProgress = " + waitingForDataProgress.ToString ();

                // try to get data again
                //SetTimelineStatus (TimelineStatus.start); // going to let app user control this now

                SetTimelineStatus (TimelineStatus.stop); // going to let app user control this now
            }


            // NEW DATA RECEIVED

            else if (status == TimelineStatus.newDataReceived) {

                // set finished - success 
                UpdateWaitingProgress (2);

                // after new data, sort ascending
                buffer.Sort ((x, y) => x.createdAt.CompareTo (y.createdAt));


                // if we have data in the buffer or history but no players 
                if (PlayerManager.Instance.playerCount < 1) {
                    // then add them
                    EventManager.TriggerEvent ("AddAllPlayers");
                } else {
                    // otherwise update them
                    EventManager.TriggerEvent ("CheckUpdatePlayers");
                }

                // start history loop again
                StartHistoryLoop ();

                // set status
                SetTimelineStatus (TimelineStatus.active);
            }

            // MOVE HISTORY (TO BUFFER)

            else if (status == TimelineStatus.moveHistory) {

                // move history back to buffer (assuming we are doing this with one big file)
                MoveListRange (bufferCountMax, history, buffer);
                // set to handle end of buffer
                SetTimelineStatus (TimelineStatus.active);
            }



            // update counts
            UpdateCounts ();
            // after checking condition
            if (totalEventCount > 0) {
                // display
                UpdateTimelineLogs ();
            }


            // trigger DataDisplay
            EventManager.TriggerEvent ("TimelineUpdated");


            // GARBAGE!!!
            //DebugManager.Instance.UpdateDisplay (debugLogStr);

            yield return new WaitForSeconds (bufferCheckFrequency);
        }
    }



    /////////////////////////////////////////////////////////////
    /////////////////// HISTORY LOOP ////////////////////////////
    /////////////////////////////////////////////////////////////


    /**
     *  Play events from buffer and move to history
     */
    IEnumerator HistoryLoop ()
    {
        while (true) {

            // update count
            UpdateCounts ();

            //Debug.Log ("Timeline.HistoryLoop()");
            //DebugManager.Instance.UpdateDisplay ("Timeline.HistoryLoop() historyCount = " + historyCount.ToString ());

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

                // show date in TimelineViz
                timelineVizDateTimeText.text = feed.createdAt.ToString ();

                // MANAGE EVENT

                // move event to history
                MoveListRange (1, buffer, history);



                UpdateCounts ();

                // after 
                if (historyCount > 0) {
                    // after update, sort ascending
                    history.Sort ((x, y) => x.createdAt.CompareTo (y.createdAt));
                }
                // display
                UpdateTimelineLogs ();


                // log feed item
                var eventString = timeDiff + " (" + timeDiffScaled + ") " +
                " (" + feed.createdAt + ") " +
                //" = (" + previousTime + " - " + feed.createdAt + ") " +
                feed.username + ", " + feed.eventType + "";

                DebugManager.Instance.UpdateDisplay ("Timeline.HistoryLoop() " + eventString);


            } else {
                // for safety and a pause between data sets
                timeDiffScaled = 3;
            }

            // time difference to next event (or safety)
            yield return new WaitForSeconds (timeDiffScaled);
        }
    }



    /////////////////////////////////////////////////////////////
    ////////////////////// GENERAL //////////////////////////////
    /////////////////////////////////////////////////////////////


    /**
     *  Update display for both history and buffer
     */
    void UpdateTimelineLogs ()
    {
        //Debug.Log ("Timeline.UpdateTimelineLogs()");


        // THE BELOW (DEBUGGING MAINLY) ADDS A LOT OF GARBAGE FOR THE COLLECTOR !!!!
        return;

        //string bufferString = "";
        //string historyString = "";

        //int safety = 0;
        //foreach (var feed in buffer) {
        //    bufferString += feed.eventType + ". " + feed.createdAt + " - " + feed.username + "<br>";
        //    if (++safety > bufferCount || safety > totalEventCount) {
        //        Debug.Log ("Safety first!");
        //        break;
        //    }
        //}
        //safety = 0;
        //foreach (var feed in history) {
        //    historyString += feed.eventType + ". " + feed.createdAt + " - " + feed.username + "<br>";
        //    if (++safety > historyCount || safety > totalEventCount) {
        //        Debug.Log ("Safety first!");
        //        break;
        //    }
        //}

        //bufferText.text = bufferString;
        //historyText.text = historyString;

        //UpdateScroll ();

        //// in the feed
        //bufferTitleText.text = "Buffer [ " + bufferCount + " ] ";
        //historyTitleText.text = "History [ " + historyCount + " ] ";

        //// in the control panel
        //bufferTitleText.text = bufferCount.ToString ();
        //historyTitleText.text = historyCount.ToString ();

    }




    /**
     *  Update counts of both history and buffer - does not actually change display 
     */
    void UpdateCounts ()
    {
        bufferCount = buffer.Count;
        historyCount = history.Count;
        totalEventCount = buffer.Count + history.Count;
    }

    public void UpdateScroll ()
    {
        // make the canvases update their positions - causes big performance spikes and is not needed for debugging
        //Canvas.ForceUpdateCanvases ();
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





