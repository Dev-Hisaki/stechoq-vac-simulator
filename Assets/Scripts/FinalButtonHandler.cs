using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class FinalButtonHandler : MonoBehaviour
{
    [SerializeField] private GameObject finishPanel;
    private void Start()
    {
        if (finishPanel.activeSelf) finishPanel.SetActive(false);
    }
    public void OpenFinishPanel()
    {
        finishPanel.SetActive(true);
    }
    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(2);
    }
}
