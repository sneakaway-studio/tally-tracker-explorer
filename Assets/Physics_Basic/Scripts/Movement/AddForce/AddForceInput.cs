using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddForceInput : PhysicsBase {

	void FixedUpdate ()
	{
		// direction to move each frame = normalized input vector * scalar
		Vector3 step = playerInput * thrust;

		// AddForce() adds the direction vector to the velocity, every second
		// takes into account the drag setting on the RigidBody
		rb.AddForce (step);
	}

}
