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

    public void BigDresserApplying(GameObject patientDresser)
    {
        if (patientDresser.activeSelf == true)
        {
            Debug.LogWarning("Dresser Already Applied");
            return;
        }
        else
        {
            patientDresser.SetActive(true);
            objective.Completed();
        }
        Debug.Log("Big Dresser Applying");
    }

    public void HoleCutting(GameObject patientBigDresser, GameObject patienBigDresserHole)
    {
        if (patientBigDresser.activeSelf == false && patienBigDresserHole.activeSelf == true)
        {
            Debug.LogWarning("Hole Already Created");
            return;
        }
        else
        {
            patientBigDresser.SetActive(false);
            patienBigDresserHole.SetActive(true);
            objective.Completed();
        }
        Debug.Log("Hole Cutting");
    }

    public void SmallDresserUncover(GameObject smallDresser, Material newMaterial)
    {
        MeshRenderer[] meshRenderers = smallDresser.GetComponentsInChildren<MeshRenderer>();

        foreach (MeshRenderer renderer in meshRenderers)
        {
            renderer.material = newMaterial;
        }

        objective.Completed();
        Debug.Log("Cover released for all children");
    }

    public void SmallDresserApplying(GameObject patientSmallDresser)
    {
        if (patientSmallDresser.activeSelf == true)
        {
            Debug.LogWarning("Small Dresser Already Applied");
            return;
        }
        else
        {
            patientSmallDresser.SetActive(true);
            objective.Completed();
        }
        Debug.Log("Big Dresser Applying");
    }

    public void PipeToVAC(GameObject selangInVAC)
    {
        if (selangInVAC.activeSelf) return;
        selangInVAC.SetActive(true);
        objective.Completed();
        Debug.Log("Pipe installed");
    }

    public void PipeToPatient(GameObject pipeline, GameObject pipeInVAC)
    {
        pipeline.SetActive(true);
        pipeInVAC.SetActive(false);
        objective.Completed();
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
