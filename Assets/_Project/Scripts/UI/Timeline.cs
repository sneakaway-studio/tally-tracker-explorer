using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timeline : MonoBehaviour
{


    [SerializeField]
    private Slider speedSlider;
    float speedMin = 0;
    float speedMax = 100;



    void Start()
    {
        speedSlider.minValue = speedMin;
        speedSlider.maxValue = speedMax;
        speedSlider.value = speedMax / 2;
    }



    void PlayBack()
    {

    }

}
