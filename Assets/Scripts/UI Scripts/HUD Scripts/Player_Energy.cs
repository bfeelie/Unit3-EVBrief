using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Energy : MonoBehaviour
{
    //Refs for player electricity (energy) values
    [Header("UI Variables")]
    public EnergyBar energyBar;

    public int maxEnergy = 100;
    public int currentEnergy;

    //Start game with full health & get enemy colliders
    void Awake()
    {
        currentEnergy = maxEnergy;
        energyBar.SetEnergy(maxEnergy);
    }

    //Practice - use electricity on button press; check bar doesn't break from too much use at once (spamming)
    void Update()
    {

    }

    // Decrease energy when used, update energy bar
    public void UseEnergy(int deplete)
    {
        currentEnergy -= deplete;
        energyBar.SetEnergy(currentEnergy);

        if (currentEnergy <= 0)
        {
            currentEnergy = 0;
        }

        //Event Listener able to detect change
        //onEnergyChanged.Raise();
    }

    // Increase energy when charging, update energy bar
    public void AddEnergy(int increase)
    {

        if (currentEnergy <= 0)
        {
            currentEnergy = 0;
        }

        currentEnergy += increase;

        energyBar.SetEnergy(currentEnergy);

        // Event Listener able to detect change
        //onEnergyChanged.Raise();
    }
}