using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PressureManager : MonoBehaviour
{
    #region Variables
    [SerializeField] private TMP_InputField pressureField;
    [SerializeField] private TMP_InputField processPressureField;
    [SerializeField] private TMP_InputField pressureText;
    private int pressure;
    private bool pressureOn;
    private float pMeter;
    #endregion

    void Start()
    {
        pressureOn = true;
        pressure = 100;
        pMeter = pressure;
        if (pressureField == null) Debug.LogWarning("Pressure field not assigned yet");
        pressureField.text = pressure.ToString();
    }

    void Update()
    {
        if (pressureOn)
        {
            UpdatePressureDisplay(pMeter);
        }
    }

    public void Add(int num)
    {
        pressure += num;
        UpdateDisplay();
    }

    public void Substract(int num)
    {
        pressure -= num;
        UpdateDisplay();
    }

    private void UpdateDisplay()
    {
        pressureField.text = pressure.ToString();
    }

    public void SetPressureTarget()
    {
        processPressureField.text = pressureField.text;
    }

    public void UpdatePressureDisplay(float meter)
    {
        meter += 1;
        float pressureMeter = Mathf.FloorToInt(meter % 60);
        pressureText.text = string.Format("{00}", pressureMeter);
    }

    public void StartPressureMeter()
    {
        pressureOn = true;
    }
}
