using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DataDisplay : MonoBehaviour {



    public TMP_Text bufferCountText;
    public TMP_Text historyCountText;
    public TMP_Text playerCountText;



    // listeners
    void OnEnable ()
    {
        //EventManager.StartListening ("DataDownloaded", UpdateDisplay);
        //EventManager.StartListening ("DataUpdated", UpdateDisplay);
        EventManager.StartListening ("PlayersUpdated", UpdateDisplay);
        EventManager.StartListening ("TimelineUpdated", UpdateDisplay);
    }
    void OnDisable ()
    {
        //EventManager.StopListening ("DataDownloaded", UpdateDisplay);
        //EventManager.StopListening ("DataUpdated", UpdateDisplay);
        EventManager.StopListening ("PlayersUpdated", UpdateDisplay);
        EventManager.StopListening ("TimelineUpdated", UpdateDisplay);
    }


    // update text
    public void UpdateDisplay ()
    {

        bufferCountText.text = "Buffer: " + Timeline.Instance.bufferCount.ToString ();
        historyCountText.text = "History: " + Timeline.Instance.historyCount.ToString ();
        playerCountText.text = "Players: " + PlayerManager.Instance.playerCount.ToString ();

    }






}
