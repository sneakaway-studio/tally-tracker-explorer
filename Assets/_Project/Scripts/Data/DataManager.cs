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

    [Serializable]
    public enum EndpointType {
        minute_1,
        minute_2,
        minute_3,
        minute_4,
        minute_5,
        minute_10,
        minute_30,
        hour_1,
        hour_2,
        hour_3,
        hour_6,
        hour_12,
        day_1,
        day_2,
        day_3,
        week_1
    }

    // full API host + endpoint
    public string path;

    public TextAsset localDataFile;





    // return the path + endpoint
    public string GetEndpoint ()
    {
        char [] delimiterChars = { '_', ' ', ',', '.', ':', '\t' };
        string [] endpointArr = selectedEndpoint.ToString ().Split (delimiterChars);
        return hosts [(int)selectedHost] + "" + endpointArr [1] + "/" + endpointArr [0];
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
        finished
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
        // decrease
        if (scale == -1) {

        }
        // increase
        else if (scale == 1) {

        }


        if (sizeOfDataRequests < 1) {
            sizeOfDataRequests = 1;
        } else if (sizeOfDataRequests > sizeOfDataRequestsMax) {
            sizeOfDataRequests = sizeOfDataRequestsMax;
        }


        // potentially change this var
        //Timeline.Instance.bufferCountMax = n

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
        Debug.Log ("OnChangeModeDropdown() _status = " + _status);
        // cast as Enum
        selectedMode = (ModeType)_status;
    }
    /**
     *  Called from UI to update endpoint
     */
    public void OnChangeEndpointDropdown (int _status)
    {
        Debug.Log ("OnChangeEndpointDropdown() _status = " + _status);
        // cast as Enum
        selectedEndpoint = (EndpointType)_status;
    }


    private void Start ()
    {
        // start everything - now called from Timeline
        //GetNewData ();
    }


    public void GetNewData ()
    {
        // reset stats
        receivedTotal = 0;
        receivedNew = 0;
        receivedDuplicates = 0;
        // set status
        dataRequestStatus = DataRequestStatus.requesting;

        // if using local archive file
        if (selectedMode == ModeType.localArchive) {

            // jump straight to handle archived json
            HandleJsonResponse (localDataFile.text);

        } else {

            // update path
            path = GetEndpoint ();

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

        using (UnityWebRequest webRequest = UnityWebRequest.Get (uri)) {

            //DebugManager.Instance.UpdateDisplay ("DataManager.GetNewData() uri = " + uri);

            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest ();

            //string [] pages = uri.Split ('/');
            //int page = pages.Length - 1;

            if (webRequest.isNetworkError || webRequest.isHttpError) {
                Debug.Log (DebugManager.GetSymbol ("asterisk") + " Error: " + webRequest.error);
            } else {
                //Debug.Log ("DataManager.GetNewData() " + webRequest.downloadHandler.text);

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
        Debug.Log ("HandleJsonResponse()");

        // set status
        dataRequestStatus = DataRequestStatus.receiving;

        // parse JSON array 
        JArray a = JArray.Parse (text);

        // update count
        receivedTotal = a.Count;
        dataRequestStatus = DataRequestStatus.handling;

        DebugManager.Instance.UpdateDisplay ("DataManager.GetNewData() dataRequestStatus = " + dataRequestStatus);

        // loop through array and add each 
        foreach (JObject item in a) {
            // base class properties
            string _username = item.GetValue ("username").ToString ();
            string _avatarPath = item.GetValue ("avatarPath").ToString ();
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
                    username = _username,
                    avatarPath = _avatarPath,
                    eventType = _eventType,
                    createdAt = _createdAt,
                    monsters = _monsters,
                    trackers = _trackers,

                    name = (string)d ["name"],
                    type = (string)d ["level"],
                    selected = (bool)d ["selected"]
                };
            } else if (_eventType == "badge") {
                output = new BadgeData {
                    username = _username,
                    avatarPath = _avatarPath,
                    eventType = _eventType,
                    createdAt = _createdAt,
                    monsters = _monsters,
                    trackers = _trackers,

                    name = (string)d ["name"],
                    level = (int)d ["level"]
                };
            } else if (_eventType == "consumable") {
                output = new ConsumableData {
                    username = _username,
                    avatarPath = _avatarPath,
                    eventType = _eventType,
                    createdAt = _createdAt,
                    monsters = _monsters,
                    trackers = _trackers,

                    name = (string)d ["name"],
                    slug = (string)d ["slug"],
                    stat = (string)d ["stat"],
                    type = (string)d ["type"],
                    value = (int)d ["value"]
                };
            } else if (_eventType == "disguise") {
                output = new DisguiseData {
                    username = _username,
                    avatarPath = _avatarPath,
                    eventType = _eventType,
                    createdAt = _createdAt,
                    monsters = _monsters,
                    trackers = _trackers,

                    name = (string)d ["name"],
                    type = (string)d ["type"]
                };
            } else if (_eventType == "monster") {
                output = new MonsterData {
                    username = _username,
                    avatarPath = _avatarPath,
                    eventType = _eventType,
                    createdAt = _createdAt,
                    monsters = _monsters,
                    trackers = _trackers,

                    mid = (int)d ["mid"],
                    level = (int)d ["level"],
                    captured = (int)d ["captured"],
                };
            } else if (_eventType == "tracker") {
                output = new TrackerData {
                    username = _username,
                    avatarPath = _avatarPath,
                    eventType = _eventType,
                    createdAt = _createdAt,
                    monsters = _monsters,
                    trackers = _trackers,

                    tracker = (string)d ["tracker"],
                    captured = (int)d ["captured"],
                };
            } else { // if (_eventType == "stream")
                output = new StreamData {
                    username = _username,
                    avatarPath = _avatarPath,
                    eventType = _eventType,
                    createdAt = _createdAt,
                    monsters = _monsters,
                    trackers = _trackers,

                    score = (int)d ["score"],
                    clicks = (int)d ["clicks"],
                    likes = (int)d ["likes"],
                };
            }

            // add to feeds - now adding to buffer in Timeline
            //feeds.Add (output);




            // if live mode == true

            // then attempt to add to buffer
            Timeline.Instance.buffer.Add (output);


            //Debug.Log ("Added " + _username + " event " + _eventType + " at " + _createdAt);
        }



        // set status
        dataRequestStatus = DataRequestStatus.finished;

        // trigger data updated event
        EventManager.TriggerEvent ("DataRequestFinished");

    }
}
