using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 *  This (somewhat temp) version adds sprites for PARTICLE animation
 */

public class MonsterAddSpritesParticleAnim : MonoBehaviour {

    // base class for monster 

    public int mid;
    //public SpriteRenderer spriteRenderer;
    //FrameAnimation frameAnimation;

    public int totalSprites;
    public int spriteIndex;

    private void Awake ()
    {

        //frameAnimation = GetComponent<FrameAnimation> ();
        //spriteRenderer = GetComponent<SpriteRenderer> ();

        // test
        //spriteRenderer.sprite = MonsterManager.Instance.monsters [0];
        //Debug.Log (spriteRenderer.sprite);







        // sprites to choose from
        totalSprites = MonsterManager.Instance.monsters.Length;


        // TEMP

        //frameAnimation.sprites = new Sprite [3];
        // make sure its divisible by 4
        spriteIndex = (int)Random.Range (0, totalSprites);
        while (spriteIndex % 4 != 0) {
            spriteIndex = (int)Random.Range (0, totalSprites);
        }

        // set the next three as the sprites for this animation
        //frameAnimation.sprites [0] = MonsterManager.Instance.monsters [spriteIndex + 1];
        //frameAnimation.sprites [1] = MonsterManager.Instance.monsters [spriteIndex + 2];
        //frameAnimation.sprites [2] = MonsterManager.Instance.monsters [spriteIndex + 3];



        ParticleSystem ps = GetComponent<ParticleSystem> ();
        ParticleSystem.TextureSheetAnimationModule tsam = ps.textureSheetAnimation;
        tsam.SetSprite (0, MonsterManager.Instance.monsters [spriteIndex + 1]);
        tsam.SetSprite (1, MonsterManager.Instance.monsters [spriteIndex + 2]);
        tsam.SetSprite (2, MonsterManager.Instance.monsters [spriteIndex + 3]);



    }




}
