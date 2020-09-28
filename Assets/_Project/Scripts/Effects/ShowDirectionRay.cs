using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowDirectionRay : MonoBehaviour {

    [ColorUsageAttribute (true, true)]
    public Color color;

    void Update ()
    {
        // Draw a ray pointing in the forward direction
        Debug.DrawRay (transform.position, Vector3.forward * 10f, color);
    }


}