using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePositionInput : PhysicsBase {

	void FixedUpdate ()
	{
		// direction to move each frame = normalized input vector * speed * time since last frame
		Vector3 step = playerInput * thrust * Time.deltaTime;

		// add step vector to current position
		rb.MovePosition (transform.position + step);
	}

}
