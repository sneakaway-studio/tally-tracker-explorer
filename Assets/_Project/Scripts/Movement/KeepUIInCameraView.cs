
using UnityEngine;
using System.Collections;
public class KeepUIInCameraView : MonoBehaviour {

    public Canvas canvas;
    RectTransform rect;
    RectTransform parentRect;

    // rect width
    public float rectWidth;

    private void Awake ()
    {
        rect = GetComponent<RectTransform> ();
        parentRect = rect.parent.GetComponent<RectTransform> ();
    }

    void Update ()
    {

        Vector2 thisPos = rect.anchoredPosition;
        Vector2 parentPos = parentRect.anchorMax;

        rectWidth = rect.sizeDelta.x;


        if (parentPos.x < 0.05f) {
            thisPos = new Vector3 (rectWidth / 2, thisPos.y);
        } else if (parentPos.x > rect.sizeDelta.x) {
            thisPos = new Vector3 (-(rectWidth / 2), thisPos.y);
        } else {
            thisPos = new Vector3 (0, thisPos.y);
        }
        //Debug.Log (parentPos.x.ToString ());
        //Debug.Log (RectTransformUtility.PixelAdjustPoint (parentPos, parentRect, canvas));

        rect.anchoredPosition = thisPos;
    }
}