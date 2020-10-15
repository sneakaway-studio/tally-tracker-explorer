using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEditor;


/**
 *  Manage game "resolution"
 */
public class ResolutionManager : MonoBehaviour {

    public float aspectRatio;
    public Vector2 screenResolution;



    private void Awake ()
    {
        Debug.Log ("Screen: " + Screen.width + "," + Screen.height);

        screenResolution = GetScreenSize ();
        Debug.Log ("screenResolution: " + screenResolution.x + "," + screenResolution.y);


        print (GetMainGameViewSize ());
    }


    public static Vector2 GetScreenSize ()
    {
        //  the orthographicSize is half of the viewing volume size.
        float height = Camera.main.orthographicSize * 2;
        // width = height * screen aspect ratio
        float width = height * Screen.width / Screen.height;

        return new Vector2 (width, height);

    }



    // get size (in editor window only?)
    public static Vector2 GetMainGameViewSize ()
    {
        System.Type T = System.Type.GetType ("UnityEditor.GameView,UnityEditor");
        System.Reflection.MethodInfo GetSizeOfMainGameView = T.GetMethod ("GetSizeOfMainGameView", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);
        System.Object Res = GetSizeOfMainGameView.Invoke (null, null);
        return (Vector2)Res;
    }



}



