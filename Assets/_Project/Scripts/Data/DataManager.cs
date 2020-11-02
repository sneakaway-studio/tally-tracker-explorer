using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System;
using System.Linq;

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
    [Header ("HOST & ENDPOINT")]

    // HOST

    public HostType chosenHost;

    [Serializable]
    public enum HostType {
        local,
        remote
    }
    string [] hosts = {
        "https://127.0.0.1:5000/api/",
        "https://tallysavestheinternet.com/api/"
    };

    // ENDPOINT

    [Serializable]
    public enum EndpointType {
        recent20,
        rangeOneWeek,
        rangePlusStreamOneDay,
        rangePlusStream12Hour,
        rangePlusStream6Hour,
        rangePlusStream3Hour,
        rangePlusStream1Hour,
        rangePlusStream30Minute,
        rangePlusStream5Minute,
        rangePlusStream1Minute
    }
    public EndpointType chosenEndpoint;
    string [] endpoints = {
        "feed/recent", // 20 recent - includes only game objects, no clicks / streams
        "feed/range/1/week",
        "feed/range/plusStream/1/day",
        "feed/range/plusStream/12/hour",
        "feed/range/plusStream/6/hour",
        "feed/range/plusStream/3/hour",
        "feed/range/plusStream/1/hour",
        "feed/range/plusStream/30/minute",
        "feed/range/plusStream/5/minute",
        "feed/range/plusStream/1/minute"
    };

    // chosen host and endpoint for API
    public string path;



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
        requestData,
        handleResponse,
        requestComplete
    }
    [Tooltip ("The current status")]
    public DataRequestStatus dataRequestStatus;

    [Tooltip ("The current event being handled in the loop")]
    public string currentEventStr;

    [Tooltip ("The list of items returned")]
    public IList<FeedData> feeds = new List<FeedData> ();





    private void Start ()
    {
        // start everything - now called from Timeline
        //GetNewData ();
    }


    public void GetNewData ()
    {
        // update path
        path = hosts [(int)chosenHost] + endpoints [(int)chosenEndpoint];

        //Debug.Log (DebugManager.GetSymbol ("asterisk") + " DataManager.GetNewData() path = " + path);
        //DebugManager.Instance.UpdateDisplay ("DataManager.GetNewData() path = " + path);

        StartCoroutine (GetRequest (path));

        // error test
        // StartCoroutine(GetRequest("https://error.html"));
    }

    // do a get request for JSON data at url
    public IEnumerator GetRequest (string uri)
    {
        // reset stats
        receivedTotal = 0;
        receivedNew = 0;
        receivedDuplicates = 0;
        dataRequestStatus = DataRequestStatus.requestData;

        using (UnityWebRequest webRequest = UnityWebRequest.Get (uri)) {

            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest ();

            //string [] pages = uri.Split ('/');
            //int page = pages.Length - 1;

            if (webRequest.isNetworkError || webRequest.isHttpError) {
                Debug.Log (DebugManager.GetSymbol ("asterisk") + " Error: " + webRequest.error);
            } else {
                //Debug.Log (DebugManager.GetSymbol ("asterisk") + " DataManager.GetNewData() " + uri + "\n" + webRequest.downloadHandler.text);
                DebugManager.Instance.UpdateDisplay ("DataManager.GetNewData() uri = " + uri);

                // parse JSON array 
                JArray a = JArray.Parse (webRequest.downloadHandler.text);

                // update count
                receivedTotal = a.Count;
                dataRequestStatus = DataRequestStatus.handleResponse;

                DebugManager.Instance.UpdateDisplay ("DataManager.GetNewData() uri = " + uri);

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






                    // SKIP IF DUPLICATE

                    // get any duplicate dates in buffer based on both conditions
                    var results = Timeline.Instance.buffer.FindAll (found => (found.createdAt == _createdAt) && (found.eventType == _eventType));

                    // skip this iteration
                    if (results.Count > 0) {
                        Debug.Log ("Duplicate");
                        receivedDuplicates++;
                        continue;
                    }




                    // IF NOT A DUPLICATE THEN PROCEED

                    receivedNew++;

                    //Debug.Log ("Not a duplicate!");

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

                    //Debug.Log(_eventType);
                }



                // set status
                dataRequestStatus = DataRequestStatus.requestComplete;

                // trigger data updated event
                EventManager.TriggerEvent ("DataRequestFinished");

            }
        }
    }


}
