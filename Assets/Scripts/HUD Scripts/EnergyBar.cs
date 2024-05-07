using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyBar : MonoBehaviour
{
    // Reference slider Inspector component 
    [Header("Slider Object")]
    public Slider slider;

    // Need to work out how much energy is used for recharge to determine when this kicks in
    [Header ("Cause/Effect Variable")]
    bool isTooLow;

    // Placed first to start at max value
    public void SetMaxEnergy(int energy)
    {
        slider.maxValue = 100;
        slider.value = energy;
    }

    // Connects energy value to slider component setting - prevents bar from going below 0.
    public void SetEnergy(int energy)
    {
        slider.value = energy;

        if (energy <= 0)
        {
            energy = 0;
        }
    }

    // This needs to have an amount - does blowing up cost a whole bar? Half bar?
    public void EnergyOut(int lose)
    {
        lose = 0;
        slider.minValue = lose;

        if (lose == 0)
        {
            // Nullify interaction at Petrol station - retain charge station; include check for Petrol Station OR Charge Station tag?
        }
    }
}