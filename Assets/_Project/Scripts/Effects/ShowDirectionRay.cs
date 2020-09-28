using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowDirectionRay : MonoBehaviour {


    void Update ()
    {
        // draw rays pointing in the standard directions
        Debug.DrawRay (transform.position, transform.up * 5f, Color.green);
        Debug.DrawRay (transform.position, transform.forward * 5f, Color.blue);
        Debug.DrawRay (transform.position, transform.right * 5f, Color.red);
    }


}