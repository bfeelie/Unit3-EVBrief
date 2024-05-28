using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField]
    private LayerMask petrolStationLayerMask;
    private LayerMask chargerStationLayerMask;

    // Uses non-player visible camera for raycast (set to display 8)
    [SerializeField]
    // Changed to public to try BlowUp
    private Transform playerCarCam;

    // Could be public later if you need other things to call into it from outside
    [HideInInspector] private Player_Energy playerEnergy;

    // Text pop up telling player how to interact
    [Header("Petrol Station")]
    [SerializeField] public bool AtPetrol = false;
    [SerializeField] PetrolHealth currentPetrolStation;
    [SerializeField] GameObject petrolInteractUI;
    [SerializeField] PetrolBar petrolBar;
    public ParticleSystem petrolParticles;

    [Header("Charging Station")]
    public bool AtCharger = false;
    [SerializeField] ChargerHealth currentCharger;
    [SerializeField] GameObject chargerInteractUI;
    public ChargerBar chargerBar;

    [Header("Player")]
    public ParticleSystem attackParticles;
    [SerializeField] [Min(1)] private float hitRange = 3;
    private RaycastHit hit;

    // Just to make sure the scripts are recognised by the script after start.
    private void Start()
    {
        petrolBar = GameObject.Find("Canvas/PetrolStationUI/Slider").GetComponent<PetrolBar>();
        playerEnergy = gameObject.GetComponent<Player_Energy>();
        currentCharger = gameObject.GetComponent<ChargerHealth>();
        chargerBar = gameObject.GetComponent<ChargerBar>();
        attackParticles.Stop();
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
        if (AtPetrol == true && Input.GetKeyDown(KeyCode.E))
        {
            if (currentPetrolStation.currentHealth > 0)
            {
                playerEnergy.UseEnergy(10);
                playerEnergy.energyBar.SetEnergy(playerEnergy.currentEnergy);
                Debug.Log("Player has " + playerEnergy.currentEnergy);

                currentPetrolStation.TakeDamage(10);
                petrolBar.SetHealth(currentPetrolStation.currentHealth);
                Debug.Log ("Petrol Station has been hit and now has" + currentPetrolStation.currentHealth + " health.");
                
                attackParticles.gameObject.SetActive(true);
                attackParticles.Play();

                // If smokeIndex is too large (out of bounds of the list), don't try to turn it off

                if (currentPetrolStation.smokeIndex < currentPetrolStation.smokeParticles.Length)
                {
                    // Turn on another smoke particle -- remember to keep particle number the same as array amt
                    currentPetrolStation.smokeParticles[currentPetrolStation.smokeIndex].SetActive(true);
                    currentPetrolStation.smokeParticles[currentPetrolStation.smokeIndex].GetComponent<ParticleSystem>().Play();
                    currentPetrolStation.smokeIndex++;
                    Debug.Log("Spawned particles in index.");
                }
                else
                {
                    Debug.Log("Trying to turn on smoke particles that don't exist: " + currentPetrolStation.smokeIndex);
                    attackParticles.gameObject.SetActive(false);
                }

                // check if Petrol Station is dead now
                if (currentPetrolStation.currentHealth <= 0)
                {
                    Debug.Log("Petrol Station destroyed.");
                    AtPetrol = false;
                    currentPetrolStation = null;
                    attackParticles.gameObject.SetActive(false);
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
        if (AtCharger)
        {
            if (playerEnergy.currentEnergy == 100 || currentCharger.currentEnergy == 0)
            {
                Debug.Log("Charger not needed.");
                AtCharger = false;
                currentCharger = null;
                return;
            }

            if (Input.GetKeyDown(KeyCode.E) && currentCharger.currentEnergy <= 100)
            {
                currentCharger.TakeEnergy(10);
                playerEnergy.AddEnergy(10);
                playerEnergy.energyBar.SetEnergy(playerEnergy.currentEnergy);
                Debug.Log("Player has " + playerEnergy.currentEnergy);
                chargerBar.SetEnergy(currentCharger.currentEnergy);
                Debug.Log("Charger used and now has " + currentCharger.currentEnergy + "charges left.");

                // Turn on charging particles -- CHANGE SMOKEPARTICLES TO ELECTRIC WHEN CREATED then add Particle system & uncomment
                currentCharger.zapParticles[currentCharger.zapIndex].SetActive(true);
                currentCharger.zapParticles[currentCharger.zapIndex].GetComponentInChildren<ParticleSystem>().Play();
            }
        }
        else
        {
            return;
        }
    }


    public void CheckForUI()
    {
        // See raycast in editor to check that it's working
        //Debug.DrawRay(playerCarCam.position, playerCarCam.forward * hitRange, Color.red);

        // Checks if in proximity (using the 'interaction spot' collider); if true then show UI object (UI object must be added in inspector slot)
        if (AtPetrol)
        {
            petrolInteractUI.SetActive(true);
            //Debug.Log("Activated UI");
        }

        // Set UI as false if not at Petrol Station so player can't use it
        else if (!AtPetrol)
        {
            petrolInteractUI.SetActive(false);
        }

        // Turn off UI for dead petrol station
        else
        {
            if (AtPetrol)
            {
                currentPetrolStation.currentHealth = 0;
                petrolInteractUI.SetActive(false);
            }

            if (AtPetrol)
            {
                currentCharger.currentEnergy = 0;
                petrolInteractUI.SetActive(false);
            }
        }

        // Checks if in proximity if true then show UI object (UI object must be added in inspector slot)
        if (AtCharger)
        {
            chargerInteractUI.SetActive(true);
        }

        // Set UI as false if not at Charger so player can't use it
        else if (!AtCharger)
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
        }

        if (Physics.Raycast(playerCarCam.position, playerCarCam.forward, out hit, hitRange, chargerStationLayerMask))
        {
            // Highlight currently not working 14 May
            hit.collider.GetComponent<Highlight>()?.ToggleHighlight(true);
        }

    }

    // Checks for proximity to Petrol Station / Charger -- Enter
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PetrolHealth>())
        {
            Debug.Log("We are at Petrol Station: " + other.gameObject.name);

            // If it is, set bool as true
            AtPetrol = true;

            // Make reference to which petrol station we are at - so script will only target THIS station
            currentPetrolStation = other.gameObject.GetComponent<PetrolHealth>();
        }
        else
        {
            //Debug.Log("(Petrol) Touched something else: " + other.gameObject.name);
        }

        if (other.gameObject.GetComponentInChildren<ChargerHealth>())
        {
            Debug.Log("We are at Charger Station: " + other.gameObject.name);

            // If it is, set isAtPetrolStation = true
            AtCharger = true;

            // Make reference to charger we are at
            currentCharger = other.gameObject.GetComponent<ChargerHealth>();
        }
        else
        {
            // Checking for incorrect collision - difference between both building checks
            Debug.Log("(Charger) Touched something else: " + other.gameObject.name);
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
            AtPetrol = false;
            // blank out petrol station reference
            currentPetrolStation = null;
        }

        // Check if it's a charger
        if (other.gameObject.GetComponentInChildren<ChargerHealth>())
        {
            Debug.Log("Left the Charger: " + other.gameObject.name);

            // Same logic as petrol station
            AtCharger = false;
            currentCharger = null;
        }

        else
        {
            //Debug.Log("Left something else: " + other.gameObject.name);
        }

    }

}