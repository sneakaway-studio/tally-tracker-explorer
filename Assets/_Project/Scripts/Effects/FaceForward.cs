using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceForward : MonoBehaviour {


    public Transform parent;
    public Quaternion rotation;
    public float angle;


    void Update ()
    {
        // get local rotation of object to match
        rotation = parent.localRotation;
        // set angles
        transform.localEulerAngles = new Vector3 (
            rotation.x,
            rotation.y,
            rotation.z
        );

        // Draw a ray pointing in the forward direction
        Debug.DrawRay (transform.position, Vector3.forward, Color.green);
    }


}
