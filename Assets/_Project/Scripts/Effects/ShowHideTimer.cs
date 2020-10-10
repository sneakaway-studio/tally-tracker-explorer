using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowHideTimer : MonoBehaviour {




    [Serializable]
    public enum ShowHide {
        show,
        hide
    }
    public ShowHide showHideChoice;
    public float wait;

    void Start ()
    {
        if ((int)showHideChoice == 0)
            StartCoroutine (Show ());
        else if ((int)showHideChoice == 1)
            StartCoroutine (Hide ());
    }

    IEnumerator Show ()
    {
        yield return new WaitForSeconds (wait);
        gameObject.SetActive (true);
    }

    IEnumerator Hide ()
    {
        yield return new WaitForSeconds (wait);
        gameObject.SetActive (false);
    }


}
