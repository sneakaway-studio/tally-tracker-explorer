using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 *	Play animations attached to this object's Animator
 */

public class TallyAnimController : MonoBehaviour {

    public Animator animator;
    public int currentAnimation = 0;    // the animation to play
    public string animEvent;            //



    void Start ()
    {
        animator = GetComponent<Animator> ();
    }


    void Update ()
    {

        // set currentAnimation based on animEvent - string set from TimelineManager or key

        // Swirl_r_sm
        if (animEvent == "attack" || Input.GetKey (KeyCode.Alpha1)) {
            currentAnimation = 1;
        }
        // Swirl_r_md
        else if (animEvent == "badge" || Input.GetKey (KeyCode.Alpha2)) {
            currentAnimation = 2;
        }
        // Pop_Shake_md
        else if (animEvent == "stream" || Input.GetKey (KeyCode.Alpha3)) {
            currentAnimation = 3;
        }
        // Pop_Shake_sm
        else if (animEvent == "leaderboard" || Input.GetKey (KeyCode.Alpha4)) {
            currentAnimation = 4;
        }
        // Pop_sm
        else if (animEvent == "consumable" || Input.GetKey (KeyCode.Alpha5)) {
            currentAnimation = 5;
        }
        // Rotate_md
        else if (animEvent == "tracker" || Input.GetKey (KeyCode.Alpha6)) {
            currentAnimation = 6;
        }
        // Rotate_Pop_sm
        else if (animEvent == "disguise" || Input.GetKey (KeyCode.Alpha7)) {
            currentAnimation = 7;
        }

        // play the animation
        animator.SetInteger ("state", currentAnimation);

        // then reset vars
        if (currentAnimation >= 0) {
            animEvent = "";
            currentAnimation = 0;
        }
    }







}
