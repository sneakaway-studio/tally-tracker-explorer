using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;

/**
 *  Updates resolution, fullscreen, and volume
 *  - Brackey's to the rescue https://www.youtube.com/watch?v=YOaYQrN1oYQ
 */
public class SettingsManager : MonoBehaviour {

    public Toggle fullscreenToggle;
    public Toggle autostartToggle;

    public bool isFullscreen;
    public bool autostart;

    public AudioMixer audioMixer;

    public TMP_Dropdown resolutionDropdown;

    Resolution [] resolutions;


    private void Awake ()
    {
        // update the values in the UI that are set in the inspector
        fullscreenToggle.isOn = isFullscreen;
        autostartToggle.isOn = autostart;
    }


    private void Start ()
    {
        // get the list of available resolutions on this device
        resolutions = Screen.resolutions;
        // clear the options in the dropdown
        resolutionDropdown.ClearOptions ();
        // create a new list for the available options
        List<string> options = new List<string> ();

        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++) {
            string option = resolutions [i].width + "x" + resolutions [i].height;
            options.Add (option);

            if (resolutions [i].width == Screen.currentResolution.width && resolutions [i].height == Screen.currentResolution.height) {
                currentResolutionIndex = i;
            }
        }
        resolutionDropdown.AddOptions (options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue ();
    }

    public void SetResolution (int resolutionIndex)
    {
        Resolution resolution = resolutions [resolutionIndex];
        Screen.SetResolution (resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetVolume (float volume)
    {
        audioMixer.SetFloat ("volume", volume);
    }

    public void SetFullscreen (bool state)
    {
        Screen.fullScreen = state;
        isFullscreen = state;
    }

    public void SetAutostart (bool state)
    {
        autostart = state;
    }

}
