using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyBar : MonoBehaviour
{
    // Made using Brackeys Health Bar tutorial, with tweaks

    // Reference slider Inspector component 
    [Header("Slider Object")]
    public Slider slider;

    // Need to work out how much energy is used for recharge to determine when this kicks in
    [Header ("Cause/Effect Variable")]
    public bool isEmpty = false;

    private void Start()
    {
        SetMaxEnergy(100);
    }


    // Placed first to start at max value
    public void SetMaxEnergy(int energy)
    {
        slider.value = energy;

        if (slider.maxValue >= 100)
        {
            slider.maxValue = 100;
        }
    }

    // Connects energy value to slider component setting - prevents bar from going below 0. Used in PlayerInteract Script.
    public void SetEnergy(int energy)
    {
        slider.value = energy;

        if (energy <= 0)
        {
            slider.value = 0;
        }
    }
}