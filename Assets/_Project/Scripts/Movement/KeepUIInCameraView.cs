
using UnityEngine;
using System.Collections;
public class KeepUIInCameraView : MonoBehaviour {

    public Canvas canvas;
    RectTransform rect;
    RectTransform parentRect;

    private void Awake ()
    {
        rect = GetComponent<RectTransform> ();
        parentRect = rect.parent.GetComponent<RectTransform> ();
    }

    void Update ()
    {

        Vector2 thisPos = rect.anchoredPosition;
        Vector2 parentPos = parentRect.anchorMax;

        if (parentPos.x < 0.05f) {
            thisPos = new Vector3 (100, thisPos.y);
        } else if (parentPos.x > 0.95f) {
            thisPos = new Vector3 (-100, thisPos.y);
        } else {
            thisPos = new Vector3 (0, thisPos.y);
        }
        //Debug.Log (parentPos.x.ToString ());
        //Debug.Log (RectTransformUtility.PixelAdjustPoint (parentPos, parentRect, canvas));

        rect.anchoredPosition = thisPos;
    }
}