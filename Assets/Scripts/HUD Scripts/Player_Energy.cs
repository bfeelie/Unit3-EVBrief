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

    //Enemy object references
    [Header("Enemy Prefab")]
    public GameObject enemy;

    [Header("Events")]
    public GameEvent onEnergyChanged;

    //Start game with full health & get enemy colliders
    void Awake()
    {
        currentEnergy = maxEnergy;
        energyBar.SetEnergy(maxEnergy);

        enemy.GetComponent<Collider>();
    }

    //Practice - use electricity on button press; check bar doesn't break from too much use at once (spamming)
    void Update()
    {
        // Deplete energy
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (currentEnergy <= 0)
            {
                currentEnergy = 0;
            }

            UseEnergy(10);
        }

        // Add energy
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (currentEnergy <= 0)
            {
                currentEnergy = 0;
            }

            AddEnergy(10);
        }
    }

    // Decrease energy when used, update energy bar
    public void UseEnergy(int deplete)
    {
        currentEnergy -= deplete;

        energyBar.SetEnergy(currentEnergy);

        // Event Listener able to detect change
        onEnergyChanged.Raise();
    }

    // Increase energy when charging, update energy bar
    public void AddEnergy(int increase)
    {
        currentEnergy += increase;

        energyBar.SetEnergy(currentEnergy);

        // Event Listener able to detect change
        onEnergyChanged.Raise();
    }
}