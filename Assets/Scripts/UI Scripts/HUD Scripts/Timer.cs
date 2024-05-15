/*using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [Header("Timer Variables")]
    public TextMeshProUGUI TimerText;
    public float time;
    public Image Fill;
    public float Max;

    [Header("Lose States")]
    public GameObject loseMenu;

    GameLoop gameLoop;

    private void Awake()
    {
        gameLoop = gameObject.GetComponent<GameLoop>();
    }

    // Update is called once per frame
    void Update()
    {
        UseTimer();
    }

    public void UseTimer()
    {
        time -= Time.deltaTime;
        TimerText.text = "" + (int)time;
        Fill.fillAmount = time / Max;

        if (time <= 0)
            time = 0;

        if (time == 0)
        {
            gameLoop.OutOfTime();
        }
    }
}*/