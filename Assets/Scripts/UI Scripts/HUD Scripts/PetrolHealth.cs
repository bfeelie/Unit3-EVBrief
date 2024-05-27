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

    public GameObject[] smokeParticles;
    [HideInInspector]
    public int smokeIndex = 0;

    public bool isDestroyed = false;

    private void Start()
    {
        currentHealth = maxHealth;
        petrolBar = GameObject.Find("Canvas/PetrolStationUI/Slider").GetComponent<PetrolBar>();
        petrolBar.SetMaxHealth(maxHealth);
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

    /*
    // Why did I make this
    public void TakeHealth(int health)
    {
        slider.value = health;

        if (health > 0)
        {
            slider.value = health--;
            Debug.Log("Depleting" + health + "petrol.");
        }
    }*/
}
