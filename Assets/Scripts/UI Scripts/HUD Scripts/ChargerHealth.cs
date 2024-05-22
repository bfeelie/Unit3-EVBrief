using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargerHealth : MonoBehaviour
{
    [Header("Charger Station Stats")]
    public int chargerHealth = 50;
    public int maxHealth = 50;
    public ChargerBar chargerBar;

    [Header("Particles")]
    public GameObject[] zapParticles;
    public int zapIndex = 0;

    private void Awake()
    {
        chargerHealth = 50;
        chargerBar.SetEnergy(maxHealth);
    }

    private void Update()
    {
        UseEnergy(10);
    }

    public void UseEnergy(int deplete)
    {
        if (chargerHealth <= 0)
        {
            return;
        }
        else
        {
            chargerHealth -= deplete;
        }

        chargerBar.SetEnergy(chargerHealth);

        //Event Listener able to detect change
        //onEnergyChanged.Raise();
    }

    // May not be necessary until need to halt/start charge? Though charge can just stop when it reaches 100 in exit?
    /*private void OnTriggerEnter(Collider other)
    {
        // check if it's a player
        if (other.gameObject.GetComponent<Player_Energy>())
        {
            Debug.Log(other.gameObject.name + " is at the Charger.");
            player.isAtCharger = true;
        }
        else
        {
            return;
        }

    }*/

    /*private void OnTriggerExit(Collider other)
    {
        // Check if it's a player
        if (other.gameObject.GetComponent<PlayerInteract>())
        {
            player.isAtCharger = false;

            if (chargerHealth < 100)
            {
                // Start re-energising at slow speed after # of time
                Debug.Log("Charger is recharging");
            }
        }
        else
        {
            return;
        }
    }*/
}
