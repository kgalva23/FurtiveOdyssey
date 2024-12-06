using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class ScreenSettings : MonoBehaviour
{
    [SerializeField] TMP_Dropdown resolutionDropdown;
    List<Resolution> uniqueResolutions = new List<Resolution>();

    void Start()
    {
        // Clear existing dropdown options to prevent duplicates
        resolutionDropdown.ClearOptions();

        // Use a HashSet to track unique resolution strings
        HashSet<string> seenResolutions = new HashSet<string>();
        List<string> dropdownOptions = new List<string>();

        // Filter unique resolutions and populate dropdown options
        foreach (Resolution res in Screen.resolutions)
        {
            string resolutionString = res.width + "x" + res.height;
            if (seenResolutions.Add(resolutionString))
            {
                uniqueResolutions.Add(res);
                dropdownOptions.Add(resolutionString);
                Debug.Log($"Added Resolution: {resolutionString}");
            }
        }

        // Add unique resolutions to the dropdown
        resolutionDropdown.AddOptions(dropdownOptions);

        // Get the current resolution and find its index
        string currentResolutionString = Screen.currentResolution.width + "x" + Screen.currentResolution.height;
        int currentResolutionIndex = dropdownOptions.FindIndex(option => option == currentResolutionString);

        // Set dropdown value
        resolutionDropdown.value = currentResolutionIndex >= 0 ? currentResolutionIndex : 0;
        resolutionDropdown.RefreshShownValue();

        // Apply the current resolution
        SetResolution();
    }

    public void SetResolution()
    {
        // Get the selected resolution index
        int selectedIndex = resolutionDropdown.value;

        // Find the corresponding resolution in uniqueResolutions
        Resolution selectedResolution = uniqueResolutions[selectedIndex];
        Screen.SetResolution(selectedResolution.width, selectedResolution.height, true);

        // Save the selected resolution index
        PlayerPrefs.SetInt("ResolutionIndex", selectedIndex);
        PlayerPrefs.Save();

        Debug.Log($"Resolution set to: {selectedResolution.width}x{selectedResolution.height}");
    }
}