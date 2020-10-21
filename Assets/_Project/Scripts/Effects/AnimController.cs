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
    TallyInputSystem inputs;


    void Start ()
    {
        animator = GetComponent<Animator> ();
        inputs = new TallyInputSystem();
        inputs.Enable();
    }


    void Update ()
    {
        // set animIndex based on animName - string set from TimelineManager - or key

        if (animName == "Swirl_r_sm" || inputs.Debug.Anim1.triggered) {
            animIndex = 1;
        } else if (animName == "Swirl_r_md" || inputs.Debug.Anim2.triggered) {
            animIndex = 2;
        } else if (animName == "Pop_Shake_md" || inputs.Debug.Anim3.triggered) {
            animIndex = 3;
        } else if (animName == "Pop_Shake_sm" || inputs.Debug.Anim4.triggered) {
            animIndex = 4;
        } else if (animName == "Pop_sm" || inputs.Debug.Anim5.triggered) {
            animIndex = 5;
        } else if (animName == "Rotate_md" || inputs.Debug.Anim6.triggered) {
            animIndex = 6;
        } else if (animName == "Rotate_Pop_sm" || inputs.Debug.Anim7.triggered) {
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
