using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    // Main camera in scene
    public Camera mainCamera;

    // Player Manager in scene
    public PlayerManager playerManager;

    public float transitionDuration = 1f;
    public float zoomDuration = 1f;

    private bool cameraMoving = false;
    private bool cameraZooming = false;
    private bool cameraZoomed = false;
    private GameObject cameraTarget;
    private List<string> players = new List<string>();
    private int playersCurrentIndex = 0;
    private GameObject currentSelectionParticle;

    TallyInputSystem inputs;

    private void Awake()
    {
        inputs = new TallyInputSystem();
        inputs.Player.SelectLeft.performed += ctx => CameraSelectLeft();
        inputs.Player.SelectRight.performed += ctx => CameraSelectRight();
        inputs.Player.ZoomIn.started += ctx => StartCoroutine(ZoomIn());
        inputs.Player.ZoomOut.started += ctx => StartCoroutine(ZoomOut());
        inputs.Player.Enable();
    }

    private void Update()
    {
        if (!cameraMoving && !cameraZooming && cameraZoomed)
        {
            mainCamera.transform.position = new Vector3(cameraTarget.transform.position.x, cameraTarget.transform.position.y, -10);
        }
        //else
        //{
        //targetPosition = new Vector3(cameraTarget.transform.position.x, cameraTarget.transform.position.y, -10);
        //StartCoroutine(Transition());
        //mainCamera.orthographicSize = 10;
        //if (currentSelectionParticle != null) currentSelectionParticle.SetActive(false);
        //}

    }

    void CameraSelectLeft()
    {
        if (!cameraMoving)
        {
            if (playersCurrentIndex == 0)
                playersCurrentIndex = players.Count - 1;
            else
                playersCurrentIndex--;

            Destroy(currentSelectionParticle);
            playerManager.playerDict.TryGetValue(players[playersCurrentIndex], out GameObject tempPlayer);
            currentSelectionParticle = (GameObject)Instantiate(playerManager.selectionParticle, tempPlayer.GetComponent<Player>().effects.transform, false);
            cameraTarget = tempPlayer.GetComponent<Player>().playerCharacter;

            if (cameraZoomed)
                StartCoroutine(Transition());
        }
    }

    void CameraSelectRight()
    {
        if (!cameraMoving)
        {
            if (playersCurrentIndex == players.Count - 1)
                playersCurrentIndex = 0;
            else
                playersCurrentIndex++;

            Destroy(currentSelectionParticle);
            playerManager.playerDict.TryGetValue(players[playersCurrentIndex], out GameObject tempPlayer);
            currentSelectionParticle = (GameObject)Instantiate(playerManager.selectionParticle, tempPlayer.GetComponent<Player>().effects.transform, false);
            cameraTarget = tempPlayer.GetComponent<Player>().playerCharacter;

            if (cameraZoomed)
                StartCoroutine(Transition());
        }
    }

    public void AddPlayer(string username)
    {
        players.Add(username);
        playerManager.playerDict.TryGetValue(players[playersCurrentIndex], out GameObject tempPlayer);
        cameraTarget = tempPlayer.GetComponent<Player>().playerCharacter;
        if (currentSelectionParticle == null) currentSelectionParticle = (GameObject)Instantiate(playerManager.selectionParticle, tempPlayer.GetComponent<Player>().effects.transform, false);
    }


    IEnumerator Transition()
    {
        float t = 0.0f;
        Vector3 startingPos = mainCamera.transform.position;

        if (!cameraMoving && !cameraZooming)
        {
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

    IEnumerator Transition(Vector3 targetPosition)
    {
        float t = 0.0f;
        Vector3 startingPos = mainCamera.transform.position;

        if (!cameraMoving && !cameraZooming)
        {
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

    
    IEnumerator ZoomIn()
    {
        float t = 0.0f;
        float startingZoom = mainCamera.orthographicSize;
        if (!cameraMoving && !cameraZooming && !cameraZoomed)
        {
            StartCoroutine(Transition());
            cameraZooming = true;
            while (t < 1.0f)
            {
                t += Time.deltaTime * (Time.timeScale / zoomDuration);

                mainCamera.orthographicSize = Mathf.Lerp(startingZoom, 10, t);
                yield return 0;
            }
            cameraZooming = false;
            cameraZoomed = true;
        }
    }

    
    IEnumerator ZoomOut()
    {
        float t = 0.0f;
        float startingZoom = mainCamera.orthographicSize;
        if (!cameraMoving && !cameraZooming && cameraZoomed)
        {
            StartCoroutine(Transition(new Vector3(0, 1, -10)));
            cameraZooming = true;
            cameraZoomed = false;
            while (t < 1.0f)
            {
                t += Time.deltaTime * (Time.timeScale / zoomDuration);

                mainCamera.orthographicSize = Mathf.Lerp(startingZoom, 25, t);
                yield return 0;
            }
            cameraZooming = false;
        }
    }
}
