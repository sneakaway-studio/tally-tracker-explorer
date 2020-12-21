
using UnityEngine;
using System.Collections;
public class KeepInCameraView : MonoBehaviour {

    Camera cam;
    public Vector2 topLeftOffset;
    public Vector2 bottomRightOffset;

    private void Awake ()
    {
        cam = Camera.main;
    }

    void Update ()
    {
        Vector3 pos = cam.WorldToViewportPoint (transform.position);

        Debug.Log (pos);

        pos.x = Mathf.Clamp01 (pos.x);
        pos.y = Mathf.Clamp01 (pos.y);

        //if (pos.x < topLeftOffset.x) pos.x = topLeftOffset.x;
        //if (pos.y < topLeftOffset.y) pos.y = topLeftOffset.y;

        //if (pos.x > bottomRightOffset.x) pos.x = bottomRightOffset.x;
        //if (pos.y > bottomRightOffset.y) pos.y = bottomRightOffset.y;

        transform.position = cam.ViewportToWorldPoint (pos);
    }
}