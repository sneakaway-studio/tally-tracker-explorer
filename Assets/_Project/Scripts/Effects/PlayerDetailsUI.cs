using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class PlayerDetailsUI : MonoBehaviour
{
    // Overall player UI panel
    public GameObject playerPanel;
    // Username text component
    public TextMeshProUGUI nameText;
    // Click data text component
    public TextMeshProUGUI clicksText;
    // Hour data text component
    public TextMeshProUGUI hoursText;
    // km scrolled data text component
    public TextMeshProUGUI kmScrolledText;
    // Click/minute data text component
    public TextMeshProUGUI clickMinuteText;
    // Monster captured data text component
    public TextMeshProUGUI monstersText;
    // Blocked data text component
    public TextMeshProUGUI blockedText;
    // Tracker data text component
    public TextMeshProUGUI trackersText;

    // Array of panels that get turned on/off
    public GameObject[] panels;
    // Current index of panel in iterator
    private int panelsIndex = 0;

    private void Awake()
    {
        StartCoroutine(onCoroutine());
    }

    // Sets the UI text to be filled with player data
    public void setUI(string name, string clicks, string hours, string kmScrolled, string clickMinute, string monsters, string blocked, string trackers)
    {
        nameText.text = "<style=c3>" + name + "</style>";
        clicksText.text = "<style=c2>" + clicks + "</style>";
        hoursText.text = "<style=c2>" + hours + "</style>";
        kmScrolledText.text = "<style=c2>" + kmScrolled + "</style>";
        clickMinuteText.text = "<style=c2>" + clickMinute + "</style>";
        monstersText.text = "<style=c2>" + monsters + "</style>";
        blockedText.text = "<style=c2>" + blocked + "</style>";
        trackersText.text = "<style=c2>" + trackers + "</style>";
    }

    // Makes a different data panel visible every two seconds
    IEnumerator onCoroutine()
    {
        while (true)
        {
            panels[panelsIndex].SetActive(false);
            if (panelsIndex == 6)
            {
                panelsIndex = 0;
                panels[panelsIndex].SetActive(true);
            }
            else
            {
                panelsIndex++;
                panels[panelsIndex].SetActive(true);
            }

            yield return new WaitForSeconds(2f);
        }
    }
}
