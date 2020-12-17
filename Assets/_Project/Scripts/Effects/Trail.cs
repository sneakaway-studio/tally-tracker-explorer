using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trail : MonoBehaviour {

    public GameObject avatar;
    public TrailRenderer trailRenderer;
    public int mid;
    public int siblingCount;
    public int trailIndex;

    // SIZE & POSITION

    public float trailWidth;
    float [] positionsByIndex = { -1f, -0.5f, 0, .5f, 1f };
    // Maximum position for overall trail edge
    public float positionMax = 1.5f;
    // Minimum position for overall trail edge
    public float positionMin = -1.5f;
    // Extra amount added to width to hide background
    public float widthExtra = 0.01f;
    // length scale
    public float lengthScale = 0.9f;






    private void Awake ()
    {
        // get renderer
        trailRenderer = GetComponent<TrailRenderer> ();
        trailRenderer.enabled = false;
    }





    public void Init ()
    {
        //Debug.Log ("Init() called on Trail");

        // testing 
        //trailRenderer.material.color = ColorManager.GetColorFromArray (-1);
        trailRenderer.material.color = ColorManager.GetColorFromDict (-1);


        // number of siblings (other trails)
        siblingCount = transform.parent.childCount;
        // define the width by the number of trails
        trailWidth = siblingCount * .1f;
        // set the position of this trail by its index in the hierarchy
        trailIndex = transform.GetSiblingIndex ();


        // range between the positionMax and positionMin
        float positionRange = positionMax - positionMin;

        // sets positionsByIndex array by number of trails
        positionsByIndex = new float [siblingCount];

        // calculate range between each trail
        float trailsRange = positionRange / (siblingCount);

        // place trails so the outer edges are touching positionMin and positionMax
        for (int i = 0; i < siblingCount; i++) {
            // starts with positionMin plus an offset that lets the trail edge touch it
            positionsByIndex [i] = positionMin + (trailsRange * (i + 0.5f)); // the 0.5 places it in the middle of the range, or something
        }                                                                   // tbh I don't really know, I'm just glad it works

        // set the trail width to the range plus a 'lil extra to cover the background
        trailWidth = trailsRange + widthExtra;

        // how long trail lasts, a.k.a. its length
        trailRenderer.time = siblingCount * lengthScale;

        // set offset based on index
        transform.localPosition = new Vector3 (0, positionsByIndex [trailIndex], 0);

        // set w & h
        trailRenderer.startWidth = trailWidth; // 5 = 0.45f
        trailRenderer.endWidth = trailWidth;

        // set vertices
        trailRenderer.numCornerVertices = 10;
        trailRenderer.numCapVertices = 5;

        //trailRenderer.sortingOrder = avatar.GetComponent<SpriteRenderer> ().sortingOrder;

        trailRenderer.enabled = true;
    }


}
