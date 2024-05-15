using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameLoop : MonoBehaviour
{

    [Header("Timer Variables")]
    public TextMeshProUGUI TimerText;
    public float time;
    public Image Fill;
    public float Max;

    [Header("Lose States")]
    public GameObject loseMenu;


    private void Awake()
    {

    }

    // Start Timer
    void Update()
    {
        UseTimer();
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

    }
}
