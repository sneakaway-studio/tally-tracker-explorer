using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ProjectManager : Singleton<ProjectManager> {
    // singleton
    protected ProjectManager () { }
    //public static new ProjectManager Instance;

    SettingsManager settingsManager;

    private void Awake ()
    {
        settingsManager = GetComponent<SettingsManager> ();
    }


    private void Start ()
    {

        // start everything
        if (settingsManager.autostart) {
            Restart ();
        }

        //StartCoroutine (CheckBuffer ());
    }

    /**
     *  Restart everything
     */
    public void Restart ()
    {
        // trigger events
        //EventManager.TriggerEvent ("GetNewData");
        // the above is called in starttimeline

        //
        EventManager.TriggerEvent ("StartTimeline");

    }


}
