using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public Health playerHealth; // Reference to the player's Health component

    void Start()
    {
        if (slider == null)
        {
            Debug.LogError("Slider component is not assigned!");
            return;
        }

        if (playerHealth == null)
        {
            Debug.LogError("Player's Health component is not assigned!");
            return;
        }

        // Set the slider's max value to the player's max health
        slider.maxValue = playerHealth.maxHealth;
    }

    void Update()
    {
        // Update the slider's value to reflect the player's current health
        slider.value = playerHealth.CurrentHealth;
    }
}
