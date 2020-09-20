using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatPhysicsCoroutine : MonoBehaviour
{
    Collider worldContainerCollider;// boundary
    Rigidbody rb;                   // rb to apply force
    float forceRange;               // use to generate new force directions
    public bool applyNewForce;      // should we apply new force on loop?
    public float newForceTime;      // the time before the current force expires
    public Vector3 force;           // the force vector (speed + direction) 
    public Vector3 forceInfluence;  // influence direction of force vector if too close to boundaries
    public float maxSpeed = 30f;    // max speed of rb


    public Vector3 facingDirection;

    void Awake()
    {
        worldContainerCollider = GameObject.Find("WorldContainer").GetComponent<Collider>();
        rb = GetComponent<Rigidbody>();

        // set the multiplier to generate new force
        forceRange = 20.0f;
        // start the force loop
        StartCoroutine(ForceVectorGenerator());
    }

    void FixedUpdate()
    {
        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
        if (applyNewForce)
        {
            // change facing direction
            //transform.Rotate(force);
            facingDirection = RandomDirectionFromCurrent(10.0f);
            // rotate to new
            StartCoroutine(RotateToNewDirection(facingDirection, 0.25f));


            rb.AddForce(Vector3.forward + force + forceInfluence);

            // add whatever force is stored in the vector
            //rb.AddForce(force + forceInfluence);
            // finished 
            applyNewForce = false;
        }
    }

    Vector3 RandomDirectionFromCurrent(float distance)
    {
        return new Vector3(
                Random.Range(-distance, distance),
                Random.Range(-distance, distance * .8f), // slowly sink
                Random.Range(-distance, distance)
            );
    }

    IEnumerator RotateToNewDirection(Vector3 facingDirection, float timeToRotate)
    {
        Quaternion targetRotation = Quaternion.LookRotation(facingDirection);
        Quaternion currentRotation = transform.rotation;
        for (float i = 0; i < 1.0f; i += Time.deltaTime / timeToRotate)
        {
            transform.rotation = Quaternion.Lerp(currentRotation, targetRotation, i);
            yield return null;
        }
    }

    // generate a new random direction at random times
    IEnumerator ForceVectorGenerator()
    {
        // run forever
        while (true)
        {
            // pick new time to wait before generating new force vector 
            newForceTime = Random.Range(3.0f, 8.0f);

            // if the GameObject has left the scene then push it back 
            forceInfluence = ForceAwayFromWall(worldContainerCollider.bounds, 3.0f);

            // generate new force vector 
            force = new Vector3(
                Random.Range(-forceRange, forceRange),
                Random.Range(-forceRange, forceRange * .8f), // slowly sink
                Random.Range(-forceRange, forceRange)
            );

            applyNewForce = true;

            // wait for before next loop
            yield return new WaitForSeconds(newForceTime);
        }
    }


    Vector3 ForceAwayFromWall(Bounds bounds, float multiplier)
    {
        Vector3 newForce = new Vector3(0, 0, 0);

        // X
        if (gameObject.transform.position.x < bounds.min.x)
        {
            newForce.x += multiplier;
        }
        else if (gameObject.transform.position.x > bounds.max.x)
        {
            newForce.x -= multiplier;
        }

        // Y
        if (gameObject.transform.position.y < bounds.min.y)
        {
            newForce.y += multiplier;
        }
        else if (gameObject.transform.position.y > bounds.max.y)
        {
            newForce.y -= multiplier;
        }

        // Z
        if (gameObject.transform.position.z < bounds.min.z)
        {
            newForce.z += multiplier;
        }
        else if (gameObject.transform.position.z > bounds.max.z)
        {
            newForce.z -= multiplier;
        }
        return newForce;
    }



}
