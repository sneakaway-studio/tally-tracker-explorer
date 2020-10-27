using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class PlayerDetailsUI : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI clicksText;
    public TextMeshProUGUI hoursText;
    public TextMeshProUGUI kmScrolledText;
    public TextMeshProUGUI clickMinuteText;
    public TextMeshProUGUI monstersText;
    public TextMeshProUGUI blockedText;
    public TextMeshProUGUI trackersText;

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
}
