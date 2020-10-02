using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAvaterController : MonoBehaviour {


    // temp sprites for assigning avatars
    public SpriteMask spriteMask;
    public SpriteRenderer spriteRenderer;


    void Start ()
    {
        spriteRenderer = GetComponent<SpriteRenderer> ();
        // temp - choose random
        spriteRenderer.sprite = PlayerManager.Instance.avatars [Random.Range (0, PlayerManager.Instance.avatars.Length - 1)];

        // set random order
        spriteRenderer.sortingOrder = Random.Range (100, 10000);

        spriteMask = transform.GetComponentInParent<SpriteMask> ();
        // set sorting layers
        spriteMask.frontSortingOrder = spriteRenderer.sortingOrder + 1;
        spriteMask.backSortingOrder = spriteRenderer.sortingOrder - 1;

    }


}
