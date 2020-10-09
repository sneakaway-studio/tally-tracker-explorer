using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 *	Play animations attached to this object's Animator
 */

public class AnimController : MonoBehaviour {

    public Animator animator;
    public int animIndex = 0;    // the animation to play
    public string animName;            //



    void Start ()
    {
        animator = GetComponent<Animator> ();
    }


    void Update ()
    {
        // set animIndex based on animName - string set from TimelineManager - or key

        if (animName == "Swirl_r_sm" || Input.GetKey (KeyCode.Alpha1)) {
            animIndex = 1;
        } else if (animName == "Swirl_r_md" || Input.GetKey (KeyCode.Alpha2)) {
            animIndex = 2;
        } else if (animName == "Pop_Shake_md" || Input.GetKey (KeyCode.Alpha3)) {
            animIndex = 3;
        } else if (animName == "Pop_Shake_sm" || Input.GetKey (KeyCode.Alpha4)) {
            animIndex = 4;
        } else if (animName == "Pop_sm" || Input.GetKey (KeyCode.Alpha5)) {
            animIndex = 5;
        } else if (animName == "Rotate_md" || Input.GetKey (KeyCode.Alpha6)) {
            animIndex = 6;
        } else if (animName == "Rotate_Pop_sm" || Input.GetKey (KeyCode.Alpha7)) {
            animIndex = 7;
        }

        // play the animation
        animator.SetInteger ("state", animIndex);

        // then reset vars
        if (animIndex >= 0) {
            animName = "";
            animIndex = 0;
        }
    }







}
