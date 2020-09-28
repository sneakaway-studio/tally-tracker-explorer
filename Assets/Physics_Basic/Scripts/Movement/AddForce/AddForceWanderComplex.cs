using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddForceWanderComplex : PhysicsBase {

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

		// slowly rotate towards the wayPoint
		RotateTowardsTargetOverTime (wayPoint, 1f, 3f);

		//Debug.Log (wayPoint + " and " + (transform.position - wayPoint).magnitude);

		// get direction from facing direction
		direction = transform.TransformDirection (Vector3.forward);

		// direction to move each frame = normalized input vector * scalar
		Vector3 step = direction * thrust;

		// AddForce() adds the direction vector to the velocity, every second
		// takes into account the drag setting on the RigidBody
		rb.AddForce (step);

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
			//Debug.Log("🙌 target is within collider = " + IsPointWithinCollider(worldContainerCollider, target));
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


	/**
	 *  Turn transform towards a target a little each frame
	 */
	void RotateTowardsTargetOverTime (Vector3 target, float minRotateTime, float maxRotateTime)
	{
		// Determine which direction to rotate towards
		Vector3 targetDirection = target - transform.position;

		// The step size is equal to speed times frame time.
		float rotateStep = Random.Range (minRotateTime, maxRotateTime) * Time.deltaTime;

		// Rotate the forward vector towards the target direction by one step
		Vector3 newDirection = Vector3.RotateTowards (transform.forward, targetDirection, rotateStep, 0.0f);

		// Draw a ray pointing at our target in
		Debug.DrawRay (transform.position, newDirection, Color.red);

		// Calculate a rotation a step closer to the target and applies rotation to this object
		transform.rotation = Quaternion.LookRotation (newDirection);
	}

}
