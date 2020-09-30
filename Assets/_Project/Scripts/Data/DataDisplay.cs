using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DataDisplay : MonoBehaviour {
    public TMP_Text TmText;



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
        // handled inside Timeline class
        TmText.text = DataManager.dataCount.ToString ();
    }






}
