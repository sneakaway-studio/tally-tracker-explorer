using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddForceNoise : PhysicsBase {

	public Vector3 direction; // a new direction vector


	void FixedUpdate ()
	{
		// get new direction from noise
		direction = VectorFromNoise ();

		// direction to move each frame = normalized input vector * scalar
		Vector3 step = direction * thrust;

		// AddForce() adds the direction vector to the velocity, every second
		// takes into account the drag setting on the RigidBody
		rb.AddForce (step);
	}


	// random direction vector from Perlin noise
	// reference https://roystan.net/articles/camera-shake.html
	Vector3 VectorFromNoise ()
	{
		float frequency = 1f;
		float seed = Random.Range (-1f, 1f);

		return new Vector3 (
			Mathf.PerlinNoise (seed + 1, Time.time * frequency) * 2 - 1,
			0,
			Mathf.PerlinNoise (seed + 2, Time.time * frequency) * 2 - 1
		);
	}

}
