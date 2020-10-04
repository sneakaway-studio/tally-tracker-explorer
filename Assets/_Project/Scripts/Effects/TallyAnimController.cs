using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 *	Play animations attached to this object's Animator
 */

public class TallyAnimController : MonoBehaviour {

    public Animator animator;
    public int currentAnimation = 0;
    public int counter;
    float r = 0;

    public string animEvent;


    void Start ()
    {
        animator = GetComponent<Animator> ();
    }


    void Update ()
    {

        //currentAnimation = 0;
        //r = 0;
        //counter++;

        //if (counter % 10 == 0) {
        //    r = Random.Range (0f, 1f);
        //}


        //Debug.Log (counter + ", " + r);



        if (animEvent == "attack" || Input.GetKey (KeyCode.Alpha1)) {
            currentAnimation = 1;
        } else if (animEvent == "badge" || Input.GetKey (KeyCode.Alpha2)) {
            currentAnimation = 2;
        } else if (animEvent == "stream" || Input.GetKey (KeyCode.Alpha3)) {
            currentAnimation = 3;
        } else if (animEvent == "stream" || Input.GetKey (KeyCode.Alpha4)) {
            currentAnimation = 4;
        } else if (animEvent == "monster" || Input.GetKey (KeyCode.Alpha5)) {
            currentAnimation = 5;
        }

        //if (currentAnimation > 0 && r > .9f)
        //    currentAnimation = (int)Random.Range (1, 5);
        //else
        //    currentAnimation = 0;

        // set the animation
        animator.SetInteger ("state", currentAnimation);




        // reset everything 
        if (currentAnimation >= 0) {
            animEvent = "";
            currentAnimation = 0;
        }
    }




}
