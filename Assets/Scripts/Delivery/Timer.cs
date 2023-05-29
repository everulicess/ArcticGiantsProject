using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using UnityEditor;

public class Timer : MonoBehaviour
{
    public TMP_Text timeText;
    float timeLimit = 20f;

    bool inMinutes;

    float time;
    bool startTimer;

    float multiplierFactor;

    TimeSpan timeConvertor;

    private void Start()
    {
        timeText.text = timeLimit.ToString();
        time = timeLimit;
        startTimer = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (!startTimer) return;
        if (time > 0f)
        {
            time -= Time.deltaTime;
           
            timeConvertor = TimeSpan.FromSeconds(time);
            float minutes = timeConvertor.Minutes;
            float seconds = timeConvertor.Seconds;

            timeText.text = $"{minutes}:{seconds}";

        }
        else
        {
            Application.Quit();
            //EditorApplication.isPlaying = false;
        }
    }
    public void StartTimer()
    {
        multiplierFactor = 1f / timeLimit;
        startTimer = true;
    }

   

   /* public void RestartTimer()
    {
        time = timeLimit;
        
        timeConvertor = TimeSpan.FromSeconds(time);
        float minutes = timeConvertor.Minutes;
        float seconds = timeConvertor.Seconds;

        timeText.text = $"{minutes}:{seconds}";
        startTimer = true;
    }*/
}
