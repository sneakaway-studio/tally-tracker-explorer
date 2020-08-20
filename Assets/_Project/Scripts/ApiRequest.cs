using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;

public class ApiRequest : MonoBehaviour
{
    void Start()
    {
        // StartCoroutine(GetRequest("https://tallysavestheinternet.com/api/feed/recent"));
        // test for error handling
        // StartCoroutine(GetRequest("https://error.html"));
    }

    public IEnumerator GetRequest(string uri)
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
                    username = (string)p["username"]
                    // AuthorName = (string)p["Author"]["Name"],
                    // AuthorTwitter = (string)p["Author"]["Twitter"],
                    // PostedDate = (DateTime)p["Date"],
                    // Body = HttpUtility.HtmlDecode((string)p["BodyHtml"])
                }).ToList();


                Debug.Log(feedsArray[0]);
                Debug.Log(feeds.ToString());
                Debug.Log(feeds[0].username);
                Debug.Log(feeds[1].username);
                Debug.Log(feeds[2].username);

            }
        }
    }
}
