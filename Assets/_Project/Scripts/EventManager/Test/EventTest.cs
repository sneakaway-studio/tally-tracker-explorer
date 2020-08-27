using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventTest : MonoBehaviour
{
    // listen OnEnable and stop listening OnDisable
    private UnityAction someListener;

    private void Awake()
    {
        someListener = new UnityAction(SomeFunction);
    }

    private void OnEnable()
    {
        EventManager.StartListening("test", someListener);
    }

    private void OnDisable()
    {
        EventManager.StopListening("test", someListener);
    }

    void SomeFunction()
    {
        Debug.Log("Some function was called");
    }

}
