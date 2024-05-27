using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargerHealth : MonoBehaviour
{
    [Header("Charger Stats")]
    public ChargerBar chargerBar;
    public int currentEnergy = 50;
    public int maxEnergy = 50;
    [HideInInspector] public bool isEmpty;

    [Header("Particles")]
    public GameObject[] zapParticles;
    public int zapIndex = 0;

    private void Start()
    {
        currentEnergy = maxEnergy;
        chargerBar.SetMaxEnergy(maxEnergy);
    }

    public void TakeEnergy(int energy)
    {
        currentEnergy -= energy;
        chargerBar.SetEnergy(currentEnergy);

        if (currentEnergy <= 0)
        {
            currentEnergy = 0;
            isEmpty = true;
        }
    }
}
