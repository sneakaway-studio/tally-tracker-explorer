﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/**
 *  Move the timeline visualization with the buffer and history counts
 */
public class TimelineViz : MonoBehaviour {

    // reference to script with the vars
    public Timeline timeline;

    // object to move
    public RectTransform historyRectTransform;

    // vector2 for new position
    public Vector2 newPosNormalized;

    // keep track of most recent
    public int lastHistoryCount;



    private void Update ()
    {

        // if there are events and we have recently increased historyCount
        if (timeline.totalEvents > 0 && lastHistoryCount != timeline.historyCount) {

            //Debug.Log ("hi " + timeline.historyCount / (float)timeline.totalEvents);

            // get new normalized position
            newPosNormalized = new Vector2 (timeline.historyCount / (float)timeline.totalEvents, historyRectTransform.anchorMax.y);

            //if (timeline.historyCount == 0) {
            //    // snap to new position
            //    historyRectTransform.anchorMax = new Vector2 (0, historyRectTransform.anchorMax.y);
            //} else {
            // start coroutine to lerp from current to new
            StartCoroutine (LerpPosition (historyRectTransform, newPosNormalized, 1f));
            //}

            // store this history count
            lastHistoryCount = timeline.historyCount;

            //// if the buffer is going to be reset soon
            //if (lastHistoryCount >= timeline.totalEvents) {
            //    // reset
            //    lastHistoryCount = 0;
            //}
        }

    }

    /**
     *  Lerp to new position (in this case, the target.anchorMax
     */
    IEnumerator LerpPosition (RectTransform target, Vector2 targetPosition, float duration)
    {
        float time = 0;
        Vector2 startPosition = target.anchorMax;

        while (time < duration) {
            target.anchorMax = Vector2.Lerp (startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        target.anchorMax = targetPosition;
    }





}
