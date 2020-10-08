using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trail : MonoBehaviour {

    public GameObject avatar;
    public TrailRenderer trailRenderer;
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


    // COLOR

    public string trailColor;
    Color color;

    string [] colors = {
        "5f1475", "5f1475", "f4e713", "8820aa", "42da82", "f413bc", "3f0236", "42da82", "31a8cb", "42da82", "f413bc", "ec391a", "6261a8", "ec391a", "4d7bbd", "4d7bbd", "6261a8", "5f1475", "31a8cb", "ec391a", "6939ac", "6261a8", "6939ac", "48daa3", "ec391a", "5f40bd", "f413bc", "5f40bd", "6939ac", "074381", "5f40bd", "6939ac", "6939ac", "074381", "5f40bd", "de2319", "058eb8", "5f1475", "6939ac", "5f1475", "074381", "de2319", "463260", "ae1ff1", "ae1ff1", "ae1ff1", "69004b", "5300e3", "129740", "f413bc", "ae1ff1", "de2319", "418fb0", "418fb0", "5f1475", "074381", "ae1ff1", "463260", "a43a9f", "0356d8", "ae1ff1", "f3af1f", "5f1475", "a6134c", "02b65c", "ef4138", "de2dca", "63fbf0", "63fbf0", "63fbf0", "5300e3", "5f1475", "8336bd", "3957c9", "3957c9", "a6134c", "a6134c", "a6134c", "a6134c", "a6134c", "ce218d", "074381", "5f1475", "ef4138", "ef4138", "ef4138", "90c053", "ef4138", "5fadd1", "f41182", "5f1475", "ef4138", "5f1475", "42da82", "5300e3", "5300e3", "5300e3", "5bd6fa", "1f4fbc", "5300e3", "60139b", "f413bc", "5f1475"
    };



    private void Awake ()
    {
        // get renderer
        trailRenderer = GetComponent<TrailRenderer> ();
        trailRenderer.enabled = false;
    }




    public void Init ()
    {
        //Debug.Log ("Init() called on TrailController");


        // get random color string 
        trailColor = colors [(int)Random.Range (0, colors.Length - 1)];
        // if hex parses
        if (ColorUtility.TryParseHtmlString ("#" + trailColor + "FF", out color)) {
            // change material color
            trailRenderer.material.color = color;
        }

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
