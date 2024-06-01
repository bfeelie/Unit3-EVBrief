using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ChargerHealth : MonoBehaviour
{
    [Header("Energy Level")]
    public ChargerBar chargerBar;
    public int currentEnergy = 50;
    public int maxEnergy = 50;
    // Only needed for recharging energy
    //[HideInInspector] public bool isEmpty;

    [Header("VFX")]
    public GameObject[] zapParticles;
    public int zapIndex = 0;
    [SerializeField] GameObject pointerIcon;
    private PlayerInteract playerInteract;

    public void Start()
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
            //isEmpty = true;
            pointerIcon.SetActive(false);
        }
    }
}
