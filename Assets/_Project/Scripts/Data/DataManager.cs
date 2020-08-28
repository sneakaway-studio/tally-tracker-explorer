using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System;
using System.Linq;

public class DataManager : Singleton<DataManager>
{
    // singleton
    protected DataManager() { }
    public static new DataManager Instance;

    // listeners 
    void OnEnable()
    {
        EventManager.StartListening("GetNewData", GetNewData);
    }
    void OnDisable()
    {
        EventManager.StopListening("GetNewData", GetNewData);
    }

    // host and endpoint for API
    public string host;
    public string endpoint;
    // the current data as string
    public static string current;
    // as Dict
    public static IList<FeedData> feeds;

    private void Start()
    {
        host = "https://tallysavestheinternet.com/api/";
        //endpoint = "feed/range/1/week"; // a whole week
        endpoint = "feed/recent"; // 20 recent 
    }


    public void GetNewData()
    {
        StartCoroutine(GetRequest(host + endpoint));
        // error test
        // StartCoroutine(GetRequest("https://error.html"));
    }

    // do a get request for JSON data at url
    public static IEnumerator GetRequest(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            if (webRequest.isNetworkError)
            {
                Debug.Log(pages[page] + ": Error: " + webRequest.error);
            }
            else
            {
                Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);

                // parse JSON array 
                JArray feedsArray = JArray.Parse(webRequest.downloadHandler.text);

                feeds = feedsArray.Select(p => new FeedData
                {
                    username = (string)p["username"],
                    avatarPath = (string)p["avatarPath"],
                    item = (string)p["item"],
                    type = (string)p["type"],
                    name = (string)p["name"],
                    level = (int)p["level"],
                    stat = (string)p["stat"],
                    captured = (int)p["captured"],
                    time = (DateTime)p["date"],

                }).ToList();


                //Debug.Log(feedsArray[0]);
                //Debug.Log(feeds.ToString());


                // dump current
                current = "";

                foreach (var feed in feeds)
                {
                    var line =
                        feed.time + "\t" +
                        feed.username + ", " + feed.item + ", " +
                        feed.type + ", " + feed.name + ", " + feed.level +
                        ", " + feed.type;
                    current += line + "<br>";

                    //Debug.Log(line);
                }

                // send event
                EventManager.TriggerEvent("DataUpdated");

            }
        }
    }
}
