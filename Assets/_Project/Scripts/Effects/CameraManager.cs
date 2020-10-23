using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    // Main camera in scene
    public Camera mainCamera;

    // Player Manager in scene
    public PlayerManager playerManager;

    // Zoom distances
    public float zoomOutLevel = 25f;
    public float zoomInLevel = 10f;

    // Speed for movement
    public float speed = 15f;

    // Movement locks
    private bool cameraMoving = false;
    private bool cameraZooming = false;
    private bool cameraZoomed = false;

    // Player selection info
    private GameObject cameraTarget;
    private List<string> players = new List<string>();
    private int playersCurrentIndex = 0;
    private GameObject currentSelectionParticle;

    TallyInputSystem inputs;

    private void Awake()
    {
        // Assign actions to inputs
        inputs = new TallyInputSystem();
        inputs.Player.SelectLeft.performed += ctx => SelectLeft();
        inputs.Player.SelectRight.performed += ctx => SelectRight();
        inputs.Player.ZoomIn.started += ctx => StartCoroutine(ZoomIn());
        inputs.Player.ZoomOut.started += ctx => StartCoroutine(ZoomOut());
        inputs.Player.Enable();
    }

    private void Update()
    {
        // Stick camera to target after zooming in
        if (!cameraMoving && !cameraZooming && cameraZoomed)
        {
            mainCamera.transform.position = new Vector3(cameraTarget.transform.position.x, cameraTarget.transform.position.y, -10);
        }
    }

    /// <summary>
    /// Selects the previous player in the list of players
    /// </summary>
    void SelectLeft()
    {
        // If the camera is not moving
        if (!cameraMoving)
        {
            // Change the player index
            if (playersCurrentIndex == 0)
                playersCurrentIndex = players.Count - 1;
            else
                playersCurrentIndex--;

            // Switch cameraTarget and change particles
            Destroy(currentSelectionParticle);
            playerManager.playerDict.TryGetValue(players[playersCurrentIndex], out GameObject tempPlayer);
            currentSelectionParticle = (GameObject)Instantiate(playerManager.selectionParticle, tempPlayer.GetComponent<Player>().effects.transform, false);
            cameraTarget = tempPlayer.GetComponent<Player>().playerCharacter;

            // Transition camera if zoomed in
            if (cameraZoomed)
                StartCoroutine(Transition());
        }
    }

    /// <summary>
    /// Selects the next player in the list of players
    /// </summary>
    void SelectRight()
    {
        // If the camera is not moving
        if (!cameraMoving)
        {
            // Change the player index
            if (playersCurrentIndex == players.Count - 1)
                playersCurrentIndex = 0;
            else
                playersCurrentIndex++;

            // Switch cameraTarget and change particles
            Destroy(currentSelectionParticle);
            playerManager.playerDict.TryGetValue(players[playersCurrentIndex], out GameObject tempPlayer);
            currentSelectionParticle = (GameObject)Instantiate(playerManager.selectionParticle, tempPlayer.GetComponent<Player>().effects.transform, false);
            cameraTarget = tempPlayer.GetComponent<Player>().playerCharacter;

            // Transition camera if zoomed in
            if (cameraZoomed)
                StartCoroutine(Transition());
        }
    }

    /// <summary>
    /// Adds a player to the list of players
    /// </summary>
    /// <param name="username"> Name of player </param>
    public void AddPlayer(string username)
    {
        // Adds username to player list
        players.Add(username);

        // Makes new player the target if one is not selected
        if (currentSelectionParticle == null)
        {
            playerManager.playerDict.TryGetValue(players[playersCurrentIndex], out GameObject tempPlayer);
            cameraTarget = tempPlayer.GetComponent<Player>().playerCharacter;
            currentSelectionParticle = (GameObject)Instantiate(playerManager.selectionParticle, tempPlayer.GetComponent<Player>().effects.transform, false);
        }
    }

    /// <summary>
    /// Gives exponential graph
    /// </summary>
    /// <param name="t"> Passed time </param>
    /// <returns> Exponential graph at time t </returns>
    float EaseIn(float t)
    {
        return t * t;
    }

    /// <summary>
    /// Flips the graph upside down
    /// </summary>
    /// <param name="x"> Passed time </param>
    /// <returns> Upside down graph at time t </returns>
    float Flip(float x)
    {
        return 1 - x;
    }

    /// <summary>
    /// Flips the exponential graph upside down for easing out
    /// </summary>
    /// <param name="t"> Passed time </param>
    /// <returns> Flipped exponential graph at time t </returns>
    float EaseOut(float t)
    {
        return Flip(EaseIn(Flip(t)));
    }

    /// <summary>
    /// Gives graph that eases in and out
    /// </summary>
    /// <param name="t"> Passed time </param>
    /// <returns> Eased in/out graph at time t </returns>
    float EaseInOut(float t)
    {
        return Mathf.Lerp(EaseIn(t), EaseOut(t), t);

        //return Mathf.Pow(Mathf.Sin((Mathf.PI * t) / 2), 2);
    }



    /// <summary>
    /// Moves the camera to the cameraTarget
    /// </summary>
    IEnumerator Transition()
    {
        float t = 0.0f;
        Vector3 startingPos = mainCamera.transform.position;
        float longestD = Vector2.Distance(mainCamera.transform.position, cameraTarget.transform.position) / speed;

        // If the camera is not moving or zooming
        if (!cameraMoving && !cameraZooming)
        {
            // Gradually move the camera towards cameraTarget
            cameraMoving = true;
            while (t < 1.0f)
            {
                t += Time.deltaTime * (Time.timeScale / longestD);

                mainCamera.transform.position = Vector3.Lerp(startingPos, new Vector3(cameraTarget.transform.position.x, cameraTarget.transform.position.y, -10), EaseInOut(t));
                yield return 0;
            }
            cameraMoving = false;
        }

        /// ARCHIVED TRANSITION IN CASE YOU NEED IT LATER
        /// FEEL FREE TO DELETE AT ANY POINT
        //float longestD = Vector2.Distance(mainCamera.transform.position, cameraTarget.transform.position);
        //float d = Vector2.Distance(mainCamera.transform.position, cameraTarget.transform.position);

        //// If the camera is not moving or zooming
        //if (!cameraMoving && !cameraZooming)
        //{
        //    // Gradually move the camera towards cameraTarget
        //    cameraMoving = true;
        //    while (d > 0.5f || !cameraZoomed)
        //    {
        //        Vector3 diff = new Vector3(cameraTarget.transform.position.x - mainCamera.transform.position.x, cameraTarget.transform.position.y - mainCamera.transform.position.y, 0);
        //        mainCamera.transform.position += diff.normalized * speed * Time.deltaTime;
        //        d = Vector2.Distance(mainCamera.transform.position, cameraTarget.transform.position);
        //        yield return 0;
        //    }
        //    cameraMoving = false;
        //}
    }

    /// <summary>
    /// Moves the camera to the targetPosition
    /// </summary>
    /// <param name="targetPosition"> Vector3 location to move the camera to </param>
    IEnumerator Transition(Vector3 targetPosition)
    {
        float t = 0.0f;
        Vector3 startingPos = mainCamera.transform.position;
        float longestD = Vector2.Distance(mainCamera.transform.position, targetPosition) / speed;

        // If the camera is not moving or zooming
        if (!cameraMoving && !cameraZooming)
        {
            // Gradually move the camera towards cameraTarget
            cameraMoving = true;
            while (t < 1.0f)
            {
                t += Time.deltaTime * (Time.timeScale / longestD);

                mainCamera.transform.position = Vector3.Lerp(startingPos, new Vector3(targetPosition.x, targetPosition.y, -10), EaseInOut(t));
                yield return 0;
            }
            cameraMoving = false;
        }
    }

    /// <summary>
    /// Zooms the camera in to zoomInLevel gradually
    /// </summary>
    IEnumerator ZoomIn()
    {
        float t = 0.0f;
        float startingZoom = mainCamera.orthographicSize;
        float longestD = Vector2.Distance(mainCamera.transform.position, cameraTarget.transform.position) / speed;

        // If the camera is not moving, zooming, or currently zoomed in
        if (!cameraMoving && !cameraZooming && !cameraZoomed)
        {
            // Move the camera towards the cameraTarget
            StartCoroutine(Transition());
            
            // Gradually reduce the size of the camera
            cameraZooming = true;
            while (t < 1.0f)
            {
                t += Time.deltaTime * (Time.timeScale / longestD);

                mainCamera.orthographicSize = Mathf.Lerp(startingZoom, zoomInLevel, t);
                yield return 0;
            }
            cameraZooming = false;
            cameraZoomed = true;
        }
    }

    /// <summary>
    /// Zooms the camera out to zoomOutLevel gradually
    /// </summary>
    IEnumerator ZoomOut()
    {
        float t = 0.0f;
        float startingZoom = mainCamera.orthographicSize;
        float longestD = Vector2.Distance(mainCamera.transform.position, new Vector3(0, 1, -10)) / speed;

        // If the camera is not moving, zooming, or currently zoomed in
        if (!cameraMoving && !cameraZooming && cameraZoomed)
        {
            // Move the camera towards the center
            StartCoroutine(Transition(new Vector3(0, 1, -10)));

            // Gradually increase the size of the camera
            cameraZooming = true;
            cameraZoomed = false;
            while (t < 1.0f)
            {
                t += Time.deltaTime * (Time.timeScale / longestD);

                mainCamera.orthographicSize = Mathf.Lerp(startingZoom, zoomOutLevel, t);
                yield return 0;
            }
            cameraZooming = false;
        }
    }

}
