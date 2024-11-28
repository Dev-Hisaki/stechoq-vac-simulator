using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToVACPanel : MonoBehaviour
{
    private GoalsManager goalsManager;
    private PlayerController playerController;
    private int missionId;

    void Start()
    {
        GameObject goalsInHierarcy = GameObject.Find("GoalsManager");
        goalsManager = goalsInHierarcy.GetComponent<GoalsManager>();
    }

    void Update()
    {
        missionId = goalsManager.getMissionId;
    }
}
