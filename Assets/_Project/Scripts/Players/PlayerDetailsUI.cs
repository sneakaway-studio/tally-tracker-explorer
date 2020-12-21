using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class PlayerDetailsUI : MonoBehaviour {

    // references
    public GameObject playerPanel;
    public TMP_Text usernameText;
    public TMP_Text dataText;

    // current data index displayed
    public int dataIndex = 0;
    public string [] dataArray = new string [9];
    private IEnumerator displayNewDataCoroutine;

    private void Awake ()
    {
        // hide in case I left open
        playerPanel.SetActive (false);

        displayNewDataCoroutine = displayNewData ();
        StartCoroutine (displayNewDataCoroutine);
    }

    void StopStartCouroutine ()
    {
        StopCoroutine (displayNewDataCoroutine);
        dataIndex = 0;
        StartCoroutine (displayNewDataCoroutine);
    }

    // Sets the UI text to be filled with player data
    public void updateData (FeedData feedData)
    {
        // reset index
        dataIndex = 0;
        // update username 
        usernameText.text = "<style=c3>" + feedData.username + "</style>";
        // update data array 
        dataArray = new string [9] {
            "level: " + string.Format("{0,1:###,###,###.##}",feedData.level) + "",
            "clicks: " + string.Format("{0,1:###,###,###.##}",feedData.clicks) + "",
            "score: " + string.Format("{0,1:###,###,###.##}",feedData.score) + "",
            "played: " + string.Format("{0,1:###,###,###.##}",(feedData.time / 60))  + " minutes",
            "battles fought: " + string.Format("{0,1:###,###,###.##}",(feedData.capturedTotal + feedData.missedTotal)) + "",
            "monsters captured: " + string.Format("{0,1:###,###,###.##}",feedData.capturedTotal) + "",
            "scrolled: " + string.Format("{0,1:###,###,###.##}",(feedData.pageActionScrollDistance / 1000000)) + " km",
            "trackers seen: " + string.Format("{0,1:###,###,###.##}",feedData.trackersSeen) + "",
            "trackers blocked: " + string.Format("{0,1:###,###,###.##}",feedData.trackersBlocked) + "",
        };
        StopStartCouroutine ();
    }

    // Makes a different data visible every n seconds
    IEnumerator displayNewData ()
    {
        while (true) {

            dataText.text = "<style=c2>" + dataArray [dataIndex] + "</style>";

            if (++dataIndex >= dataArray.Length)
                dataIndex = 0;

            yield return new WaitForSeconds (2f);
        }
    }
}
