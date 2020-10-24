using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEditor;
using TMPro;


/**
 *  Manage game "resolution" - runs in play mode AND editor
 */

[ExecuteAlways]
public class ResolutionManager : MonoBehaviour {



    [Space (10)]
    [Header ("CANVAS")]
    // the canvas from the UI

    [SerializeField]
    private float cameraSize;

    [Tooltip ("The RectTransform on the Canvas that controls the screen/UI")]
    public RectTransform canvasRect;

    [Tooltip ("Canvas resolution")]
    public Vector2 canvasResolution;




    [Space (10)]
    [Header ("UNITY PLAYER")]
    // states of the Unity 'player' or 'game view' window

    [Tooltip ("Resolution of the player window")]
    // - in full-screen it is also the current resolution
    // - unfortunately named using "Screen" https://docs.unity3d.com/ScriptReference/Screen-height.html
    public Vector2 playerResolution;

    //[Tooltip ("Resolution of the game view")]
    //// - set in Unity, stored in Application Support (Mac) (seems to always be same as above)
    //public Vector2 gameViewResolution;

    [Tooltip ("The size of the player view (we will set) in unity units")]
    // depends on camera size
    public Vector3 playerViewSize;

    [Tooltip ("Aspect ratio of the player")]
    // aspect ratio W:1 - relationship between resolution w:h (e.g. 1.33 (4:3) or 1.77 (16:9))
    public float playerAspectRatio;

    [Tooltip ("Whether or not player is in fullscreen mode")]
    public bool playerFullScreen;




    [Space (10)]
    [Header ("----- DEVICE -----")]
    // states of the screen / device display

    [Tooltip ("Device resolution (px)")]
    // - the actual screen or "display" this project is running on
    // - if the player is running in window mode, this returns the current resolution of the desktop
    // - unfortunately named Screen.currentdeviceResolution (as opposed to another, less recent one?)
    public Vector2 deviceResolution;

    [Tooltip ("Device aspect ratio")]
    // - W:1 - relationship between resolution w:h (e.g. 1.33 (4:3) or 1.77 (16:9))
    public float deviceAspectRatio;




    // OBJECTS - UPDATE IF PARAMETERS CHANGE

    [Space (10)]
    [Header ("OBJECT REFERENCES")]

    [Tooltip ("Collider that defines the volume of the visible game world")]
    public BoxCollider worldContainerCollider;
    public TMP_Text resolutionReportText;





    private void Awake ()
    {
        // for some reason using the collider on this object caused the editor to crash every time
        worldContainerCollider = GetComponent<BoxCollider> ();

        StartCoroutine (SendResolutionUpdatedEvent ());
    }


    private void Update ()
    {

        // and if player resolution has changed
        if (playerResolution.x != Screen.width || playerResolution.y != Screen.height) {
            //Debug.Log ("ResolutionManager.Update() change " + playerResolution.ToString ());

            StartCoroutine (SendResolutionUpdatedEvent ());
        }
    }

    IEnumerator SendResolutionUpdatedEvent ()
    {
        //Debug.Log ("ResolutionManager.SendResolutionUpdatedEvent() - resolution has changed to " + playerResolution.ToString ());
        // if application is playing 
        if (Application.IsPlaying (gameObject)) {
            DebugManager.Instance.UpdateDisplay ("ResolutionManager.SendResolutionUpdatedEvent() change " + playerResolution.ToString ());
        }
        // update the parameters
        UpdateResolutionParams ();

        // update collider
        UpdateColliderSize ();

        UpdateReport ();

        yield return new WaitForSeconds (.2f);


        // if application is playing 
        //if (Application.IsPlaying (gameObject)) {

        // trigger data updated event
        EventManager.TriggerEvent ("ResolutionUpdated");
        //}
    }



    /**
     *  Update all the resolution parameters
     */
    public void UpdateResolutionParams ()
    {
        //Debug.Log ("ResolutionManager.UpdateResolutionParams()");


        // CAMERA / CANVAS
        cameraSize = Camera.main.orthographicSize;
        canvasResolution = new Vector2 (canvasRect.sizeDelta.x, canvasRect.sizeDelta.y);

        // PLAYER PARAMS
        playerResolution = new Vector2 (Screen.width, Screen.height);
        //gameViewResolution = GetMainGameViewSize ();
        playerViewSize = GetPlayerViewSize ();
        playerAspectRatio = playerResolution.x / playerResolution.y;
        playerFullScreen = Screen.fullScreen;

        // DEVICE PARAMS
        deviceResolution = new Vector2 (Screen.currentResolution.width, Screen.currentResolution.height);
        deviceAspectRatio = deviceResolution.x / deviceResolution.y;
    }

    /**
     *  Update the text in the UI
     */
    void UpdateReport ()
    {
        // update text in control panel
        string report =
            //"canvasResolution (px): " + canvasResolution.ToString () + "\n" + 
            "playerResolution (px): " + playerResolution.ToString () + "\n" +
            //"gameViewResolution (px): " + gameViewResolution.ToString () +
            "playerViewSize (units): " + playerViewSize.ToString () + "\n" +
            "playerAspectRatio: " + (Mathf.Round (playerAspectRatio * 100) / 100).ToString () + ":1 \n" +
            "playerFullScreen: " + playerFullScreen.ToString () + "\n" +
            "deviceResolution: " + deviceResolution.ToString ();
        resolutionReportText.text = report;
    }

    void UpdateColliderSize ()
    {
        //Debug.Log ("ResolutionManager.UpdateColliderSize() [1]");

        if (playerViewSize.x > 0 && playerViewSize.y > 0) {
            // set new size, making sure there isn't a negative number
            worldContainerCollider.size = new Vector3 (playerViewSize.x, playerViewSize.y, worldContainerCollider.size.z);

            //Debug.Log ("ResolutionManager.UpdateColliderSize() [2]");
        }
        //Debug.Log ("ResolutionManager.UpdateColliderSize() playerViewSize = " + playerViewSize.ToString ());
        //Debug.Log ("ResolutionManager.UpdateColliderSize() worldContainerCollider.size = " + worldContainerCollider.size.ToString ());
        //Debug.Log ("ResolutionManager.UpdateColliderSize() worldContainerCollider.bounds = " + worldContainerCollider.bounds.ToString ());
    }

    /**
     *  Return the player viewing volume size (in Unity units)
     */
    public static Vector3 GetPlayerViewSize ()
    {
        //Debug.Log ("ResolutionManager.GetPlayerViewSize()");

        // The orthographicSize is half of the vertical viewing volume size
        float height = Camera.main.orthographicSize * 2;
        // The horizontal size of the viewing volume depends on the aspect ratio so...
        // width = height * screen resolution aspect ratio
        float width = height * Screen.width / Screen.height;
        // return both
        return new Vector2 (width, height);
    }

    /**
     *  Return the editor Game View window size - works in editor
     */
    static Vector2 GetMainGameViewSize ()
    {
        System.Type T = System.Type.GetType ("UnityEditor.GameView,UnityEditor");
        System.Reflection.MethodInfo GetSizeOfMainGameView = T.GetMethod ("GetSizeOfMainGameView", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);
        System.Object Res = GetSizeOfMainGameView.Invoke (null, null);
        return (Vector2)Res;
    }




















}



