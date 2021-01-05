using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trail : MonoBehaviour {


    public GameObject avatar;
    public TrailRenderer trailRenderer;
    public int mid;
    public int passes;
    public int siblingCount;
    public int trailIndex;

    // SIZE & POSITION

    public float trailWidth;
    float [] positionsByIndex = { -1f, -0.5f, 0, .5f, 1f };
    // Maximum position for overall trail edge
    public float positionMax = 1.5f;
    // Minimum position for overall trail edge
    public float positionMin = -1.5f;
    // range between the positionMax and positionMin
    float positionRange = 1.5f - -1.5f;
    // Extra amount added to width to hide background
    public float widthExtra = 0.01f;
    // length scale
    public float lengthScale = 0.9f;




    private void Awake ()
    {
        // get renderer
        trailRenderer = GetComponent<TrailRenderer> ();
        // disable until position is set 
        trailRenderer.enabled = false;
        trailRenderer.emitting = false;

        // set trail color 
        SetColor ();
    }

    /**
     *  Set color on start 
     */
    public void SetColor ()
    {
        // get from mid
        if (mid > 0) {
            trailRenderer.material.color = ColorManager.GetColorFromDict (mid);
        } else {
            // testing
            // random from array 
            //trailRenderer.material.color = ColorManager.GetColorFromArray (-1);
            // random from dictionary 
            trailRenderer.material.color = ColorManager.GetColorFromDict (-1);
        }
    }


    public void UpdatePositions ()
    {
        //Debug.Log ("Trail.UpdatePositions() called");


        // POSITION 

        // set siblings count (other trails)
        siblingCount = transform.parent.childCount;

        // set trail index by its position in the hierarchy
        trailIndex = transform.GetSiblingIndex ();

        // update array length to total number of trails
        positionsByIndex = new float [siblingCount];

        // calculate range between each trail
        float trailsRange = positionRange / (siblingCount);

        // create positions, placing trails so the outer edges are touching positionMin and positionMax
        for (int i = 0; i < siblingCount; i++) {
            // starts with positionMin plus an offset that lets the trail edge touch it
            // the 0.5 places it in the middle of the range, or something
            // tbh I don't really know, I'm just glad it works
            positionsByIndex [i] = positionMin + (trailsRange * (i + 0.5f));
        }

        // set offset based on index
        transform.localPosition = new Vector3 (0, positionsByIndex [trailIndex], 0);


        // WIDTH

        // set width based on the number of trails
        //trailWidth = siblingCount * .1f;

        // set width based on the range + extra to cover the background
        trailWidth = Mathf.Min (trailsRange + widthExtra, 1.4f);

        // set start and end width 
        trailRenderer.startWidth = trailWidth; // 5 = 0.45f
        trailRenderer.endWidth = trailWidth;


        // OPTIONS

        // set vertices - how smooth the corners are
        trailRenderer.numCornerVertices = 10;
        trailRenderer.numCapVertices = 5;

        // set length - how long trail lasts
        trailRenderer.time = siblingCount * lengthScale;

        //trailRenderer.sortingOrder = avatar.GetComponent<SpriteRenderer> ().sortingOrder;

        // enable and start emitting after all parameters are set
        trailRenderer.enabled = true;
        trailRenderer.emitting = true;
    }
}