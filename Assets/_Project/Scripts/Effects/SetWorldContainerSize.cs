using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetWorldContainerSize : MonoBehaviour {


    Camera cam;
    BoxCollider boxCollider;
    public Vector3 newSize;



    void Awake ()
    {
        cam = Camera.main;
        boxCollider = GetComponent<BoxCollider> ();

        Vector2 cameraBounds = ReturnCameraBounds ();

        newSize = new Vector3 (cameraBounds.x, cameraBounds.y, 10);


        transform.localScale = newSize;
    }



    Vector2 ReturnCameraBounds ()
    {


        float aspectRatio = (float)Screen.width / (float)Screen.height;
        float cameraHeight = cam.orthographicSize * 2;

        //return new Vector2 (
        //     Camera.main.transform.position,
        //     new Vector3 (cameraHeight * screenAspect, cameraHeight, 0)
        // );


        Debug.Log ("aspectRatio = " + aspectRatio);
        Debug.Log ("cameraHeight = " + cameraHeight);
        Debug.Log ("bounds = " + new Vector2 (cam.orthographicSize * aspectRatio, cameraHeight));


        return new Vector2 (cam.orthographicSize * aspectRatio, cameraHeight);


    }



}
