using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// this is the 2D version

public class MovePositionWanderComplex : PhysicsBase {

    public Vector3 direction;           // the direction vector

    // for wandering algorithm
    public BoxCollider worldContainerCollider;    // collider to test new positions
    public Vector3 wayPoint;            // new position to head towards
    public float targetThreshold = 1f;  // test distance to target - must be > 0
    public float pointSelectRange = 8f; // range from which to select new wayPoint
    public float distanceToWaypoint;

    // for rotation
    public float rotateTimeElapsed = 0;
    public float rotateDuration = 200;

    private CameraManager cameraManager;

    private void Start ()
    {
        // get container collider
        worldContainerCollider = GameObject.Find ("ResolutionManager").GetComponent<BoxCollider> ();

        // first wander point
        wayPoint = ReturnNewWanderPoint ();

        cameraManager = GetComponentInParent<Player>().cameraManager;
    }

    protected override void Update ()
    {
        base.Update ();

        ShowRayBetweenPoints (transform.position, wayPoint);
    }

    void FixedUpdate ()
    {
        // distance from object to waypoint
        distanceToWaypoint = (transform.position - wayPoint).magnitude;

        // when the distance between gameObject and target is small enough
        if (distanceToWaypoint < targetThreshold) {

            rotateTimeElapsed = 0;

            // create a new target wayPoint 
            wayPoint = ReturnNewWanderPoint ();
        }

        // if player input is happening and the current object is targeted
        if (playerInput.magnitude != 0 && cameraManager.getCameraTarget().Equals(gameObject))
        {
            // get player input
            direction = playerInput;
        }
        else
        {
            // get direction
            direction = transform.TransformDirection(Vector2.right);
        }

        // distance to move each frame = normalized distance vector * speed * time since last frame
        Vector3 step = direction * thrust * Time.deltaTime;

        // add step vector to current position
        rb.MovePosition (transform.position + step);
        //rb.velocity = direction * thrust;


        // rotate towards waypoint
        RotateTowardsTarget2D ();
    }

    /**
     *  Turn transform towards a target
     */
    void RotateTowardsTarget2D ()
    {
        // change look direction immediately 
        //transform.right = wayPoint - transform.position;

        // change look direction slowly
        Vector3 temp = Vector3.Lerp(transform.right, (wayPoint - transform.position), rotateTimeElapsed / rotateDuration);

        // Values used to check for bad flipping
        float safetyX = Mathf.Abs(-1f - Mathf.Round(temp.x * 100.0f) / 100.0f);
        float safetyY = Mathf.Round(temp.y * 100.0f) / 100.0f;

        // If new rotation would flip player in y rotation, prevent this
        if (safetyX <= 0.1f && safetyY == 0)
        {
            temp = new Vector3(temp.x, 0.1f, 0);
        }

        // Set right vector
        transform.right = temp;

        rotateTimeElapsed += Time.deltaTime;
    }

    /**
	 *	Show ray between two points 
	 */
    void ShowRayBetweenPoints (Vector3 p1, Vector3 p2)
    {
        Debug.DrawRay (p1, (p2 - p1), Color.yellow);
    }

    /**
     *  Return a new target wander point within bounds of collider
     */
    Vector3 ReturnNewWanderPoint ()
    {
        bool pointWithin = false;       // is the point within the collider?
        Vector3 target = Vector3.zero;  // the new point, which defaults to center
        int safety = 0;

        // update the selection range depending on the size of the resolution
        pointSelectRange = worldContainerCollider.size.x * .1f;

        // loop until new point is within defined area
        while (!pointWithin) {
            // create target 
            target = new Vector3 (
                Random.Range (transform.position.x - pointSelectRange, transform.position.x + pointSelectRange),
                Random.Range (transform.position.y - pointSelectRange, transform.position.y + pointSelectRange),
                transform.position.z
            );
            //Debug.Log ("MovePositionWanderComplex.ReturnNewWanderPoint() - 🙌 target is within collider = " + IsPointWithinCollider (worldContainerCollider, target));
            // if found to be within safe area then return
            pointWithin = IsPointWithinCollider (worldContainerCollider, target);

            if (++safety > 10) {
                Debug.Log ("MovePositionWanderComplex.ReturnNewWanderPoint() - Safety first!");
                return Vector3.zero;
            }
        }
        return target;
    }

    /**
     *  Return true if point is inside worldcontainer collider
     */
    public static bool IsPointWithinCollider (BoxCollider collider, Vector3 point)
    {
        return (collider.ClosestPoint (point) - point).sqrMagnitude < Mathf.Epsilon * Mathf.Epsilon;
    }

    /**
     *  Return random Vector3 position inside bounds
     */
    public static Vector3 RandomPointInBounds (Bounds bounds)
    {
        return new Vector3 (
            Random.Range (bounds.min.x, bounds.max.x),
            Random.Range (bounds.min.y, bounds.max.y),
            Random.Range (bounds.min.z, bounds.max.z)
        );
    }

    /**
     *  If GameObject hits anything with a collider
     */
    void OnCollisionEnter2D (Collision2D collision)
    {
        // create a new wayPoint target
        wayPoint = ReturnNewWanderPoint ();
    }





}
