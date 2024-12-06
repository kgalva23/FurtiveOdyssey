using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BrightnessPercentageSlider : MonoBehaviour
{
    [SerializeField] Slider ourSlider;
    [SerializeField] TextMeshProUGUI valueText;


    public void SetPercentage()
    {
        valueText.text = (ourSlider.value * 100).ToString("F0");
    }
}
