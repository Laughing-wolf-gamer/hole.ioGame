using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
public class GameTimer : MonoBehaviour
{

    public TMPro.TextMeshProUGUI timeText;
    public float targetTime = 60.0f;

    void Update()
    {
        if (!GameManager.isGameStarted) return;
        targetTime -= Time.deltaTime;

        if (targetTime <= 0.0f)
        {
            timerEnded();
        }
        else
        {
            TimeSpan time = TimeSpan.FromSeconds(targetTime);

            timeText.text = "" + time.Minutes.ToString("00") + ":" + time.Seconds.ToString("00");
        }
    }

    void timerEnded()
    {
        timeText.text = "T-UP!";
        GameManager.Instance.StopGameByTimeOut();
    }

}
