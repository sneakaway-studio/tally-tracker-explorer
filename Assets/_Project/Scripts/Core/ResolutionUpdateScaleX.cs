using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 *  Update components based on parameters from the ResolutionManager
 */

public class ResolutionUpdateScaleX : ResolutionUpdateBase {

    // default settings @ 1920x1080
    [SerializeField]
    Vector3 originalScale;

    [Range (-10, 10)]
    public float scalar = 1;

    protected override void Awake ()
    {
        base.Awake ();

        // base original scale on transform (assuming 1920x1080)
        originalScale = transform.localScale;

        // set original scale (after everything is computer) based on startup aspect ratio
        UpdateResolution ();
    }

    protected override void UpdateResolution ()
    {
        //base.UpdateResolution ();

        // update the horizontal width 
        transform.localScale = new Vector3 (
            originalScale.x * resolutionManager.playerAspectRatio * scalar,
            originalScale.y,
            originalScale.z
        );

    }
}