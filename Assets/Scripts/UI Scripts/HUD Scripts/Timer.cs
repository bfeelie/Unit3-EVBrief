using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float time;
    public TextMeshProUGUI TimerText;
    public Image Fill;
    public float Max;

    PlayerLose playerlose;

    // Update is called once per frame
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

        OutOfTime();
    }

    void OutOfTime()
    {
        if (time == 0)
        {
            playerlose.LoseState();
        }
        else
            return;
    }
}
