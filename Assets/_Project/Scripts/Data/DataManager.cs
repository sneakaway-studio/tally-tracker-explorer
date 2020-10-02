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
    public static new DataManager Instance;

    // listeners 
    void OnEnable ()
    {
        EventManager.StartListening ("GetNewData", GetNewData);
    }
    void OnDisable ()
    {
        EventManager.StopListening ("GetNewData", GetNewData);
    }





    // HOST

    [Serializable]
    public enum HostType {
        local,
        remote
    }
    public HostType chosenHost;
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
        rangePlusStreamTwelveHour,
        rangePlusStreamSixHour,
        rangePlusStreamOneHour,
        rangePlusStreamFiveMinute
    }
    public EndpointType chosenEndpoint;
    string [] endpoints = {
        "feed/recent",      // 20 recent
        "feed/range/1/week", // 1 week
        "feed/range/plusStream/1/day",  // 1 day
        "feed/range/plusStream/12/hour", // 6 hour
        "feed/range/plusStream/6/hour", // 6 hour
        "feed/range/plusStream/1/hour", // 1 hour
        "feed/range/plusStream/5/minute" // 5 minutes
    };

    // FINAL PATH

    // chosen host and endpoint for API
    public string path;



    // META

    // the number of events
    public static int dataCount;
    // the current data as string
    public static string current;
    // as list
    public static IList<FeedData> feeds = new List<FeedData> ();


    public static Dictionary<string, FeedData> eventDictionary = new Dictionary<string, FeedData> ();



    private void Start ()
    {
        // start everything
        GetNewData ();
    }


    public void GetNewData ()
    {
        // update path
        path = hosts [(int)chosenHost] + endpoints [(int)chosenEndpoint];

        //Debug.Log (DebugManager.GetSymbol ("asterisk") + " DataManager.GetNewData() path = " + path);

        StartCoroutine (GetRequest (path));

        // error test
        // StartCoroutine(GetRequest("https://error.html"));
    }

    // do a get request for JSON data at url
    public static IEnumerator GetRequest (string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get (uri)) {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest ();

            //string [] pages = uri.Split ('/');
            //int page = pages.Length - 1;

            if (webRequest.isNetworkError) {
                Debug.Log ("Error: " + webRequest.error);
            } else {
                Debug.Log (DebugManager.GetSymbol ("asterisk") + " DataManager.GetNewData() " +
                        uri + "\n" + webRequest.downloadHandler.text);

                // parse JSON array 
                JArray a = JArray.Parse (webRequest.downloadHandler.text);

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

                    // parse eventData 
                    JObject d = JObject.Parse (item.GetValue ("eventData").ToString ());


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

                    feeds.Add (output);


                    // SAVE FOR CREATING BUFFER
                    //// create key
                    //string key = _createdAtStr + "_" + _username + "_" + _eventType;
                    //FeedData val;
                    //if (!eventDictionary.TryGetValue (key, out val)) {
                    //    eventDictionary.Add (key, output);
                    //}



                    //Debug.Log(_eventType);
                }

                // SAVE FOR CREATING BUFFER
                ///// Acquire keys and sort them.
                //var list = eventDictionary.Keys.ToList ();
                //list.Sort ();

                //// Loop through keys.
                //foreach (var key in list) {
                //    Debug.Log (key + ": " + eventDictionary [key]);
                //}



                // OLD METHOD - KEEPING FOR REFERENCEx
                //feeds = a.Select(p => new FeedData
                //{
                //    username = (string)p["username"],
                //    avatarPath = (string)p["avatarPath"],
                //    eventType = (string)p["eventType"],
                //    createdAt = (DateTime)p["createdAt"],

                //    //type = (string)p["type"],
                //    //name = (string)p["name"],
                //    //level = (int)p["level"],
                //    //stat = (string)p["stat"],
                //    //captured = (int)p["captured"],
                //}).ToList();
                ////Debug.Log(a[0]);
                //Debug.Log(feeds.ToString());




                // FOR DEBUGGING - MAY OR MAY NOT HAVE A GARBAGE COLLECTION ISSUE

                //// reset current
                //current = "";

                //foreach (var feed in feeds) {

                //    var line =

                //        feed.createdAt + "\t" +
                //        feed.username + ", " +
                //        feed.eventType + ", " +

                //        //feed.type + ", " + feed.name + ", " + feed.level +
                //        //", " + feed.type

                //        ""
                //        ;

                //    current += line + "<br>";

                //    //Debug.Log(line);
                //}




                // update count
                dataCount = feeds.Count;

                // trigger event
                EventManager.TriggerEvent ("DataUpdated");



            }
        }
    }
}
