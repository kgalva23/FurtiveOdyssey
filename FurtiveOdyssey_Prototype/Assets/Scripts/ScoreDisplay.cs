using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreDisplay : MonoBehaviour
{
    [SerializeField] Coin coin;

    [SerializeField] TextMeshProUGUI scoreText;
    
    [SerializeField] Image mainWeaponImage; // Image component for the main weapon

    [SerializeField] Sprite mainWeaponSprite; // Sprite for the main weapon

    [SerializeField] Sprite secondaryWeaponSprite; // Sprite for the secondary weapon

    // Reference to the WeaponSwitcher
    [SerializeField] WeaponSwitcher weaponSwitcher;

    [SerializeField] Transform barTransform;

    [SerializeField] Ross ross;

    void Start()
    {
        // Initialize health bar progress
        SetProgress(1.0f);  // Full health at the start
        //SetProgress(0.5f);
    }

    void SetProgress (float progress) {
        // Clamp progress between 0 and 1
        //progress = Mathf.Clamp01(progress);
        barTransform.localScale = new Vector3(progress, barTransform.localScale.y, 1);
    }
    // Update is called once per frame
    void Update()
    {

        // Calculate health percentage from Ross's current health
        float healthProgress = ross.currentHealthStatus() / ross.maxHealthStatus();

        // Update the health bar based on the health percentage
        SetProgress(healthProgress);

        //float healthProgress = ross.currentHealthStatus();


        // Convert Coin score to text and display the score
        int newCoinScore = ScoreCounter.Instance.totalCoinScore;
        scoreText.text = "Coins collected: " + newCoinScore.ToString() + "\n" + "Weapon: ";

        // Check the return value of returnProjectileLauncher() from WeaponSwitcher
        if (weaponSwitcher.returnProjectileLauncher())
        {
            // If true, set the main weapon sprite
            mainWeaponImage.sprite = mainWeaponSprite;
            mainWeaponImage.transform.localScale = Vector3.one; // Reset scale to default (1, 1, 1)
        }
        else
        {
            // If false, switch to the secondary weapon sprite
            mainWeaponImage.sprite = secondaryWeaponSprite;
            mainWeaponImage.transform.localScale = new Vector3(0.55f, 0.55f, 1f); // Scale to 75%
        }
    }
}
