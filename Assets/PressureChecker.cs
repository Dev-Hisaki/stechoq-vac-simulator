using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PressureChecker : MonoBehaviour
{
    private GameObject questManager;
    private GoalsManager quest;
    private TMP_InputField thisInputField;
    void Start()
    {
        questManager = GameObject.Find("QuestManager");
        quest = questManager.GetComponent<GoalsManager>();
        thisInputField = GetComponent<TMP_InputField>();
    }

    void Update()
    {
        if (thisInputField.text == "110")
        {
            quest.SetInstruction(2);
        }
        else
        {
            quest.SetInstruction(1);
        }
    }
}
