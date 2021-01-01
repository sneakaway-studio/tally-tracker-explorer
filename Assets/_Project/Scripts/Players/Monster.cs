using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 *  Adds sprites for PARTICLE animation
 */

public class Monster : MonoBehaviour {


    // temp sprites for assigning monsters
    // - when you drag in all the files they will include the texture parent of the sprites so you have to handle this in the picker
    //public Sprite [] monsters;


    [Space (10)]
    [Header ("DETAILS")]

    // the mid of this current monster
    public int mid;
    // the index of this mid in the midsInGame
    public int gameMidsIndex;
    // the index of this monster in the sprite
    public int spriteIndex;

    public int totalSprites;

    private void Awake ()
    {

    }


    public void Init (int _mid)
    {
        // save the mid and it's position in the index
        mid = _mid;
        gameMidsIndex = MonsterIndex.Instance.GetGameMidIndex (mid);

        // set random sprite
        SetSpriteInParticleSystem (mid, gameMidsIndex);

    }


    // random
    void SetSpriteInParticleSystem (int _mid = -1, int _gameMidsIndex = -1)
    {
        //Debug.Log ("SetRandomSpriteInParticleSystem() mid =" + mid);

        // choose random
        if (_mid < 1) {
            // sprites to choose from
            totalSprites = MonsterIndex.Instance.monstersFromSheetsDistinct.Length;

            // make sure its divisible by 3
            spriteIndex = (int)Random.Range (0, totalSprites);
            while (spriteIndex % 3 != 0) {
                spriteIndex = (int)Random.Range (0, totalSprites);
            }
        } else {
            spriteIndex = _gameMidsIndex * 3;
        }


        // access particle system's TextureSheetAnimationModule
        ParticleSystem ps = GetComponent<ParticleSystem> ();
        ParticleSystem.TextureSheetAnimationModule tsam = ps.textureSheetAnimation;
        // SetSprite on the three
        tsam.SetSprite (0, MonsterIndex.Instance.monstersFromSheetsDistinct [spriteIndex + 0]);
        tsam.SetSprite (1, MonsterIndex.Instance.monstersFromSheetsDistinct [spriteIndex + 1]);
        tsam.SetSprite (2, MonsterIndex.Instance.monstersFromSheetsDistinct [spriteIndex + 2]);
    }



    //// old test version, before all monster sprites were combined into three large sheets 
    //void SetRandomSpritesFromArray ()
    //{
    //    // sprites to choose from
    //    totalSprites = MonsterIndex.Instance.monsters.Length;

    //    // make sure its divisible by 4 (to account for the texture that showed up in the list)
    //    spriteIndex = (int)Random.Range (0, totalSprites);
    //    while (spriteIndex % 4 != 0) {
    //        spriteIndex = (int)Random.Range (0, totalSprites);
    //    }
    //    // access particle system's TextureSheetAnimationModule
    //    ParticleSystem ps = GetComponent<ParticleSystem> ();
    //    ParticleSystem.TextureSheetAnimationModule tsam = ps.textureSheetAnimation;
    //    // SetSprite on the three
    //    tsam.SetSprite (0, MonsterIndex.Instance.monsters [spriteIndex + 1]);
    //    tsam.SetSprite (1, MonsterIndex.Instance.monsters [spriteIndex + 2]);
    //    tsam.SetSprite (2, MonsterIndex.Instance.monsters [spriteIndex + 3]);
    //}





}
