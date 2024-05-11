using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{

    [SerializeField]
    private LayerMask petrolStationLayerMask;

    // Instead of using a player camera, checking using the player's car
    [SerializeField]
    private Transform playerCarCam;

    // Text pop up telling player how to interact
    [SerializeField]
    private GameObject petrolInteractUI;
    private GameObject chargerInteractUI;

    [SerializeField]
    [Min(1)]
    private float hitRange = 3;

    private RaycastHit hit;

    private void Update()
    {
        CheckForUI();
    }

    void CheckForUI()
    {
        // See raycast in editor
        Debug.DrawRay(playerCarCam.position, playerCarCam.forward * hitRange, Color.white);

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
