using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor; 
#endif

public class MenuBehaviour : MonoBehaviour
{
    // Tried a different way to do it, doesn't really work
    //public GameObject menuOverlay;

    void Awake()
    {
        GameObject mainMenu = GameObject.Find("Main Menu");
        mainMenu.SetActive(true);
       // menuOverlay.SetActive(true);
        PauseGame();
    }

    void Update()
    {

    }

    public void PlayGame()
    {
        GameObject mainMenu = GameObject.Find("Main Menu");
        mainMenu.SetActive(false);
        ResumeGame();
    }

    public void QuitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#endif

    }
    public void OptionsMenu()
    {
        // Add overlay & set boolean to false; and add a rule for activating when needed active
    }

    // Use these functions for Pause & Main Menu
    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        //menuOverlay.SetActive(false);
    }
}
