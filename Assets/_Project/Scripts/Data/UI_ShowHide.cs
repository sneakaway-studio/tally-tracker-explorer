using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_ShowHide : MonoBehaviour {


    public GameObject controlParent;
    public GameObject legendParent;
    public GameObject feedParent;
    public GameObject timelineParent;
    TallyInputSystem inputs;


    private void Start ()
    {
        inputs = new TallyInputSystem ();
        inputs.Enable ();

        // show on run
        controlParent.SetActive (true);
        timelineParent.SetActive (true);
        // hide on run
        legendParent.SetActive (false);
        feedParent.SetActive (false);
    }


    void Update ()
    {
        if (inputs.UI.Quit.triggered) {
            Application.Quit ();
        } else if (inputs.UI.ControlToggle.triggered) {
            ShowHide (controlParent);
        } else if (inputs.UI.LegendToggle.triggered) {
            //Debug.Log ("inputs.UI.LegendToggle.triggered");
            ShowHide (legendParent);
        } else if (inputs.UI.FeedToggle.triggered) {
            ShowHide (feedParent);
        } else if (inputs.UI.TimelineToggle.triggered) {
            ShowHide (timelineParent);
        }

    }

    /**
     *  Show or hide a GameObject based on its current state
     */
    void ShowHide (GameObject obj)
    {
        if (obj.activeSelf)
            obj.SetActive (false);
        else
            obj.SetActive (true);
    }


}
