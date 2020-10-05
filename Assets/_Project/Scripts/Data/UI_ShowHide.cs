using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_ShowHide : MonoBehaviour {


    public GameObject debugParent;
    public GameObject instructionsParent;



    void Update ()
    {
        if (Input.GetKeyDown ("escape")) {
            Application.Quit ();
        } else if (Input.GetKeyDown (KeyCode.D)) {
            ShowHide (debugParent);
        } else if (Input.GetKeyDown (KeyCode.I)) {
            ShowHide (instructionsParent);
        }


    }


    void ShowHide (GameObject obj)
    {
        if (obj.activeSelf)
            obj.SetActive (false);
        else
            obj.SetActive (true);

    }




}
