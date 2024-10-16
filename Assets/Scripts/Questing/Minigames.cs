using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minigames : MonoBehaviour
{
    GoalsManager objective;
    private void Awake()
    {
        objective = FindAnyObjectByType<GoalsManager>();
    }

    public void FoamCutting(GameObject foam)
    {
        Destroy(foam);
        objective.Completed();
        Debug.Log("Foam Cutting");
    }

    public void FoamApplying(GameObject patientFoam)
    {
        if (patientFoam.activeSelf == true)
        {
            Debug.LogWarning("Foam Already Applied");
            return;
        }
        else
        {
            patientFoam.SetActive(true);
            objective.Completed();
        }
        Debug.Log("Foam Applying");
    }

    public void BigDresserUncover(GameObject bigDresser, Material newMaterial)
    {
        MeshRenderer meshRenderer = bigDresser.GetComponent<MeshRenderer>();
        meshRenderer.material = newMaterial;
        objective.Completed();
        Debug.Log("Cover released");
    }

    public void BigDresserApplying()
    {
        Debug.Log("Big Dresser Applying");
    }

    public void HoleCutting()
    {
        Debug.Log("Hole Cutting");
    }

    public void SmallDresserApplying()
    {
        Debug.Log("Small Dresser Applying");
    }

    public void VACActivation()
    {
        Debug.Log("Activating VAC");
    }

    public void VACConfiguration()
    {
        Debug.Log("Configurating VAC");
    }
}
