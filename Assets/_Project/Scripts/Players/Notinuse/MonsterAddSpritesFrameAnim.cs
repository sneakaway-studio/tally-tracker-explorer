using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 *  This (somewhat temp) version adds sprites for FRAME animation
 */

public class MonsterAddSpritesFrameAnim : MonoBehaviour {

    // base class for monster 

    public int mid;
    public SpriteRenderer spriteRenderer;
    FrameAnimation frameAnimation;

    public int totalSprites;
    public int spriteIndex;

    private void Awake ()
    {

        frameAnimation = GetComponent<FrameAnimation> ();
        spriteRenderer = GetComponent<SpriteRenderer> ();


        //SetRandomSpritesFromArray ();
    }

    // old test version, before all monster sprites were combined into three large sheets 
    //void SetRandomSpritesFromArray ()

    //            // test
    //    spriteRenderer.sprite = MonsterIndex.Instance.monsters [0];
    //    Debug.Log(spriteRenderer.sprite);

    //    frameAnimation.sprites = new Sprite [3];
    //    // pick random value
    //    totalSprites = MonsterIndex.Instance.monsters.Length;
    //    // make sure its divisible by 4
    //    spriteIndex = (int) Random.Range (0, totalSprites);
    //    while (spriteIndex % 4 != 0) {
    //        spriteIndex = (int) Random.Range (0, totalSprites);
    //    }

    //    // set the next three as the sprites for this animation
    //    frameAnimation.sprites [0] = MonsterIndex.Instance.monsters [spriteIndex + 1];
    //    frameAnimation.sprites [1] = MonsterIndex.Instance.monsters [spriteIndex + 2];
    //    frameAnimation.sprites [2] = MonsterIndex.Instance.monsters [spriteIndex + 3];

    //}




}
