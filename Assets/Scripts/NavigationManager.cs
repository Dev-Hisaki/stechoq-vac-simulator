using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NavigationManager : MonoBehaviour
{
    #region Variables
    [Header("Buttons")]
    [SerializeField] private Button start;
    [SerializeField] private GameObject finish;
    [SerializeField] private Button continous;
    [SerializeField] private Button intermittent;

    [Header("Panels")]
    [SerializeField] private GameObject home;
    [SerializeField] private GameObject process;

    [Header("Additional")]
    [SerializeField] private TextMeshProUGUI title;
    #endregion

    void Start()
    {
        start.enabled = false;
    }

    void Update()
    {
        if (continous.enabled && !intermittent.enabled)
        {
            if (!start.enabled) start.enabled = true;
            return;
        }
        else if (!continous.enabled && intermittent.enabled)
        {
            if (!start.enabled) start.enabled = true;
            return;
        }
    }

    public void StartOperation()
    {
        FindObjectOfType<VACAudioManager>().Play("VAC Starting");
        if (!continous.enabled)
        {
            process.SetActive(true);
            home.SetActive(false);
            continous.enabled = false;
            intermittent.enabled = false;
            Destroy(start.gameObject);
            finish.SetActive(true);

            GetComponent<TimeManager>().StartTimer();
            GetComponent<PressureManager>().SetPressureTarget();

            title.text = "Mode Continous";
        }
        else if (!intermittent.enabled)
        {
            process.SetActive(true);
            home.SetActive(false);
            continous.enabled = false;
            intermittent.enabled = false;
            Destroy(start.gameObject);
            finish.SetActive(true);

            GetComponent<TimeManager>().StartTimer();
            GetComponent<PressureManager>().SetPressureTarget();

            title.text = "Mode Intermittent";
        }
    }

    public void StopOperation()
    {
        GetComponent<TimeManager>().StopTimer();
    }
}
