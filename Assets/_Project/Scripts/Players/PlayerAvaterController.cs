using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAvaterController : MonoBehaviour {


    // temp sprites for assigning avatars
    public SpriteRenderer spriteRenderer;
    public SpriteMask spriteMask;


    void Start ()
    {
        spriteRenderer = GetComponent<SpriteRenderer> ();
        // temp - choose random avatar
        spriteRenderer.sprite = PlayerManager.Instance.avatars [Random.Range (0, PlayerManager.Instance.avatars.Length - 1)];

        // set random sorting order
        spriteRenderer.sortingOrder = Random.Range (100, 10000);
        // get spritemask parent
        spriteMask = transform.GetComponentInParent<SpriteMask> ();
        // set sorting layers based on this order (prevents accidentally showing what's behind the mask of other avatars)
        spriteMask.frontSortingOrder = spriteRenderer.sortingOrder + 1;
        spriteMask.backSortingOrder = spriteRenderer.sortingOrder - 1;

    }


}
