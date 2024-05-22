using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlayer : MonoBehaviour
{
    public GameObject loseMenu;

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Time.timeScale = 0;
            loseMenu.SetActive(true);
            Debug.Log("Game over!");
        }
    }
    
}
