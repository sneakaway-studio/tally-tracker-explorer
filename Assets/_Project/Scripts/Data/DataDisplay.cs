using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DataDisplay : MonoBehaviour {



    public TMP_Text eventCountText;
    public TMP_Text playerCountText;
    public TMP_Text eventNumberText;



    // listeners
    void OnEnable ()
    {
        EventManager.StartListening ("DataUpdated", UpdateDisplay);
    }
    void OnDisable ()
    {
        EventManager.StopListening ("DataUpdated", UpdateDisplay);
    }


    // update text
    public void UpdateDisplay ()
    {
        eventCountText.text = DataManager.dataCount.ToString ();
        playerCountText.text = PlayerManager.Instance.gameObject.transform.childCount.ToString ();
        eventNumberText.text = TimelineManager.Instance.feedIndex.ToString ();
    }






}
