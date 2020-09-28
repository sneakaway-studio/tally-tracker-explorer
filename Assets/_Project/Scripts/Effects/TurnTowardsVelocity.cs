using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnTowardsVelocity : MonoBehaviour {

    // get RigidBody2d of parent
    public Rigidbody2D rb2d;
    public Vector3 direction;
    public float angle;


    private void Awake ()
    {

    }

    void Update ()
    {
        // get the direction / velocity vector
        direction = rb2d.velocity;
        // compute angle of vector
        angle = Mathf.Atan2 (direction.y, direction.x) * Mathf.Rad2Deg;
        // set rotation with angle
        transform.rotation = Quaternion.AngleAxis (angle, Vector3.forward);

        // Draw a ray pointing in the direction
        Debug.DrawRay (transform.position, direction, Color.green);

    }


}
