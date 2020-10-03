using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// display a sprite animation with a coroutine

public class FrameAnimation : MonoBehaviour
{
    public SpriteRenderer sr;
    public Sprite[] sprites;
    public int frame;
    public float speed;

    // set initial vars
    void Awake()
    {
        // get components, init vars
        sr = GetComponent<SpriteRenderer>();
        frame = 0;
        speed = 0.1f;

        // start main animation loop
        StartCoroutine(Loop());
    }

    IEnumerator Loop()
    {
        while (true)
        {
            // if all frames are present
            if (sprites.Length == 3)
            {
                sr.sprite = sprites[frame];
                frame++;
            }
            if (frame >= 3)
                frame = 0;

            yield return new WaitForSeconds(speed);
        }
    }


}
