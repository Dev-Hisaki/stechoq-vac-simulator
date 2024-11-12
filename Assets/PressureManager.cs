using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PressureManager : MonoBehaviour
{
    #region Variables
    [SerializeField] private TMP_InputField pressureField;
    private int pressure;
    #endregion

    void Start()
    {
        pressure = 100;
        if (pressureField == null) Debug.LogWarning("Pressure field not assigned yet");
        pressureField.text = pressure.ToString();
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
}
