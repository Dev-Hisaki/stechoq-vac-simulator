using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonHandler : MonoBehaviour
{
    [Header("Player")]
    public GameObject player;

    [Header("Panels")]
    public GameObject controlPanel;
    public GameObject pausePanel;
    public GameObject settingsPanel;
    public GameObject quitPanel;

    [Header("Buttons")]
    public GameObject backButton;

    [Header("Canvas")]
    public GameObject uIControllerCanvas;
    public GameObject informationCanvas;
    public GameObject objectPosition;

    public CanvasGroup pausePanelCanvasGroup;
    private CanvasGroup controlPanelCanvasGroup;
    private CanvasGroup settingsPanelCanvasGroup;
    private CanvasGroup quitPanelCanvasGroup;

    public void Start()
    {
        controlPanelCanvasGroup = controlPanel.GetComponent<CanvasGroup>();

        if (pausePanel.activeSelf)
        {
            pausePanel.SetActive(false);
        }
        if (controlPanel.activeSelf || settingsPanel.activeSelf || quitPanel.activeSelf)
        {
            settingsPanel.SetActive(false);
            quitPanel.SetActive(false);
        }
    }

    public void Pause()
    {
        controlPanel.SetActive(false);

        pausePanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        controlPanel.SetActive(true);

        pausePanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void OpenSettings()
    {
        pausePanel.LeanAlpha(0f, 2f);
        settingsPanel.SetActive(true);
    }

    public void CloseSettings()
    {
        settingsPanel.SetActive(false);
        pausePanel.LeanAlpha(1f, 1f);
    }

    public void OpenQuit()
    {
        pausePanel.LeanAlpha(0f, 2f);
        quitPanel.SetActive(true);
    }

    public void CloseQuit()
    {
        quitPanel.SetActive(false);
        pausePanel.LeanAlpha(1f, 1f);
    }

    public void ExitToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void CloseInformationPanel()
    {
        PlayerController playerController = player.GetComponent<PlayerController>();
        if (objectPosition.transform.childCount > 0)
        {
            Destroy(objectPosition.transform.GetChild(0).gameObject);
        }
        playerController.enabled = true;
        uIControllerCanvas.SetActive(true);
        informationCanvas.SetActive(false);
    }
}
