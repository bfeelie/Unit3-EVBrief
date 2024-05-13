using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargerHealth : MonoBehaviour
{
    [Header("Charger Station Stats")]
    public int chargerHealth = 100;
    public int maxHealth = 100;

    private void Awake()
    {
        chargerHealth = maxHealth;
    }

    private void OnTriggerEnter(Collider other)
    {
        // check if it's a player
        if (other.gameObject.GetComponent<Player_Energy>())
        {
            Debug.Log(other.gameObject.name + " is at the Charger.");
        }
        else
        {
            return;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        // check if it's a player
        if (other.gameObject.GetComponent<Player_Energy>() && chargerHealth < 100)
        {
            // Start re-energising at slow speed after # of time
            Debug.Log("Charger is recharging");
        }
        else
        {
            return;
        }
 
    }
}
