using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{


    public string username;
    public string avatarPath;


    public void Init(string username, string avatarPath)
    {
        this.username = username;
        this.avatarPath = avatarPath;
    }


}
