using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public GameObject informationCanvas;
    public GameObject uIControllerCanvas;
    public GameObject player;
    public void OnInteraction()
    {
        PlayerController playerController = player.GetComponent<PlayerController>();
        playerController.RightFingerId = -1;
        playerController.LeftFingerId = -1;
        playerController.enabled = false;
        uIControllerCanvas.SetActive(false);
        informationCanvas.SetActive(true);
        Debug.Log("Interact!");
    }
}
