using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 *  Make a game object face the camera regardless of its parent transform(s)
 */
public class FaceCamera : MonoBehaviour {

    public bool slowly;         // whether or not to step or "transport"
    float zAxisStep = 0;        // amount to step
    [Range (0f, .5f)]
    public float stepScalar = .01f;    // how fast to step
    Camera cam;                 // which camera to face

    private void Awake ()
    {
        cam = Camera.main;
    }

    void Update ()
    {
        // should we slowly step
        if (slowly)
            zAxisStep = Mathf.Lerp (transform.rotation.eulerAngles.z, 0, stepScalar);
        else
            zAxisStep = 0;

        // face the camera on each axis
        transform.rotation = Quaternion.Euler (0, cam.transform.eulerAngles.y, zAxisStep);
    }

}
