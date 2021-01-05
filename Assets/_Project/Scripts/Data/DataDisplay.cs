using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DataDisplay : MonoBehaviour {



    [Space (10)]
    [Header ("DATA DETAILS PANEL")]


    // DataRequestStats
    public TMP_Text receivedNewText;
    public TMP_Text receivedDuplicatesText;
    public TMP_Text dataRequestStatusText;


    // TimelineStats


    [Space (10)]
    [Header ("TIMELINE PANEL")]

    public TMP_Text bufferCountText;
    public TMP_Text historyCountText;



    [Space (10)]
    [Header ("INFO PANEL")]

    public TMP_Text receivedTotalText;
    public TMP_Text playerCountText;
    public TMP_Text currentEventTxt;



    // listeners
    void OnEnable ()
    {
        // data requests
        EventManager.StartListening ("GetNewData", OnUpdateDisplay);
        EventManager.StartListening ("DataManagerUpdated", OnUpdateDisplay);
        EventManager.StartListening ("DataRequestFinished", OnUpdateDisplay);

        EventManager.StartListening ("PlayersUpdated", OnUpdateDisplay);
        EventManager.StartListening ("TimelineUpdated", OnUpdateDisplay);
    }
    void OnDisable ()
    {
        // data requests
        EventManager.StopListening ("GetNewData", OnUpdateDisplay);
        EventManager.StopListening ("DataManagerUpdated", OnUpdateDisplay);
        EventManager.StopListening ("DataRequestFinished", OnUpdateDisplay);

        EventManager.StopListening ("PlayersUpdated", OnUpdateDisplay);
        EventManager.StopListening ("TimelineUpdated", OnUpdateDisplay);
    }


    // update text
    public void OnUpdateDisplay ()
    {
        //Debug.Log ("DataDisplay.OnUpdateDisplay()");

        // data details panel - col 1
        //receivedNewText.text = "New data: " + DataManager.Instance.receivedNew.ToString ();
        //receivedDuplicatesText.text = "Duplicates: " + DataManager.Instance.receivedDuplicates.ToString ();
        dataRequestStatusText.text = DataManager.Instance.dataRequestStatus.ToString ();

        // timeline
        bufferCountText.text = Timeline.Instance.bufferCount.ToString ();
        historyCountText.text = Timeline.Instance.historyCount.ToString ();


        // InfoPanel
        playerCountText.text = PlayerManager.Instance.playerCount.ToString ();
        receivedTotalText.text = DataManager.Instance.receivedTotal.ToString ();
        currentEventTxt.text = PlayerManager.Instance.currentEventText.ToString ();
    }






}
