using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePositionWander : PhysicsBase {

	public Vector3 direction; // a new direction vector


	// properties for wandering algorithm
	Collider worldContainerCollider;    // collider to test new positions
	public Vector3 wayPoint;            // new position to head towards
	public float targetThreshold = 1f;  // test distance to target - must be > 0
	public float pointSelectRange = 6f; // range from which to select new wayPoint


	private void Start ()
	{
		// get container collider
		worldContainerCollider = GameObject.Find ("WorldContainer").GetComponent<Collider> ();

		// first wander point
		wayPoint = ReturnNewWanderPoint ();
	}

	void FixedUpdate ()
	{
		// when the distance between gameObject and target is small enough
		if ((transform.position - wayPoint).magnitude < targetThreshold) {
			// create a new target wayPoint 
			wayPoint = ReturnNewWanderPoint ();
		}

		// look at wayPoint
		transform.LookAt (wayPoint);

		//Debug.Log (wayPoint + " and " + (transform.position - wayPoint).magnitude);

		// get direction from facing direction
		direction = transform.TransformDirection (Vector3.forward);

		// distance to move each frame = normalized distance vector * speed * time since last frame
		Vector3 step = direction * thrust * Time.deltaTime;

		// add step vector to current position
		rb.MovePosition (transform.position + step);

	}


	/**
      *  Return a new target wander point within bounds of collider
      */
	Vector3 ReturnNewWanderPoint ()
	{
		bool pointWithin = false;       // is the point within the collider?
		Vector3 target = Vector3.zero;  // the new point

		// loop until new point is within defined area
		while (!pointWithin) {
			// create target 
			target = new Vector3 (
				Random.Range (transform.position.x - pointSelectRange, transform.position.x + pointSelectRange),
				1,
				Random.Range (transform.position.z - pointSelectRange, transform.position.z + pointSelectRange)
			);
			//Debug.Log ("🙌 target is within collider = " + IsPointWithinCollider (worldContainerCollider, target));
			// if found to be within safe area then return
			pointWithin = IsPointWithinCollider (worldContainerCollider, target);
		}
		return target;
	}

	/**
     *  Return true if point is inside worldcontainer collider
     */
	public static bool IsPointWithinCollider (Collider collider, Vector3 point)
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
	void OnCollisionEnter (Collision collision)
	{
		// create a new wayPoint target
		wayPoint = ReturnNewWanderPoint ();
	}





}
