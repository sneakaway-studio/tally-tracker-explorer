using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePositionFloating : PhysicsBase {

	// amplitude (size) and frequency (speed) of the wave
	[SerializeField]
	private float _xAmplitude = .1f;
	[SerializeField]
	private float _xFrequency = .2f;

	[SerializeField]
	private float _zAmplitude = .12f;
	[SerializeField]
	private float _zFrequency = .6f;


	public Vector3 direction; // a new direction vector


	void FixedUpdate ()
	{
		// get new distance from Sine wave
		direction = VectorFromSine ();

		// distance to move each frame = normalized distance vector * speed * time since last frame
		Vector3 step = direction * thrust * Time.deltaTime;

		// add step vector to current position
		rb.MovePosition (transform.position + step);
	}


	// return a new Vector3 from Sin and Cosine
	// reference https://www.youtube.com/watch?v=mFOi6W7lohk
	Vector3 VectorFromSine ()
	{
		return new Vector3 (
			Mathf.Sin (Time.time * _xFrequency) * _xAmplitude,
			0f,
			Mathf.Cos (Time.time * _zFrequency) * _zAmplitude
		);
	}


}
