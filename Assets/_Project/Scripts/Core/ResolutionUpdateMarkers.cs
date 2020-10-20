using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 *  Update components based on parameters from the ResolutionManager
 */

[ExecuteAlways]
public class ResolutionUpdateMarkers : ResolutionUpdateBase {

    [Tooltip ("Show markers")]
    public bool showMarkers;

    [Tooltip ("Array (grid) of resolution anchors")]
    public Vector2 [] ninePointGrid;

    [Tooltip ("Array of marker references")]
    public GameObject [] resolutionMarkers;


    protected override void Awake ()
    {
        base.Awake ();

        // set original scale (after everything is computer) based on startup aspect ratio
        UpdateResolution ();
    }

    protected override void UpdateResolution ()
    {
        //base.UpdateResolution ();

        // update marker positions
        UpdateMarkerPositions ();
    }



    void UpdateMarkerPositions ()
    {
        //Debug.Log ("UpdateMarkerPositions()");


        // if application is playing 
        if (Application.IsPlaying (gameObject)) {
            // show // hide markers
            if (showMarkers != DebugManager.status) {
                showMarkers = DebugManager.status;
                for (int i = 0; i < resolutionMarkers.Length; i++) {
                    resolutionMarkers [i].GetComponent<SpriteRenderer> ().enabled = showMarkers;
                }
            }
        }

        // display the nine point grid
        ninePointGrid = GetPlanePointGrid (resolutionManager.worldContainerCollider.bounds);
        for (int i = 0; i < ninePointGrid.Length; i++) {
            resolutionMarkers [i].transform.localPosition = new Vector3 (ninePointGrid [i].x, ninePointGrid [i].y, resolutionMarkers [0].transform.localPosition.z);
        }

    }



    /**
     *  Get 9 points on a plane
     */
    public Vector2 [] GetPlanePointGrid (Bounds b)
    {
        Vector2 [] p = new Vector2 [9];

        p [0] = new Vector2 (b.min.x, b.max.y); // top left
        p [1] = new Vector2 (b.center.x, b.max.y); // top center
        p [2] = new Vector2 (b.max.x, b.max.y); // top right

        p [3] = new Vector2 (b.min.x, b.center.y); // center left
        p [4] = new Vector2 (b.center.x, b.center.y); // center center
        p [5] = new Vector2 (b.max.x, b.center.y); // center right

        p [6] = new Vector2 (b.min.x, b.min.y); // bottom left
        p [7] = new Vector2 (b.center.x, b.min.y); // bottom center
        p [8] = new Vector2 (b.max.x, b.min.y); // bottom right

        return p;
    }







    void DrawBounds (Bounds b, float delay = 0)
    {
        // bottom
        var p1 = new Vector3 (b.min.x, b.min.y, b.min.z);
        var p2 = new Vector3 (b.max.x, b.min.y, b.min.z);
        var p3 = new Vector3 (b.max.x, b.min.y, b.max.z);
        var p4 = new Vector3 (b.min.x, b.min.y, b.max.z);

        Debug.DrawLine (p1, p2, Color.blue, delay);
        Debug.DrawLine (p2, p3, Color.red, delay);
        Debug.DrawLine (p3, p4, Color.yellow, delay);
        Debug.DrawLine (p4, p1, Color.magenta, delay);

        // top
        var p5 = new Vector3 (b.min.x, b.max.y, b.min.z);
        var p6 = new Vector3 (b.max.x, b.max.y, b.min.z);
        var p7 = new Vector3 (b.max.x, b.max.y, b.max.z);
        var p8 = new Vector3 (b.min.x, b.max.y, b.max.z);

        Debug.DrawLine (p5, p6, Color.blue, delay);
        Debug.DrawLine (p6, p7, Color.red, delay);
        Debug.DrawLine (p7, p8, Color.yellow, delay);
        Debug.DrawLine (p8, p5, Color.magenta, delay);

        // sides
        Debug.DrawLine (p1, p5, Color.white, delay);
        Debug.DrawLine (p2, p6, Color.gray, delay);
        Debug.DrawLine (p3, p7, Color.green, delay);
        Debug.DrawLine (p4, p8, Color.cyan, delay);
    }


    void UpdateBoundsDisplay (Bounds b)
    {
        Debug.DrawLine (new Vector3 (b.min.x, b.min.y, b.min.z), new Vector3 (b.max.x, b.min.y, b.min.z), Color.red);
        Debug.DrawLine (new Vector3 (b.max.x, b.min.y, b.min.z), new Vector3 (b.max.x, b.max.y, b.min.z), Color.red);
        Debug.DrawLine (new Vector3 (b.max.x, b.max.y, b.min.z), new Vector3 (b.min.x, b.max.y, b.min.z), Color.red);
        Debug.DrawLine (new Vector3 (b.min.x, b.max.y, b.min.z), new Vector3 (b.min.x, b.min.y, b.min.z), Color.red);
        Debug.DrawLine (b.min, b.max, Color.red);
    }






}
