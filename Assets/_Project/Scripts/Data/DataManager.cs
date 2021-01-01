using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine.UI;
using TMPro;

public class DataManager : Singleton<DataManager> {
    // singleton
    protected DataManager () { }
    //public static new DataManager Instance;


    // singleton
    private static DataManager dataManager;
    public static DataManager instance {
        get {
            // if we don't have it
            if (!dataManager) {
                // then find it 
                dataManager = FindObjectOfType (typeof (DataManager)) as DataManager;
                // if null
                if (!dataManager) {
                    Debug.LogError ("There needs to be one active DataManager script on a GameObject in your scene.");
                } else {
                    // initialize 
                    //dataManager.Init ();
                }
            }
            return dataManager;
        }
    }



    // listeners 
    void OnEnable ()
    {
        EventManager.StartListening ("GetNewData", GetNewData);
    }
    void OnDisable ()
    {
        EventManager.StopListening ("GetNewData", GetNewData);
    }





    [Space (10)]
    [Header ("MODE")]

    public TMP_Dropdown modeDropdown;

    [Tooltip ("The data source mode")]
    public ModeType selectedMode;

    [Serializable]
    public enum ModeType {
        remoteLive, // remote - refresh with new data from server - the endpoint automatic
        remoteArchive, // remote - make only one data request, shuffle between buffer / history 
        localArchive // same as remoteArchive except using local - the endpoint is disabled
    }




    [Space (10)]
    [Header ("HOST + ENDPOINT")]

    [Tooltip ("The selected host (users cannot choose)")]
    public HostType selectedHost;

    [Serializable]
    public enum HostType {
        remote,
        local
    }
    string [] hosts = {
        "https://tallysavestheinternet.com/api/feed/range/plusStream/",
        "https://127.0.0.1:5000/api/feed/range/plusStream/"
    };





    // ENDPOINT

    // 20 recent - includes only game objects, no clicks / streams

    public TMP_Dropdown endpointDropdown;

    [Tooltip ("The selected endpoint")]
    public EndpointType selectedEndpoint;

    // all the endpoints - these should match the API downloads...
    [Serializable]
    public enum EndpointType {
        minute_1,   // 1 minute...
        minute_2,   // 2
        minute_5,   // 5
        minute_10,  // 10
        minute_30,  // 30
        hour_1,     // 60
        hour_2,     // 120
        hour_6,     // 360
        hour_12,    // 720
        day_1,      // 1440
        day_2,      // 2880
        day_4,      // 5760
        week_1      // 10080
    }

    // full API host + endpoint
    public string path;

    public TextAsset localDataFile;
    // array of local files
    public TextAsset [] localDataFiles;




    // return the unit + sample size
    public string [] GetUnitAndSampleSize ()
    {
        char [] delimiterChars = { '_', ' ', ',', '.', ':', '\t' };
        string [] unitSampleSize = selectedEndpoint.ToString ().Split (delimiterChars);
        return unitSampleSize;
    }

    // return the string from a local file
    public string GetLocalFileString ()
    {
        // get unit (e.g. "minute") and sample size (e.g. "5")
        string [] unitSampleArr = GetUnitAndSampleSize ();
        // make a string to look for filename
        string unitSampleStr = unitSampleArr [0] + "-" + unitSampleArr [1];
        // loop through array (drag all files into inspector)
        for (int i = 0; i < localDataFiles.Length; i++) {
            //Debug.Log (localDataFiles [i].name);

            // if name is contained then return
            if (localDataFiles [i].name.Contains (unitSampleStr)) {
                return localDataFiles [i].text;
            }
        }
        return "[]";
    }

    // return the path + endpoint
    public string GetRemoteEndpoint ()
    {
        string [] unitSampleArr = GetUnitAndSampleSize ();
        return hosts [(int)selectedHost] + "" + unitSampleArr [1] + "/" + unitSampleArr [0];
    }



    public int sizeOfDataRequests = 1;
    public int sizeOfDataRequestsMax = 10;




    // META


    [Space (10)]
    [Header ("RETURNED DATA")]

    [Tooltip ("The number of events returned in this call")]
    public int receivedTotal;
    [Tooltip ("The number of events returned that were new (not duplicates)")]
    public int receivedNew;
    [Tooltip ("The number of events returned that were duplicates")]
    public int receivedDuplicates;

    [Serializable]
    public enum DataRequestStatus {
        requesting,
        receiving,
        handling,
        success,
        noDataReceived
    }
    [Tooltip ("The current status")]
    public DataRequestStatus dataRequestStatus;

    [Tooltip ("The current event being handled in the loop")]
    public string currentEventStr;

    [Tooltip ("The list of items returned")]
    public IList<FeedData> feeds = new List<FeedData> ();









    /**
     *  Increase or decrease how much data we are trying to get - only called in live mode
     */
    public void ScaleSizeOfDataRequests (int scale)
    {

        return; // going to let app user control this now 


        //// decrease
        //if (scale == -1) {

        //}
        //// increase
        //else if (scale == 1) {

        //}


        //if (sizeOfDataRequests < 1) {
        //    sizeOfDataRequests = 1;
        //} else if (sizeOfDataRequests > sizeOfDataRequestsMax) {
        //    sizeOfDataRequests = sizeOfDataRequestsMax;
        //}


        //// potentially change this var
        ////Timeline.Instance.bufferCountMax = n

    }





    private void Awake ()
    {
        // populate dropdown options
        PopulateDropdown (modeDropdown, new ModeType ());
        PopulateDropdown (endpointDropdown, new EndpointType ());

        // get inspector values
        modeDropdown.value = (int)selectedMode;
        endpointDropdown.value = (int)selectedEndpoint;
    }

    /// <summary>
    /// Populate TMP dropdown options
    /// </summary>
    /// <param name="dropdown">A reference to a TMP_Dropdown</param>
    /// <param name="e">Use like this: "new Enum()"</param>
    void PopulateDropdown (TMP_Dropdown dropdown, Enum e)
    {
        // clear the options in the dropdown
        dropdown.ClearOptions ();
        // save the enum options as a string[]
        string [] enumOptions = Enum.GetNames (e.GetType ());
        // create a new list for the available options
        List<string> options = new List<string> (enumOptions);
        // add options to list
        dropdown.AddOptions (options);
    }

    /**
     *  Called from UI to update the data source
     */
    public void OnChangeModeDropdown (int _status)
    {
        //Debug.Log ("OnChangeModeDropdown() _status = " + _status);
        // cast as Enum
        selectedMode = (ModeType)_status;
    }
    /**
     *  Called from UI to update endpoint
     */
    public void OnChangeEndpointDropdown (int _status)
    {
        //Debug.Log ("OnChangeEndpointDropdown() _status = " + _status);
        // cast as Enum
        selectedEndpoint = (EndpointType)_status;
    }


    private void Start ()
    {
        // start everything - now called from Timeline
        //GetNewData ();
    }

    public void EnableEndpointDropdown (bool _status)
    {
        modeDropdown.interactable = _status;
        endpointDropdown.interactable = _status;
    }


    public void GetNewData ()
    {
        //Debug.Log ("DataManager.GetNewData()");

        // disable dropdown temporarily
        EnableEndpointDropdown (false);

        // reset stats
        receivedTotal = 0;
        receivedNew = 0;
        receivedDuplicates = 0;

        // set status
        dataRequestStatus = DataRequestStatus.requesting;
        // trigger data updated event
        EventManager.TriggerEvent ("DataManagerUpdated");

        // if using local archive file
        if (selectedMode == ModeType.localArchive) {

            // jump straight to handle archived json
            //HandleJsonResponse (localDataFile.text);
            HandleJsonResponse (GetLocalFileString ());

        } else {

            // update path
            path = GetRemoteEndpoint ();

            //Debug.Log (DebugManager.GetSymbol ("asterisk") + " DataManager.GetNewData() path = " + path);
            //DebugManager.Instance.UpdateDisplay ("DataManager.GetNewData() path = " + path);

            StartCoroutine (GetRequest (path));

            // error test
            // StartCoroutine(GetRequest("https://error.html"));

        }
    }


    // do a get request for JSON data at url
    public IEnumerator GetRequest (string uri)
    {
        // wait a second 
        yield return new WaitForSeconds (1.0f);

        using (UnityWebRequest webRequest = UnityWebRequest.Get (uri)) {

            //DebugManager.Instance.UpdateDisplay ("DataManager.GetRequest() uri = " + uri);

            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest ();

            //string [] pages = uri.Split ('/');
            //int page = pages.Length - 1;

            if (webRequest.isNetworkError || webRequest.isHttpError) {
                Debug.Log (DebugManager.GetSymbol ("asterisk") + " Error: " + webRequest.error);
            } else {
                //Debug.Log ("DataManager.GetRequest() " + webRequest.downloadHandler.text);

                // handle the response
                HandleJsonResponse (webRequest.downloadHandler.text);
            }
        }
    }



    /// <summary>
    /// Handle JSON data 
    /// </summary>
    /// <param name="text">JSON data as string</param>
    public void HandleJsonResponse (string text)
    {
        //Debug.Log ("HandleJsonResponse() text.Length = " + text.Length);

        // set status
        dataRequestStatus = DataRequestStatus.receiving;

        // parse JSON array 
        JArray a = JArray.Parse (text);

        // update count
        receivedTotal = a.Count;
        dataRequestStatus = DataRequestStatus.handling;

        // no data received
        if (receivedTotal == 0 || text == "[]" || text == "") {
            //Debug.Log ("HandleJsonResponse() receivedTotal = " + receivedTotal);

            // stop / display timeline 
            Timeline.Instance.waitingForDataProgress = -1;

            // set status
            dataRequestStatus = DataRequestStatus.noDataReceived;
            // trigger data updated event
            EventManager.TriggerEvent ("DataRequestFinished");

            return;
        }

        DebugManager.Instance.UpdateDisplay ("DataManager.HandleJsonResponse() dataRequestStatus = " + dataRequestStatus);

        // loop through array and add each 
        foreach (JObject item in a) {

            // base class properties
            string _username = item.GetValue ("username").ToString ();
            string _avatarPath = item.GetValue ("avatarPath").ToString ();

            int _level = (int)item.GetValue ("level");
            int _clicks = (int)item.GetValue ("clicks");
            int _score = (int)item.GetValue ("score");
            int _time = (int)item.GetValue ("time");
            int _capturedTotal = (int)item.GetValue ("capturedTotal");
            int _missedTotal = (int)item.GetValue ("missedTotal");
            int _pageActionScrollDistance = (int)item.GetValue ("pageActionScrollDistance");
            int _trackersBlocked = (int)item.GetValue ("trackersBlocked");
            int _trackersSeen = (int)item.GetValue ("trackersSeen");

            string _eventType = item.GetValue ("eventType").ToString ();
            string _createdAtStr = item.GetValue ("createdAt").ToString ();
            string _monsters = item.GetValue ("monsters").ToString ();
            string _trackers = item.GetValue ("trackers").ToString ();

            // parse string to ISO 8601 format
            DateTime _createdAt = DateTime.Parse (_createdAtStr, null, System.Globalization.DateTimeStyles.RoundtripKind);




            // LIVE MODE ONLY - SKIP DUPLICATES

            if (selectedMode == ModeType.remoteLive) {
                // get any duplicate dates in buffer based on both conditions
                var bufferMatches = Timeline.Instance.buffer.FindAll (found => found.createdAt == _createdAt);
                var historyMatches = Timeline.Instance.history.FindAll (found => found.createdAt == _createdAt);

                // skip this iteration
                if (bufferMatches.Count > 0 || historyMatches.Count > 0) {
                    Debug.Log ("DUPLICATE createdAt = " + _createdAt + " AND eventType = " + _eventType);
                    receivedDuplicates++;
                    continue;
                }
            }






            // IF NOT A DUPLICATE THEN PROCEED

            receivedNew++;

            // parse eventData 
            JObject d = JObject.Parse (item.GetValue ("eventData").ToString ());

            // object to hold data
            FeedData output;

            if (_eventType == "attack") {
                output = new AttackData {
                    _name = (string)d ["name"],
                    _type = (string)d ["level"],
                    _selected = (bool)d ["selected"]
                };
            } else if (_eventType == "badge") {
                output = new BadgeData {
                    _name = (string)d ["name"],
                    _level = (int)d ["level"]
                };
            } else if (_eventType == "consumable") {
                output = new ConsumableData {
                    _name = (string)d ["name"],
                    _slug = (string)d ["slug"],
                    _stat = (string)d ["stat"],
                    _type = (string)d ["type"],
                    _value = (int)d ["value"]
                };
            } else if (_eventType == "disguise") {
                output = new DisguiseData {
                    _name = (string)d ["name"],
                    _type = (string)d ["type"]
                };
            } else if (_eventType == "monster") {
                output = new MonsterData {
                    _mid = (int)d ["mid"],
                    _level = (int)d ["level"],
                    _captured = (int)d ["captured"],
                };
            } else if (_eventType == "tracker") {
                output = new TrackerData {
                    _tracker = (string)d ["tracker"],
                    _captured = (int)d ["captured"],
                };
            } else { // if (_eventType == "stream")
                output = new StreamData {
                    _score = (int)d ["score"],
                    _clicks = (int)d ["clicks"],
                    _likes = (int)d ["likes"],
                };
            }

            output.username = _username;
            output.avatarPath = _avatarPath;

            output.level = _level;
            output.clicks = _clicks;
            output.score = _score;
            output.time = _time;
            output.capturedTotal = _capturedTotal;
            output.missedTotal = _missedTotal;
            output.pageActionScrollDistance = _pageActionScrollDistance;
            output.trackersBlocked = _trackersBlocked;
            output.trackersSeen = _trackersSeen;
            output.clicks = _clicks;

            output.eventType = _eventType;
            output.createdAt = _createdAt;
            output.monsters = _monsters;
            output.trackers = _trackers;



            // add to feeds - now adding to buffer in Timeline
            //feeds.Add (output);




            // if live mode == true

            // then attempt to add to buffer
            Timeline.Instance.buffer.Add (output);


            //Debug.Log ("Added " + _username + " event " + _eventType + " at " + _createdAt);
        }



        // set status
        dataRequestStatus = DataRequestStatus.success;
        // trigger data updated event
        EventManager.TriggerEvent ("DataRequestFinished");
    }



}
