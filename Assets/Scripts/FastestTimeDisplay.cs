using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FastestTimeDisplay : MonoBehaviour
{
    public TMP_Text fastestTimeText;

    void Start()
    {
        float fastestTime = PlayerPrefs.GetFloat("FastestTime", float.MaxValue);

        if (fastestTime == float.MaxValue)
        {
            fastestTimeText.text = "No fastest time recorded.";
        }
        else
        {
            fastestTimeText.text = "Fastest Time: " + fastestTime.ToString("F2") + "s";
        }
    }
}

