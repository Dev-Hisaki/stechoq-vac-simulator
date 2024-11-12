using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NavigationManager : MonoBehaviour
{
    #region Variables
    [Header("Buttons")]
    [SerializeField] private Button start;
    [SerializeField] private Button finish;
    [SerializeField] private Button continous;
    [SerializeField] private Button intermittent;
    public GameObject current;
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

    public void StartOperation(GameObject target)
    {
        current.SetActive(false);
        target.SetActive(true);
        current = target;
    }
}
