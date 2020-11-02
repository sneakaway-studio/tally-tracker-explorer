using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DataDisplay : MonoBehaviour {


    // DataRequestStats
    public TMP_Text receivedTotalText;
    public TMP_Text receivedNewText;
    public TMP_Text receivedDuplicatesText;
    public TMP_Text dataRequestStatusText;


    // TimelineStats
    public TMP_Text bufferPlusHistoryText;
    public TMP_Text bufferCountText;
    public TMP_Text historyCountText;
    public TMP_Text playerCountText;



    // listeners
    void OnEnable ()
    {
        // data requests
        EventManager.StartListening ("GetNewData", OnUpdateDisplay);
        EventManager.StartListening ("DataRequestFinished", OnUpdateDisplay);

        EventManager.StartListening ("PlayersUpdated", OnUpdateDisplay);
        EventManager.StartListening ("TimelineUpdated", OnUpdateDisplay);
    }
    void OnDisable ()
    {
        // data requests
        EventManager.StopListening ("GetNewData", OnUpdateDisplay);
        EventManager.StopListening ("DataRequestFinished", OnUpdateDisplay);

        EventManager.StopListening ("PlayersUpdated", OnUpdateDisplay);
        EventManager.StopListening ("TimelineUpdated", OnUpdateDisplay);
    }


    // update text
    public void OnUpdateDisplay ()
    {
        // datamanager
        receivedTotalText.text = "Received: " + DataManager.Instance.receivedTotal.ToString ();
        receivedNewText.text = "New data: " + DataManager.Instance.receivedNew.ToString ();
        receivedDuplicatesText.text = "Duplicates: " + DataManager.Instance.receivedDuplicates.ToString ();
        dataRequestStatusText.text = DataManager.Instance.dataRequestStatus.ToString ();
        // timeline
        bufferPlusHistoryText.text = "Total: " + Timeline.Instance.totalEventCount.ToString ();
        bufferCountText.text = "Buffer: " + Timeline.Instance.bufferCount.ToString ();
        historyCountText.text = "History: " + Timeline.Instance.historyCount.ToString ();
        playerCountText.text = "Players: " + PlayerManager.Instance.playerCount.ToString ();

    }






}
