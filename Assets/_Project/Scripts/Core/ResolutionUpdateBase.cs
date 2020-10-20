using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 *  Update components based on parameters from the ResolutionManager
 *  - only used in runtime (editor scripts that modify prefabs are complex!)
 */

//[ExecuteAlways]
public class ResolutionUpdateBase : MonoBehaviour {

    protected ResolutionManager resolutionManager;

    // these listeners call the method below, which are overridden with specific behaviors in inherited classes
    void OnEnable ()
    {
        EventManager.StartListening ("ResolutionUpdated", UpdateResolution);
    }
    void OnDisable ()
    {
        EventManager.StopListening ("ResolutionUpdated", UpdateResolution);
    }

    protected virtual void Awake ()
    {
        // object references for child classes
        resolutionManager = GameObject.Find ("ResolutionManager").GetComponent<ResolutionManager> ();

    }

    protected virtual void UpdateResolution ()
    {
        //Debug.Log ("ResolutionUpdaterBase");
    }
}
