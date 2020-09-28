using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceCamera : MonoBehaviour {

    Camera cam;

    private void Awake ()
    {
        cam = Camera.main;
    }

    void Update ()
    {
        // turn on the Y axis to face face camera
        transform.rotation = Quaternion.Euler (0, cam.transform.eulerAngles.y, 0);

    }
}
