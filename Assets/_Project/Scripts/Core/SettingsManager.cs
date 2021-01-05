using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;

/**
 *  SettingsManager
 *  - Controls settings only - resolution, fullscreen, and volume
 *  - See ProjectManager for starting program, getting data, etc.
 *  - Brackey's to the rescue https://www.youtube.com/watch?v=YOaYQrN1oYQ
 */
public class SettingsManager : MonoBehaviour {

    public Toggle fullscreenToggle;
    public bool isFullscreen;
    public Toggle showMonstersToggle;
    public bool showMonsters;


    public AudioMixer audioMixer;

    public TMP_Dropdown resolutionDropdown;

    Resolution [] resolutions;


    private void Awake ()
    {
        // update the values in the UI that are set in the inspector
        fullscreenToggle.isOn = isFullscreen;
        showMonsters = showMonstersToggle.isOn;
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

    public void SetShowMonsters (bool state)
    {
        showMonsters = state;


        GameObject [] currentMonsters = GameObject.FindGameObjectsWithTag ("monster");
        foreach (GameObject m in currentMonsters) {
            m.GetComponent<Monster> ().emission.enabled = showMonsters;
        }

        //// master list
        //foreach (GameObject m in MonsterIndex.Instance.monsterMasterList) {
        //    if (m != null)
        //        m.SetActive (showMonsters);
        //}

    }


}
