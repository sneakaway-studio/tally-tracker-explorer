using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsInfo : MonoBehaviour {

	public float speed;
	public float angularSpeed;
	protected Rigidbody rb;


	void Start ()
	{
		rb = GetComponent<Rigidbody> ();
	}

	void FixedUpdate ()
	{
		// Velocity
		// - Represents the rate of change of Rigidbody position from forces
		// - Is only affected by physics simulation (colliders and AddForce)
		// - Forces on object will increase velocity by that value, each second
		// - ^ For example, gravity will increase velocity 9.81 each second and will 
		// - ^ continue until it reaches terminal velocity (maximum velocity per the force)
		// - Velocity is also affected by forces acting against it
		// - ^ For example, the drag value (a.k.a. "air resistance") will slow the effect of the force
		// - ^ More drag means more resistance (feather = 20, stone = 1)
		speed = rb.velocity.magnitude;

		// Angular speed
		// - Represents maximum turning speed in (deg/s)
		// - All rules above apply here as well...
		angularSpeed = rb.angularVelocity.magnitude;

	}

}
