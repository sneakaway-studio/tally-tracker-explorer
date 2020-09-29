using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceCamera : MonoBehaviour {

    // make a game object face the camera regardless of its parent transform

    Camera cam;

    private void Awake ()
    {
        cam = Camera.main;
    }

    void Update ()
    {
        // turn on the Y axis to face camera
        transform.rotation = Quaternion.Euler (0, cam.transform.eulerAngles.y, 0);
    }

}
