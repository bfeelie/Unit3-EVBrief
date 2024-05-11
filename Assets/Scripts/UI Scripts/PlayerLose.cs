using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;

public class PlayerLose : MonoBehaviour
{
    [Header("Fail UI")]
    public GameObject loseMenu;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Time.timeScale = 0;
            loseMenu.SetActive(true);
        }
    }

    /*private void Awake()
    {
        loseMenu.SetActive(false);
    }*/
}