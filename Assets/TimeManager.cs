using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    private float timer;
    private bool timerOn = false;
    public TMP_InputField timerText;

    void Start()
    {
        timer = 0f;
    }

    void Update()
    {
        if (timerOn)
        {
            if (timer >= 0)
            {
                timer += Time.deltaTime;
                UpdateTimer(timer);
            }
        }
    }

    void UpdateTimer(float currentTime)
    {
        currentTime += 1;
        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);
        timerText.text = string.Format("{0:00} : {1:00}", minutes, seconds);
    }

    public void StartTimer()
    {
        timerOn = true;
    }

    public void StopTimer()
    {
        timerOn = false;
    }
}
