﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatPhysics : MonoBehaviour
{
    //// container
    //public Collider worldContainerCollider;


    //Rigidbody rb;                   // rb to apply force
    //public float forcemultiplier;   // use to generate new force directions
    //public int applyNewForceCounter;      // should we apply new force on loop?
    //public int newForceTime;      // the time before the current force expires
    //public Vector3 force;           // the force vector (speed + direction) 
    //public Vector3 forceInfluence;  // influence direction of force vector if it gets close to the boundaries
    //public float maxSpeed = 30f;    // max speed of rb

    //public Vector3 mousePos;

    //void Awake()
    //{
    //    worldContainerCollider = GameObject.Find("WorldContainer").GetComponent<Collider>();
    //    rb = GetComponent<Rigidbody>();

    //    // first run 
    //    applyNewForceCounter = 0;
    //    // start the force loop
    //    force = ForceVectorGenerator();
    //}

    //void FixedUpdate()
    //{
    //    //if (rb.velocity.magnitude > maxSpeed)
    //    //{
    //    //    rb.velocity = rb.velocity.normalized * maxSpeed;
    //    //}
    //    if (--applyNewForceCounter < 0)
    //    {
    //        // add whatever force is stored in the vector
    //        rb.AddForce(force + forceInfluence);
    //        applyNewForceCounter = (int)Random.Range(4.0f, 8.0f);
    //    }

    //}


    //// generate a new random direction at random times
    //Vector3 ForceVectorGenerator()
    //{

    //    // pick new time to wait before generating new force vector 
    //    newForceTime = Random.Range(4.0f, 8.0f);

    //    // if the GameObject has left the scene then push it back 
    //    forceInfluence = ForceAwayFromWall(worldContainerCollider.bounds);




    //    applyNewForce = true;

    //    // generate new force vector 
    //    return new Vector3(
    //        Random.Range(-forcemultiplier, forcemultiplier),
    //        Random.Range(-forcemultiplier, forcemultiplier),
    //        Random.Range(-forcemultiplier, forcemultiplier)
    //    );

    //}


    //Vector3 ForceAwayFromWall(Bounds bounds)
    //{
    //    Vector3 newForce = new Vector3(0, 0, 0);

    //    // X
    //    if (gameObject.transform.position.x < bounds.min.x)
    //    {
    //        newForce.x += 2;
    //    }
    //    else if (gameObject.transform.position.x > bounds.max.x)
    //    {
    //        newForce.x -= 2;
    //    }

    //    // Y
    //    if (gameObject.transform.position.y < bounds.min.y)
    //    {
    //        newForce.y += 2;
    //    }
    //    else if (gameObject.transform.position.y > bounds.max.y)
    //    {
    //        newForce.y -= 2;
    //    }

    //    // Z
    //    if (gameObject.transform.position.z < bounds.min.z)
    //    {
    //        newForce.z += 2;
    //    }
    //    else if (gameObject.transform.position.z > bounds.max.z)
    //    {
    //        newForce.z -= 2;
    //    }
    //    return newForce;
    //}



}