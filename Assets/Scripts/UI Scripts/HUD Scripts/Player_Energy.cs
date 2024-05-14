using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
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
        if (currentEnergy <= 0)
        {
            return;
        }
        else
        {
            currentEnergy -= deplete;
        }

        energyBar.SetEnergy(currentEnergy);
       
        //Event Listener able to detect change
        //onEnergyChanged.Raise();
    }

    // Increase energy when charging, update energy bar - stop from adding/taking more energy
    public void AddEnergy(int increase)
    {
        if (currentEnergy >= 0)
        {
            currentEnergy += increase;
            Debug.Log("Added energy?");
        }

        // Event Listener able to detect change
        //onEnergyChanged.Raise();
    }
}