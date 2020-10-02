using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : Singleton<MonsterManager> {
    // singleton
    protected MonsterManager () { }
    public static new MonsterManager Instance;



    public Sprite [] monsterSprites;


    public Sprite RandomSprite ()
    {
        return monsterSprites [Random.Range (0, (monsterSprites.Length - 1))];
    }


}
