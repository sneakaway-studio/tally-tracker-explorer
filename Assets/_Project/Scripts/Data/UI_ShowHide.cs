using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_ShowHide : MonoBehaviour {


    public GameObject controlParent;
    public GameObject feedParent;
    public GameObject timelineParent;



    void Update ()
    {
        if (Input.GetKeyDown ("escape") || Input.GetKeyDown (KeyCode.Q)) {
            Application.Quit ();
        } else if (Input.GetKeyDown (KeyCode.C)) {
            ShowHide (controlParent);
        } else if (Input.GetKeyDown (KeyCode.F)) {
            ShowHide (feedParent);
        } else if (Input.GetKeyDown (KeyCode.T)) {
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
