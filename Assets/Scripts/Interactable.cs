using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public GameObject informationCanvas;
    public GameObject uIControllerCanvas;
    public GameObject player;
    public void OnInteraction(int id)
    {
        PlayerController playerController = player.GetComponent<PlayerController>();
        SpawnUIObject itemUISpawner = informationCanvas.GetComponent<SpawnUIObject>();
        playerController.RightFingerId = -1;
        playerController.LeftFingerId = -1;
        playerController.enabled = false;
        uIControllerCanvas.SetActive(false);
        informationCanvas.SetActive(true);
        itemUISpawner.SpawnUIObjectAtPoint(id);
    }

    public void SpawnInHand(int id)
    {
        SpawnUIObject itemUISpawner = informationCanvas.GetComponent<SpawnUIObject>();
        itemUISpawner.SpawnObjectOnHand(id);
    }

    public void DropItem(int id)
    {
        SpawnUIObject itemUISpawner = informationCanvas.GetComponent<SpawnUIObject>();
        itemUISpawner.SpawnObjectOnHand(id);
        Debug.Log("DropItem initiated");
    }
}
