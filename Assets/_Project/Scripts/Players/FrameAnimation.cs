using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 *  Display a frame by frame sprite animation with a coroutine 
 */
public class FrameAnimation : MonoBehaviour {

    public SpriteRenderer sr;   // renderer to hold the frames
    public Sprite [] sprites;   // array of sprites to animate
    public int frame;           // current frame number
    public float speed;         // play speed
    public bool loop = true;    // play more than once?
    public bool playOnceAndDestroy = false; // play more than once?

    // set initial vars
    void Awake ()
    {
        // get components, init vars
        sr = GetComponent<SpriteRenderer> ();
        frame = 0;
        speed = 0.1f;

        // start main animation loop
        StartCoroutine (Loop ());
    }

    IEnumerator Loop ()
    {
        while (true) {

            // if all frames are present (useful for loading sprites async)
            //if (sprites.Length == 3) {
            sr.sprite = sprites [frame];
            frame++;
            //}

            // after reaching the last frame
            if (frame >= sprites.Length) {
                // reset count
                frame = 0;
                // should we destroy the gameobejct that holds this animation after one play?
                if (playOnceAndDestroy) {
                    Destroy (this.gameObject);
                }
                // if animation should only play once
                else if (!loop) {
                    // remove current frame
                    sr.sprite = null;
                    break;
                }
            }

            yield return new WaitForSeconds (speed);
        }
    }


}
