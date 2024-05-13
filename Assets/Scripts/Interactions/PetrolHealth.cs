using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PetrolHealth : MonoBehaviour
{

    // Max health just in as a precaution for starting game
    [Header("Petrol Station Stats")]
    public float stationHealth = 50;
    public float maxHealth = 50;
    public bool isDestroyed = false;

    public GameObject player;

    private void Awake()
    {
        // Safekeeping in case petrol station is set to isDestroyed in editor
        stationHealth = maxHealth;

        if (isDestroyed == true && stationHealth == maxHealth)
        {
            isDestroyed = false;
        }
        else
            return;
    }

    private void Update()
    {
        
    }

    private void OnTrigggerStay(Collider other)
    {
        if (other == player)
        {
            // Test with just player presses E
            if (Input.GetKeyDown(KeyCode.E))
            {
                other.GetComponent<Player_Energy>().UseEnergy(10);
            }
            else
                return;
        }

        if (stationHealth <= 0)
        {
            stationHealth = 0;

            if (stationHealth == 0)
            {
                gameObject.SetActive(false);
            }
        }
    }


    void DestroyStation()
    {
        //if ()
    }
}
