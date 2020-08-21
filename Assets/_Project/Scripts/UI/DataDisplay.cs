using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DataDisplay : MonoBehaviour
{
    public TMP_Text TmText;


    private void Start()
    {
        ApiManager.dataUpdated += UpdateDisplay;
    }

    



    public void UpdateDisplay()
    {
        TmText.text = DataManager.current;
    }



}
