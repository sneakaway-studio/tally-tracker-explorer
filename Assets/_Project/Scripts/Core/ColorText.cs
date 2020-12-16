using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ColorText : MonoBehaviour {

    public int mid;
    public GameObject colorPanel;
    public TMP_Text colorText;


    public void Init (int _mid, MarketingColor m)
    {
        //Debug.Log ("Init() called on ColorText");

        mid = _mid;

        // random color
        //colorPanel.GetComponent<Image> ().color = ColorManager.GetColorFromDict (-1);
        colorPanel.GetComponent<Image> ().color = m.color;

        // set text     
        colorText.text = m.category;
    }

}
