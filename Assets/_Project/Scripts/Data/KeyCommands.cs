using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KeyCommands : MonoBehaviour
{

    private void Update()
    {
        if (Input.GetKeyDown("q"))
        {
            EventManager.TriggerEvent("GetNewData");
        }

        if (Input.GetKeyDown("w"))
        {
            EventManager.TriggerEvent("DataUpdated");
        }

        if (Input.GetKeyDown("e"))
        {
            EventManager.TriggerEvent("Destroy");
        }
    }

}
