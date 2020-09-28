using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformInput : PhysicsBase {


	protected override void Update ()
	{
		// call in base 
		base.Update ();

		// direction to move each frame = normalized input vector * speed * time since last frame
		Vector3 step = playerInput * thrust * Time.deltaTime;

		// add step vector to current position
		transform.position += step;
		// this does the same as above
		//transform.Translate (step);
	}

}
