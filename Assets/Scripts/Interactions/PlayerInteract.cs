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
    // Changed to public to try BlowUp
    private Transform playerCarCam;

    // Could be public later if you need other things to call into it from outside
    [HideInInspector]  private Player_Energy playerEnergy;

    // Text pop up telling player how to interact
    [Header("Petrol Station")]
    [SerializeField]
    public bool isAtPetrolStation = false;
    [SerializeField] PetrolHealth currentPetrolStation;
    [SerializeField] GameObject petrolInteractUI;

    [Header("Charging Station")]
    public bool isAtCharger = false;
    public ChargerHealth currentCharger;
    public GameObject chargerInteractUI;
    public ParticleSystem chargeParticle;

    [SerializeField]
    [Min(1)]

    private float hitRange = 3;

    private RaycastHit hit;

    // Just to make sure the scripts are recognised by the script after start.
    private void Start()
    {
        playerEnergy = gameObject.GetComponent<Player_Energy>();
        currentCharger = gameObject.GetComponent<ChargerHealth>();
    }

    private void Update()
    {
        CheckForUI();
        AttackPetrol();
        UseCharger();
    }

    // Function to make Player deplete Petrol station health IF in range and pressing E
    // Calls on invisible health bar from stationHealth script
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
                playerEnergy.energyBar.SetEnergy(playerEnergy.currentEnergy);

                // turn on another smoke particle -- remember to keep particle number the same as array amt
                currentPetrolStation.smokeParticles[currentPetrolStation.smokeIndex].SetActive(true);
                //currentPetrolStation.smokeParticles[currentPetrolStation.smokeIndex].SetActive(true);
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

        // Defensive code just to stop this function running all the time
        else return;
    }

    // Function to make Player deplete charger energy IF in range and pressing E
    // Calls on invisible charger 'health' bar from ChargerHealth script; fills Electricity bar
    public void UseCharger()
    {
        if (isAtCharger == true)
        {
            if (playerEnergy.currentEnergy == 100 || currentCharger.chargerHealth == 0)
            {

                Debug.Log("Charger not needed.");
                isAtCharger = false;
                currentCharger = null;
            }

            if (Input.GetKeyDown(KeyCode.E) && currentCharger.chargerHealth <= 100)
            {
                if (playerEnergy.currentEnergy == 100)
                {
                    Debug.Log("Charger not needed.");
                    return;
                }

                    playerEnergy.AddEnergy(10);
                    currentCharger.DepleteEnergy();
                    Debug.Log("Player has " + playerEnergy.currentEnergy);
                    playerEnergy.energyBar.SetEnergy(playerEnergy.currentEnergy);
                    // Add tell to use ChargerHealth's Deplete energy later

                    currentCharger.chargerHealth -= 10;
                    Debug.Log("Charger used and now has " + currentCharger.chargerHealth + "charges left.");

                    // Turn on charging particles -- CHANGE SMOKEPARTICLES TO ELECTRIC WHEN CREATED then add Particle system & uncomment
                    currentCharger.zapParticles[currentCharger.zapIndex].SetActive(true);
                    currentCharger.zapParticles[currentCharger.zapIndex].GetComponent<ParticleSystem>().Play();
                    gameObject.GetComponentInChildren<GameObject>(chargeParticle).SetActive(true);
                    chargeParticle.Play();
            }
        }

    }


    public void CheckForUI()
    {
        // See raycast in editor to check that it's working
        //Debug.DrawRay(playerCarCam.position, playerCarCam.forward * hitRange, Color.red);
        
        // Checks if in proximity (using the 'interaction spot' collider); if true then show UI object (UI object must be added in inspector slot)
        if (isAtPetrolStation)
        {
            petrolInteractUI.SetActive(true);
            //Debug.Log("Activated UI");
        }

        // Set UI as false if not at Petrol Station so player can't use it
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

        // Checks if in proximity (using the 'interaction spot' collider); if true then show UI object (UI object must be added in inspector slot)
        if (isAtCharger)
        {
            chargerInteractUI.SetActive(true);
        }

        // Set UI as false if not at Charger so player can't use it
        else if (!isAtCharger)
        {
            chargerInteractUI.SetActive(false);
        }

        // Turn off UI for dead petrol station
        else
        {
            return;
        }


        // If not hitting collider, do not show highlight -- this is currently not working (14 May)
        if (hit.collider != null)
        {
            hit.collider.GetComponent<Highlight>()?.ToggleHighlight(false);

        }

        // The raycast hits based on player's position and within range, only check interactionLayerMask - out 'saves' the hit to check
        if (Physics.Raycast(playerCarCam.position, playerCarCam.forward, out hit, hitRange, petrolStationLayerMask))
        {
            // Highlight currently not working 14 May
            hit.collider.GetComponent<Highlight>()?.ToggleHighlight(true);
            petrolInteractUI.SetActive(true);
        }

    }

    // Checks for proximity to Petrol Station / Charger -- Enter
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PetrolHealth>())
        {
            Debug.Log("We are at Petrol Station: " + other.gameObject.name);

            // If it is, set bool as true
            isAtPetrolStation = true;

            // Make reference to which petrol station we are at - so script will only target THIS station
            currentPetrolStation = other.gameObject.GetComponent<PetrolHealth>();
        }
        else
        {
            //Debug.Log("(Petrol) Touched something else: " + other.gameObject.name);
        }

        if (other.gameObject.GetComponent<ChargerHealth>())
        {
            Debug.Log("We are at Charger Station: " + other.gameObject.name);

            // If it is, set isAtPetrolStation = true
            isAtCharger = true;

            // Make reference to charger we are at
            currentCharger = other.gameObject.GetComponent<ChargerHealth>();
        }
        else
        {
            // Checking for incorrect collision - difference between both building checks
            //Debug.Log("(Charger) Touched something else: " + other.gameObject.name);
        }

    }

    // Checks for proximity to Petrol Station / Charger -- Exit
    private void OnTriggerExit(Collider other)
    {
        // Check if it's a petrol station
        if (other.gameObject.GetComponent<PetrolHealth>())
        {
            Debug.Log("Left the Petrol Station: " + other.gameObject.name);

            // if it is, set isAtPetrolStation = false
            isAtPetrolStation = false;
            // blank out petrol station reference
            currentPetrolStation = null;
        }

        // Check if it's a charger
        if (other.gameObject.GetComponent<ChargerHealth>())
        {
            Debug.Log("Left the Charger: " + other.gameObject.name);

            // Same logic as petrol station
            isAtCharger = false;
            currentCharger = null;
        }

        else
        {
            //Debug.Log("Left something else: " + other.gameObject.name);
        }

    }

}