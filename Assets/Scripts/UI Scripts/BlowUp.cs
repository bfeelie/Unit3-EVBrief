using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR.Haptics;

public class BlowUp : MonoBehaviour
{
    PlayerInteract playerInteract;

    [SerializeField]
    GameObject player;

    [SerializeField]
    //changed to public to try BlowUp
    public Transform playerCarCam;
    public RaycastHit hit;

    [SerializeField]
    //private InputActionAsset interact;
    bool active = false;

    // Update is called once per frame
    void Update()
    {
        
        {

        }
    }

    void BlowUpPetrol()
    {
        active = Physics.Raycast(playerInteract.playerCarCam.position, playerInteract.playerCarCam.TransformDirection(Vector3.forward), out playerInteract.hit, playerInteract.hitRange);

        if (Input.GetKeyDown(KeyCode.E) && active == true)
        {
            //GameObject petrolStation = GameObject.FindWithTag("Petrol Station");
            if (hit.collider.tag == "Player")
            {
                gameObject.SetActive(false);
                Debug.Log("Attempted Petrol Hide");
            }

            else
                Debug.Log("Did not hide");
            return;
        }
    }

    // This is in case I want to use InputAction ref stuff
    /*private void Use(InputAction.CallbackContext context)
    {
        //throw new NotImplementedException();

        if (hit.collider != null)
        {
            Debug.Log(hit.collider.name);
        }
    }*/
}
