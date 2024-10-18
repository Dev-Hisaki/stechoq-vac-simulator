using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GoalsManager : MonoBehaviour
{
    public TextMeshProUGUI questText;
    public Goals[] goals;
    private int id = -1;

    public int getMissionId
    {
        get
        {
            Debug.Log("Mission ID: " + id);
            return id;
        }
    }

    private void Start()
    {
        id++;
        SetInstruction(id);
    }

    public void SetInstruction(int id)
    {
        Goals g = Array.Find(goals, goal => goal.id == id);
        if (g == null)
        {
            Debug.LogWarning("Goal not found");
            return;
        }
        questText.SetText(g.Instructions);
    }

    public void Completed()
    {
        id++;
        SetInstruction(id);
    }
}