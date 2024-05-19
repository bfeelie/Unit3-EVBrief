using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargerHealth : MonoBehaviour
{
    [Header("Charger Station Stats")]
    public int chargerHealth = 100;
    public int maxHealth = 100;
    public int regenAmount = 10;

    PlayerInteract player;

    private void Awake()
    {
        player = gameObject.GetComponent<PlayerInteract>();
    }

    // Started in case I needed to tell this to stop
    /*void PlayerFull()
    {
        if (playerEnergy.currentEnergy == 100)
        {

        }
    }*/

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
