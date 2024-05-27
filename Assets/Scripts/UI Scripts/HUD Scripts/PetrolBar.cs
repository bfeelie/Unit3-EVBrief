using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PetrolBar : MonoBehaviour
{
    // Reference slider Inspector component 
    [Header("Slider Object")]
    public Slider slider;

    // Need to work out how much energy is used for recharge to determine when this kicks in
    [Header("Cause/Effect Variable")]
    public bool isDead = false;

    private void Start()
    {
        SetMaxHealth(50);
    }

    // Connects energy value to slider component setting - prevents bar from going below 0. Used in PlayerInteract Script.
    public void SetHealth(int health)
    {
        slider.value = health;

        if (health <= 0)
        {
            slider.value = 0;
            isDead = true;
        }
    }

    // Placed first to start at max value
    public void SetMaxHealth(int health)
    {
        slider.value = health;

        if (slider.maxValue >= 50)
        {
            slider.maxValue = 50;
        }
    }
}
