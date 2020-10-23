using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

// EventManager class from
// https://learn.unity.com/tutorial/create-a-simple-messaging-system-with-events

[ExecuteAlways]
public class EventManager : MonoBehaviour {
    // hold references to events
    private Dictionary<string, UnityEvent> eventDictionary;
    // show event count
    public int eventCount = 0;
    // show event names (as list because you can serialize)
    public List<string> eventNames = new List<string> ();



    // singleton
    private static EventManager eventManager;
    public static EventManager instance {
        get {
            // if we don't have it
            if (!eventManager) {
                // then find it 
                eventManager = FindObjectOfType (typeof (EventManager)) as EventManager;
                // if null
                if (!eventManager) {
                    Debug.LogError ("There needs to be one active EventManger script on a GameObject in your scene.");
                } else {
                    // initialize 
                    eventManager.Init ();
                }
            }
            return eventManager;
        }
    }

    void Init ()
    {
        if (eventDictionary == null) {
            eventDictionary = new Dictionary<string, UnityEvent> ();
        }
    }

    public static void StartListening (string eventName, UnityAction listener)
    {
        UnityEvent thisEvent = null;
        // is there already a key/value pair?
        if (instance.eventDictionary.TryGetValue (eventName, out thisEvent)) {
            thisEvent.AddListener (listener);
        } else {
            // add new event
            thisEvent = new UnityEvent ();
            thisEvent.AddListener (listener);
            instance.eventDictionary.Add (eventName, thisEvent);
        }
        UpdateEventDetails ();
    }

    public static void StopListening (string eventName, UnityAction listener)
    {
        // make sure not null
        if (eventManager == null) return;
        // otherwise remove event
        UnityEvent thisEvent = null;
        if (instance.eventDictionary.TryGetValue (eventName, out thisEvent)) {
            thisEvent.RemoveListener (listener);
        }
        UpdateEventDetails ();
    }

    public static void TriggerEvent (string eventName)
    {
        // if application is playing 
        if (Application.IsPlaying (instance)) {
            //Debug.Log (DebugManager.GetSymbol ("blackstar") + " EventManager.TriggerEvent() -> eventName = " + eventName);
            DebugManager.Instance.UpdateDisplay ("EventManager.TriggerEvent() eventName = " + eventName);
        }

        UnityEvent thisEvent = null;
        if (instance.eventDictionary.TryGetValue (eventName, out thisEvent)) {
            // invoke event 
            thisEvent.Invoke ();
        }
    }

    public static void UpdateEventDetails ()
    {
        // get count 
        instance.eventCount = instance.eventDictionary.Count;
        // clear list
        instance.eventNames.Clear ();
        // add names to list to display in inspector
        foreach (var e in instance.eventDictionary) {
            instance.eventNames.Add (e.Key);
        }
    }

}
