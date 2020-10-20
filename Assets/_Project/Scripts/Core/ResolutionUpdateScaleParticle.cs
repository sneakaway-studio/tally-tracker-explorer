using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 *  Update components based on parameters from the ResolutionManager
 */

public class ResolutionUpdateScaleParticle : ResolutionUpdateBase {

    ParticleSystem ps;

    // default settings @ 1920x1080
    [SerializeField]
    float originalShapeScaleX = 20f;
    [SerializeField]
    float originalEmissionRateOverTime = 3f;


    protected override void Awake ()
    {
        base.Awake ();
        ps = GetComponent<ParticleSystem> ();
    }

    protected override void UpdateResolution ()
    {
        base.UpdateResolution ();
        //Debug.Log ("ResolutionUpdaterSnow()");

        // update the horizontal width of the emitter shape
        var sh = ps.shape;
        sh.scale = new Vector3 (originalShapeScaleX * resolutionManager.playerAspectRatio, 1f, 1f);

        // increase the count
        var em = ps.emission;
        em.rateOverTime = originalEmissionRateOverTime * resolutionManager.playerAspectRatio;

    }
}
