using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatPhysicsCoroutine : MonoBehaviour
{
    Collider worldContainerCollider;// boundary
    Rigidbody rb;                   // rb to apply force

    // thrust to give the game object
    public float thrust = 20f;
    // current heading
    public Vector3 facingDirection;
    // extra influence to keep in camera view
    public Vector3 forceInfluence;


    private void Awake()
    {
        worldContainerCollider = GameObject.Find("WorldContainer").GetComponent<Collider>();
        rb = GetComponent<Rigidbody>();

        // start rotation timer 
        StartCoroutine(RotateToNewDirection(facingDirection, 0.25f));
    }



    private void FixedUpdate()
    {
        // keep on keepin' on
        rb.AddRelativeForce(Vector3.forward * thrust);
    }



    // pick random Vector3 from current direction
    Vector3 RandomDirectionFromCurrent(Vector3 distance)
    {
        return new Vector3(
                Random.Range(-distance.x, distance.x),
                Random.Range(-distance.y, distance.y),
                Random.Range(-distance.z, distance.z)
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
            float waitTime = Random.Range(3.0f, 8.0f);

            // if the GameObject has left the scene then push it back 
            forceInfluence = ForceAwayFromWall(worldContainerCollider.bounds, 3.0f);

            // change facing direction
            facingDirection = RandomDirectionFromCurrent(new Vector3(0f, 0f, 0f) + forceInfluence);

            // wait for before next loop
            yield return new WaitForSeconds(waitTime);
        }
    }


    Vector3 ForceAwayFromWall(Bounds bounds, float multiplier)
    {
        Vector3 newForce = new Vector3(0, 0, 0);

        // X
        if (transform.position.x < bounds.min.x)
        {
            newForce.x += multiplier;
        }
        else if (transform.position.x > bounds.max.x)
        {
            newForce.x -= multiplier;
        }

        // Y
        if (transform.position.y < bounds.min.y)
        {
            newForce.y += multiplier;
        }
        else if (transform.position.y > bounds.max.y)
        {
            newForce.y -= multiplier;
        }

        // Z
        if (transform.position.z < bounds.min.z)
        {
            newForce.z += multiplier;
        }
        else if (transform.position.z > bounds.max.z)
        {
            newForce.z -= multiplier;
        }
        return newForce;
    }


}
