using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
//#if UNITY_EDITOR
using UnityEditor;
//#endif
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Microlight.MicroAudio;

public class GameLoop : MonoBehaviour
{

    [Header("Timer Variables")]
    public TextMeshProUGUI TimerText;
    public float time;
    public Image Fill;
    public float Max;

    [Header("Game State UI")]
    public GameObject loseMenu;
    public GameObject winMenu;
    [SerializeField] GameObject pauseMenu;
    public PetrolHealth[] petrolStations;

    [Header("Music")]
    [SerializeField] AudioClip gameplayMusic;

    private void Awake()
    {
        MicroAudio.PlayOneTrack(gameplayMusic, true);
    }

    // Start Timer
    void Update()
    {
        UseTimer();
        WinGame();
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
    }

    void UseTimer()
    {
        time -= Time.deltaTime;
        TimerText.text = "" + (int)time;
        Fill.fillAmount = time / Max;

        if (time <= 0)
            time = 0;

        if (time == 0)
        {
            OutOfTime();
        }
    }

    public void OutOfTime()
    {
        if (time == 0)
        {
            Time.timeScale = 0;
            loseMenu.SetActive(true);
        }
        else
            return;
    }

    void WinGame()
    {
        bool allAreDestroyed = true;

        foreach (PetrolHealth petrol in petrolStations)
        {
            if (petrol.isDestroyed == false)
            {
                allAreDestroyed = false;
                return;
            }
        }

        if (allAreDestroyed == true)
        {
            Time.timeScale = 0;
            winMenu.SetActive(true);
        }   
    }

    public void PlayGame()
    {
        //EditorApplication.isPlaying = true;
        Time.timeScale = 1;
        SceneManager.LoadSceneAsync(1);
        Cursor.visible = false;
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
            pauseMenu.SetActive(true);
            Time.timeScale = 0;
            Cursor.visible = true;     
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        /*
        if (Cursor.visible != false)
        {
            Cursor.visible = false;
        }
  */
    }

    public void MainMenuReturn()
    {
        Cursor.visible = true;
        SceneManager.LoadSceneAsync(0);
    }
}
