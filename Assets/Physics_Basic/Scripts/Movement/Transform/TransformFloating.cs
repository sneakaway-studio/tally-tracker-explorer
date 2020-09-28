using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformFloating : PhysicsBase {

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


	protected override void Update ()
	{
		// call in base 
		base.Update ();

		// get new distance from Sine wave
		direction = VectorFromSine ();

		// direction to move each frame = normalized input vector * speed * time since last frame
		Vector3 step = direction * thrust * Time.deltaTime;

		// add step vector to current position
		transform.position += step;
		// this does the same as above
		//transform.Translate (step);
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
