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

    public GameObject[] smokeParticles;
    [HideInInspector]
    public int smokeIndex = 0;

    public bool isDestroyed = false;

    private void Awake()
    {
        stationHealth = maxHealth;
    }

    public void DestroyPetrolStation()
    {
        if (stationHealth == 0)
        {
            isDestroyed = true;
            // change material or meshes OR not necessary and just keep on PlayerInteract
        }

    }




}
