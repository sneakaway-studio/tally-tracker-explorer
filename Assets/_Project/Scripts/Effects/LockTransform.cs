using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockTransform : MonoBehaviour {


    public Vector3 lockPosition;
    Vector3 holdPosition;

    public Vector3 lockScale;
    Vector3 holdScale;

    void LateUpdate ()
    {

        // POSITION

        holdPosition = new Vector3 (
            transform.position.x,
            transform.position.y,
            transform.position.z
        );

        if (lockPosition.x != 0)
            holdPosition.x = lockPosition.x;
        if (lockPosition.y != 0)
            holdPosition.y = lockPosition.y;
        if (lockPosition.z != 0) {
            holdPosition.z = lockPosition.z;
            //Debug.Log ("LockTransform test");
        }
        // keep position on specific axis
        transform.position = holdPosition;


        // SCALE

        holdScale = new Vector3 (
            transform.localScale.x,
            transform.localScale.y,
            transform.localScale.z
        );
        if (lockScale.x != 0)
            holdScale.x = lockScale.x;
        if (lockScale.y != 0)
            holdScale.y = lockScale.y;
        if (lockScale.z != 0)
            holdScale.z = lockScale.z;

        // keep scale sam
        transform.localScale = holdScale;


    }


}
