using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FPSCounter : MonoBehaviour
{
    public TMP_Text display;
    int frameCounter = 0;
    float timeCounter = 0.0f;
    float lastFramerate = 0.0f;
    public float refreshTime = 0.5f;

    private int intCounter;
    void Start()
    {
        display = GetComponent<TMP_Text>();
    }

    void Update()
    {
        if (timeCounter < refreshTime)
        {
            timeCounter += Time.deltaTime;
            frameCounter++;
        }
        else
        {
            lastFramerate = (float)frameCounter / timeCounter;
            frameCounter = 0;
            timeCounter = 0.0f;
        }
        intCounter = (int)lastFramerate;
        display.text = intCounter.ToString();
    }
}
