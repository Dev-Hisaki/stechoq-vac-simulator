using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minigames : MonoBehaviour
{
    public void FoamCutting(GameObject foam)
    {
        Destroy(foam);
        FindAnyObjectByType<GoalsManager>().Completed();
        Debug.Log("Foam Cutting");
    }

    public void FoamApplying()
    {
        Debug.Log("Foam Applying");
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
