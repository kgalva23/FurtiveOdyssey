using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixer;

    string masterVolume = "MasterVolume";
    string sfxVolume = "SFXVolume";
    string musicVolume = "MusicVolume";

    void Start()
    {
        // Load saved audio preferences and apply them to the AudioMixer
        float masterVol = PlayerPrefs.GetFloat(masterVolume, 0.5f);
        float sfxVol = PlayerPrefs.GetFloat(sfxVolume, 0.5f);
        float musicVol = PlayerPrefs.GetFloat(musicVolume, 0.5f);

        SetVolume(masterVolume, masterVol);
        SetVolume(sfxVolume, sfxVol);
        SetVolume(musicVolume, musicVol);
    }

    void SetVolume(string groupName, float value)
    {
        float adjustedVolume = Mathf.Log10(Mathf.Max(value, 0.0001f)) * 20; // Avoid log10(0)
        audioMixer.SetFloat(groupName, adjustedVolume);
    }
}
