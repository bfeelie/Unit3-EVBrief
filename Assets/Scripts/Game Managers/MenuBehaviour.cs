using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor; 
#endif

public class MenuBehaviour : MonoBehaviour
{
    // Only necessary for menu overlay
    public GameObject pauseMenu;
    public GameObject winMenu;

    void Awake()
    {
     
    }

    public void PlayGame()
    {
        SceneManager.LoadSceneAsync(1);

        // This code is for an overlay instead of scene change - setup for that is in Pacman_Coding scene
        //mainMenu.SetActive(false);
        //ResumeGame();
    }

    public void QuitGame()
    {
        Application.Quit();

//This code is to test quit in Unity - need to uncomment the if statement at the system library above too ^
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
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
    }

    public void MainMenuReturn()
    {
        SceneManager.LoadSceneAsync(0);
    }
}
