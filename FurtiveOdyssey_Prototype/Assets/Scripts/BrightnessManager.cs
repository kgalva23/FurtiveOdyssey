using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BrightnessManager : MonoBehaviour
{
    public Image overlayPanel;

    void Start()
    {
        // Load saved brightness value
        float savedBrightness = PlayerPrefs.GetFloat("Brightness", 1f);

        // Apply brightness to overlay panel
        if (overlayPanel != null)
        {
            Color color = overlayPanel.color;
            color.a = 1 - savedBrightness; // Invert saved value to match alpha logic
            overlayPanel.color = color;
        }
    }
}
