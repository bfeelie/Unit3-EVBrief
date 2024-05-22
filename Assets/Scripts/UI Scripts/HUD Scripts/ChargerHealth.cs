using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargerHealth : MonoBehaviour
{
    [Header("Charger Station Stats")]
    public int chargerHealth = 50;
    public int maxHealth = 50;
    public int regenAmount = 10;
    public int useAmount = 10;

    [Header("Particles")]
    public GameObject[] zapParticles;
    public int zapIndex = 0;

    ChargerBar chargerBar;

    private void Awake()
    {
        chargerBar = gameObject.GetComponentInChildren<ChargerBar>();
    }

    public void DepleteEnergy()
    {
        chargerHealth -= useAmount;
        chargerBar.SetEnergy(useAmount);
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
