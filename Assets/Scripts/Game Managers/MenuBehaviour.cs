using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor; 
#endif

public class MenuBehaviour : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadSceneAsync("1");
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
}
