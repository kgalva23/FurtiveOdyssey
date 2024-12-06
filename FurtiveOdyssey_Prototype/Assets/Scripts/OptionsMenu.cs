using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Rendering.PostProcessing;

public class OptionsMenu : MonoBehaviour
{
    public Toggle fullscreenToggle;
    public Toggle vSyncToggle;

    public Slider brightnessSlider; // Reference to the slider
    public Image overlayPanel;       // Assign your UI Panel here

    // Start is called before the first frame update
    void Start()
    {
        // Load VSync setting from PlayerPrefs (default is 1 = VSync enabled)
        int vSyncEnabled = PlayerPrefs.GetInt("VSyncEnabled", 1);
        QualitySettings.vSyncCount = vSyncEnabled;

        // Set the toggle to match the loaded setting
        vSyncToggle.isOn = vSyncEnabled == 1;

        // Optional: Load and apply fullscreen setting
        bool fullscreenEnabled = PlayerPrefs.GetInt("FullscreenEnabled", 1) == 1;
        Screen.fullScreen = fullscreenEnabled;
        fullscreenToggle.isOn = fullscreenEnabled;

        // Load brightness setting, default to 1 (max brightness) if not set
        float savedBrightness = PlayerPrefs.GetFloat("Brightness", 1f);
        brightnessSlider.value = savedBrightness;

        // Apply the loaded brightness value to overlayPanel
        SetBrightness(savedBrightness);

        // Add a listener to adjust the overlay transparency
        brightnessSlider.onValueChanged.AddListener(SetBrightness);
        
    }

    public void SetBrightness(float value)
    {
        Color color = overlayPanel.color;

        // Map the slider value to the alpha range (0 to 1)
        float calculatedAlpha = 1 - value;

        // Ensure the alpha value does not exceed 253 in the 0-255 scale
        calculatedAlpha = Mathf.Min(calculatedAlpha, 253 / 255f);

        // Apply the calculated alpha directly
        color.a = calculatedAlpha;

        overlayPanel.color = color; // Update the panel color

        // Save brightness slider value to PlayerPrefs
        PlayerPrefs.SetFloat("Brightness", value);
        PlayerPrefs.Save();
    }

    public void ApplyFullscreen()
    {
        Screen.fullScreen = fullscreenToggle.isOn;
        PlayerPrefs.SetInt("FullscreenEnabled", fullscreenToggle.isOn ? 1 : 0);
        PlayerPrefs.Save();
    }

    public void ApplyVSync()
    {
        if (vSyncToggle.isOn)
        {
            QualitySettings.vSyncCount = 1; // Enable VSync
            Debug.Log("VSync Enabled");
        }
        else
        {
            QualitySettings.vSyncCount = 0; // Disable VSync
            Debug.Log("VSync Disabled");
        }

        // Reapply current quality settings to force update
        QualitySettings.SetQualityLevel(QualitySettings.GetQualityLevel(), true);

        // Save the setting
        PlayerPrefs.SetInt("VSyncEnabled", vSyncToggle.isOn ? 1 : 0);
        PlayerPrefs.Save();
    }

    public void ClosePanel()
    {
        gameObject.SetActive(false);
    }
}
