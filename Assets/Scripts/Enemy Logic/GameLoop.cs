using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameLoop : MonoBehaviour
{

    [Header("Timer Variables")]
    public TextMeshProUGUI TimerText;
    public float time;
    public Image Fill;
    public float Max;

    [Header("Lose State")]
    public GameObject loseMenu;

    [Header("Wind State")]
    public GameObject winMenu;

    public PetrolHealth[] petrolStations;

    private void Awake()
    {
        
    }

    // Start Timer
    void Update()
    {
        UseTimer();
        WinGame();
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
}
