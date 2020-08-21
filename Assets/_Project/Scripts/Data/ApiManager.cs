using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System;
using System.Linq;

public class ApiManager : Singleton<ApiManager>
{
    // singleton
    protected ApiManager() { }
    public static new ApiManager Instance;

    // create delegate for other classes to subscribe to 
    public delegate void DataUpdated();
    public static event DataUpdated dataUpdated;


    void Start()
    {
        // StartCoroutine(GetRequest("https://tallysavestheinternet.com/api/feed/recent"));
        // test for error handling
        // StartCoroutine(GetRequest("https://error.html"));
    }

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

                IList<FeedData> feeds = feedsArray.Select(p => new FeedData
                {
                    username = (string)p["username"],
                    avatarPath = (string)p["avatarPath"],
                    item = (string)p["item"],
                    type = (string)p["type"],
                    name = (string)p["name"],
                    level = (int)p["level"],
                    stat = (string)p["stat"],
                    captured = (int)p["captured"],
                    date = (DateTime)p["date"],


                    // AuthorName = (string)p["Author"]["Name"],
                    // AuthorTwitter = (string)p["Author"]["Twitter"],
                    // PostedDate = (DateTime)p["Date"],
                    // Body = HttpUtility.HtmlDecode((string)p["BodyHtml"])
                }).ToList();


                Debug.Log(feedsArray[0]);
                Debug.Log(feeds.ToString());

                //for (int i = 0; i < 3; i++)
                foreach (var feed in feeds)
                {
                    var line = feed.username + ", " + feed.item + ", " +
                        feed.type + ", " + feed.name + ", " + feed.level +
                        ", " + feed.type + ", " + feed.date;
                    DataManager.current += line + "<br>";
                    
                    Debug.Log(line);

                }
                if (dataUpdated != null)
                    dataUpdated();

            }
        }
    }
}
