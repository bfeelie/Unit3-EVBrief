using System;
using System.Collections;
using System.Collections.Generic;
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

    // Text pop up telling player how to interact
    [SerializeField]
    private GameObject petrolInteractUI;
    //private GameObject chargerInteractUI;

    [SerializeField]
    [Min(1)]
  
    private float hitRange = 3;

    //changed to public to try BlowUp
    private RaycastHit hit;

    private void Update()
    {
        CheckForUI();
    }

    //changed to public to try BlowUp
    public void CheckForUI()
    {
        // See raycast in editor
        Debug.DrawRay(playerCarCam.position, playerCarCam.forward * hitRange, Color.red);

        // If not hitting collider, do not show text
        if (hit.collider != null)
        { 
            hit.collider.GetComponent<Highlight>()?.ToggleHighlight(false);
            petrolInteractUI.SetActive(false);
        }

        // The raycast hits based on player's position and within range, only check interactionLayerMask - out 'saves' the hit to check
        if (Physics.Raycast(playerCarCam.position, playerCarCam.forward, out hit, hitRange, petrolStationLayerMask))
        {
            hit.collider.GetComponent<Highlight>()?.ToggleHighlight(true);
            petrolInteractUI.SetActive(true);
        }
    }

}
