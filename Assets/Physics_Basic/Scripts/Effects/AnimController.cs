using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 *	Play animations attached to this object's Animator
 */

public class AnimController : MonoBehaviour {

	public Animator animator;
	public int currentAnimation;


	void Start ()
	{
		animator = GetComponent<Animator> ();
	}


	void Update ()
	{



		if (Input.GetKey (KeyCode.Alpha1)) {
			currentAnimation = 1;
		} else if (Input.GetKey (KeyCode.Alpha2)) {
			currentAnimation = 2;
		} else if (Input.GetKey (KeyCode.Alpha3)) {
			currentAnimation = 3;
		} else if (Input.GetKey (KeyCode.Alpha4)) {
			currentAnimation = 4;
		} else if (Input.GetKey (KeyCode.Alpha5)) {
			currentAnimation = 5;
		} else {
			currentAnimation = 0;
		}


		// set the animation
		animator.SetInteger ("state", currentAnimation);


	}




}
