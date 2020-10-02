using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour {

    // base class for monster 

    public int mid;
    FrameAnimation frameAnimation;


    private void Awake ()
    {
        frameAnimation = GetComponent<FrameAnimation> ();

        //frameAnimation.sprites = MonsterManager.RandomSprite ();
    }


}
