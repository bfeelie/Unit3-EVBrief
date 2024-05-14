using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteract : MonoBehaviour
{

    [SerializeField]
    private LayerMask petrolStationLayerMask;

    // Uses non-player visible camera for raycast (set to display 8)
    [SerializeField]
    //changed to public to try BlowUp
    private Transform playerCarCam;

    [SerializeField]
    //could be public later if you need other things to call into it from outside
    private Player_Energy playerEnergy;


    // Text pop up telling player how to interact
    [Header("Petrol Station")]
    [SerializeField]
    public bool isAtPetrolStation = false;
    public PetrolHealth currentPetrolStation;
    public GameObject petrolInteractUI;

    [Header("Charging Station")]
    public bool isAtCharger = false;
    public ChargerHealth currentCharger;
    public GameObject chargerInteractUI;

    [SerializeField]
    [Min(1)]

    private float hitRange = 3;

    private RaycastHit hit;

    private void Start()
    {
        playerEnergy = gameObject.GetComponent<Player_Energy>();
    }


    private void Update()
    {
        CheckForUI();
        AttackPetrol();
        playerEnergy.energyBar.SetEnergy(playerEnergy.currentEnergy);
    }


    public void AttackPetrol()
    {
        if (isAtPetrolStation == true && Input.GetKeyDown(KeyCode.E))
        {

            if (currentPetrolStation.stationHealth > 0)
            {
                playerEnergy.UseEnergy(10);
                Debug.Log("Player has " + playerEnergy.currentEnergy);
                currentPetrolStation.stationHealth -= 10;
                Debug.Log("Petrol Station has been hit and now has " + currentPetrolStation.stationHealth + " health.");

                // turn on another smoke particle -- remember to keep particle number the same as array amt
                currentPetrolStation.smokeParticles[currentPetrolStation.smokeIndex].SetActive(true);
                currentPetrolStation.smokeParticles[currentPetrolStation.smokeIndex].GetComponent<ParticleSystem>().Play();
                currentPetrolStation.smokeIndex++;

                // check if Petrol Station is dead now
                if (currentPetrolStation.stationHealth <= 0)
                {
                    Debug.Log("Petrol Station destroyed.");
                    currentPetrolStation.DestroyPetrolStation();
                    isAtPetrolStation = false;
                    currentPetrolStation = null;
                }
            }
        }
    }

    public void UseCharger()
    {
        if (isAtCharger == true && Input.GetKeyDown(KeyCode.E))
        {

            if (currentPetrolStation.stationHealth > 0)
            {
                playerEnergy.UseEnergy(10);
                Debug.Log("Player has " + playerEnergy.currentEnergy);
                currentPetrolStation.stationHealth -= 10;
                Debug.Log("Petrol Station has been hit and now has " + currentPetrolStation.stationHealth + " health.");

                // turn on another smoke particle -- remember to keep particle number the same as array amt
                currentPetrolStation.smokeParticles[currentPetrolStation.smokeIndex].SetActive(true);
                currentPetrolStation.smokeParticles[currentPetrolStation.smokeIndex].GetComponent<ParticleSystem>().Play();
                currentPetrolStation.smokeIndex++;

                // check if Petrol Station is dead now
                if (currentPetrolStation.stationHealth <= 0)
                {
                    Debug.Log("Petrol Station destroyed.");
                    currentPetrolStation.DestroyPetrolStation();
                    isAtPetrolStation = false;
                    currentPetrolStation = null;
                }
            }
        }
    }


    public void CheckForUI()
    {
        // See raycast in editor
        //Debug.DrawRay(playerCarCam.position, playerCarCam.forward * hitRange, Color.red);
        if (isAtPetrolStation)
        {
            petrolInteractUI.SetActive(true);
        }

        // Set UI as false if not at Petrol Station
        else if (!isAtPetrolStation)
        {
            petrolInteractUI.SetActive(false);
        }

        // Turn off UI for dead petrol station
        else
        {
            if (isAtPetrolStation)
            {
                currentPetrolStation.stationHealth = 0;
                petrolInteractUI.SetActive(false);
            }
        }


        // If not hitting collider, do not show text
        if (hit.collider != null)
        {
            hit.collider.GetComponent<Highlight>()?.ToggleHighlight(false);

        }

        // The raycast hits based on player's position and within range, only check interactionLayerMask - out 'saves' the hit to check
        if (Physics.Raycast(playerCarCam.position, playerCarCam.forward, out hit, hitRange, petrolStationLayerMask))
        {
            hit.collider.GetComponent<Highlight>()?.ToggleHighlight(true);
            petrolInteractUI.SetActive(true);
        }

    }



    private void OnTriggerEnter(Collider other)
    {
        // check if it's a petrol station
        if (other.gameObject.GetComponent<PetrolHealth>())
        {
            Debug.Log("We are at Petrol Station: " + other.gameObject.name);

            // if it is, set isAtPetrolStation = true
            isAtPetrolStation = true;
            // make reference to which petrol station we are at
            currentPetrolStation = other.gameObject.GetComponent<PetrolHealth>();
        }
        else
        {
            Debug.Log("Touched something else: " + other.gameObject.name);
        }

        if (other.gameObject.GetComponent<ChargerHealth>())
        {
            Debug.Log("We are at Charger Station: " + other.gameObject.name);

            // if it is, set isAtPetrolStation = true
            isAtCharger = true;

            // make reference to which petrol station we are at
            currentCharger = other.gameObject.GetComponent<ChargerHealth>();
        }
        else
        {
            Debug.Log("Touched something else: " + other.gameObject.name);
        }

    }




    private void OnTriggerExit(Collider other)
    {
        // check if it's a petrol station
        if (other.gameObject.GetComponent<PetrolHealth>())
        {
            Debug.Log("Left the Petrol Station: " + other.gameObject.name);

            // if it is, set isAtPetrolStation = false
            isAtPetrolStation = false;
            // blank out petrol station reference
            currentPetrolStation = null;
        }

        if (other.gameObject.GetComponent<ChargerHealth>())
        {

        }

        else
        {
            //Debug.Log("Left something else: " + other.gameObject.name);
        }

    }

}