using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePositionNoise : PhysicsBase {

	public Vector3 direction; // a new direction vector


	void FixedUpdate ()
	{
		// get new direction from noise
		direction = VectorFromNoise ();

		// distance to move each frame = normalized distance vector * speed * time since last frame
		Vector3 step = direction * thrust * Time.deltaTime;

		// add step vector to current position
		rb.MovePosition (transform.position + step);
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
