
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
    public int currentHealth = 50;
    public int maxHealth = 50;
    public PetrolBar petrolBar;

    [Header("FX")]
    public GameObject[] smokeParticles;
    [HideInInspector]
    public int smokeIndex = 0;

    public bool isDestroyed = false;

    private void Start()
    {
        // This was needed to prevent the problem with not manually assigning objects in the inspector -- kept as reminder (same in Chargerbar/ChargerHealth)
        //petrolBar = GameObject.Find("Canvas/PetrolStationUI/Slider").GetComponent<PetrolBar>();
        currentHealth = maxHealth;
        petrolBar?.SetMaxHealth(maxHealth);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        petrolBar.SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            isDestroyed = true;

            // Change material or meshes OR not necessary and just keep on PlayerInteract
        }
    }
}
