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

    // Duration times for movement
    public float transitionDuration = 1f;
    public float zoomDuration = 1f;

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
    /// Moves the camera to the cameraTarget
    /// </summary>
    IEnumerator Transition()
    {
        float t = 0.0f;
        Vector3 startingPos = mainCamera.transform.position;

        // If the camera is not moving or zooming
        if (!cameraMoving && !cameraZooming)
        {
            // Gradually move the camera towards cameraTarget
            cameraMoving = true;
            while (t < 1.0f)
            {
                t += Time.deltaTime * (Time.timeScale / transitionDuration);

                mainCamera.transform.position = Vector3.Lerp(startingPos, new Vector3(cameraTarget.transform.position.x, cameraTarget.transform.position.y, -10), t);
                yield return 0;
            }
            cameraMoving = false;
        }
    }

    /// <summary>
    /// Moves the camera to the targetPosition
    /// </summary>
    /// <param name="targetPosition"> Vector3 location to move the camera to </param>
    IEnumerator Transition(Vector3 targetPosition)
    {
        float t = 0.0f;
        Vector3 startingPos = mainCamera.transform.position;

        // If the camera is not moving or zooming
        if (!cameraMoving && !cameraZooming)
        {
            // Gradually move the camera towards cameraTarget
            cameraMoving = true;
            while (t < 1.0f)
            {
                t += Time.deltaTime * (Time.timeScale / transitionDuration);


                mainCamera.transform.position = Vector3.Lerp(startingPos, targetPosition, t);
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

        // If the camera is not moving, zooming, or currently zoomed in
        if (!cameraMoving && !cameraZooming && !cameraZoomed)
        {
            // Move the camera towards the cameraTarget
            StartCoroutine(Transition());
            
            // Gradually reduce the size of the camera
            cameraZooming = true;
            while (t < 1.0f)
            {
                t += Time.deltaTime * (Time.timeScale / zoomDuration);

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
                t += Time.deltaTime * (Time.timeScale / zoomDuration);

                mainCamera.orthographicSize = Mathf.Lerp(startingZoom, zoomOutLevel, t);
                yield return 0;
            }
            cameraZooming = false;
        }
    }
}
