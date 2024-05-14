using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using Unity.VisualScripting;
using UnityEngine;

[SelectionBase]
public class PetrolHealth : MonoBehaviour
{

    // Max health just in as a precaution for starting game
    [Header("Petrol Station Stats")]
    public int stationHealth = 50;
    public int maxHealth = 50;

    //public bool isDestroyed = false;

    public GameObject[] smokeParticles;
    [HideInInspector]
    public int smokeIndex = 0;


    private void Awake()
    {
        stationHealth = maxHealth;
    }


    /*
    public void AttackStation(int deplete)
    {
        stationHealth -= deplete;
        Debug.Log("Station has been hit and now has " + stationHealth + " health.");

        if (stationHealth <= 0)
        {
            stationHealth = 0;
            isDestroyed = true;
            Debug.Log("This" + gameObject + "set as inactive.");
        }
    }
    */




    public void DestroyPetrolStation()
    {
        // turn on smoke particles
        // change material or meshes

    }




}
