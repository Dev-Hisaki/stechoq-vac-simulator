using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonNavigate : MonoBehaviour
{
    [Header("Panels")]
    public GameObject logos;
    public GameObject buttonPanel;
    public GameObject selectionModePanel;
    public GameObject settingsPanel;
    public GameObject creditPanel;
    public GameObject quitPanel;
    public GameObject loadingPanel;

    private CanvasGroup logosCanvasGroup;
    private CanvasGroup buttonPanelCanvasGroup;
    private CanvasGroup selectionPanelCanvasGroup;
    private CanvasGroup settingsPanelCanvasGroup;
    private CanvasGroup creditPanelCanvasGroup;
    private CanvasGroup quitPanelCanvasGroup;
    void Start()
    {
        buttonPanelCanvasGroup = buttonPanel.GetComponent<CanvasGroup>();
        logosCanvasGroup = logos.GetComponent<CanvasGroup>();
        if (selectionModePanel.activeSelf || settingsPanel.activeSelf || creditPanel.activeSelf || quitPanel.activeSelf || loadingPanel.activeSelf)
        {
            selectionModePanel.SetActive(false);
            settingsPanel.SetActive(false);
            creditPanel.SetActive(false);
            quitPanel.SetActive(false);
            loadingPanel.SetActive(false);
        }
    }

    // Navigation
    public void EnterInformationMode()
    {
        loadingPanel.SetActive(true);
        Invoke("LoadInformationScene", 2f);
    }

    public void EnterSimulationMode()
    {
        loadingPanel.SetActive(true);
        Invoke("LoadSimulationScene", 2f);
    }

    private void LoadInformationScene()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        int nextScene = currentScene + 1;
        if (nextScene == SceneManager.sceneCountInBuildSettings)
        {
            nextScene = 0;
        }
        SceneManager.LoadScene(nextScene);
    }

    private void LoadSimulationScene()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        int nextScene = currentScene + 2;
        if (nextScene == SceneManager.sceneCountInBuildSettings)
        {
            nextScene = 0;
        }
        SceneManager.LoadScene(nextScene);
    }

    public void LoadVACScene()
    {
        SceneManager.LoadScene(3);
    }

    // Animation
    public void OpenSelectionMode()
    {
        buttonPanelCanvasGroup.LeanAlpha(0f, 1f).setEaseOutQuart();
        logosCanvasGroup.LeanAlpha(0f, 1f).setEaseOutQuart();

        OpenSelectionModeAnimation();
    }

    public void CloseSelectionMode()
    {
        buttonPanelCanvasGroup.LeanAlpha(1f, 0.5f).setEaseOutQuart();
        logos.LeanAlpha(0f, 1f).setEaseOutQuart();

        CloseSelectionModeAnimation();
    }

    public void OpenSettings()
    {
        buttonPanelCanvasGroup.LeanAlpha(0f, 0.5f).setEaseOutQuart();
        logos.LeanAlpha(0f, 1f).setEaseOutQuart();

        OpenSettingsAnimation();
    }

    public void CloseSettings()
    {
        buttonPanelCanvasGroup.LeanAlpha(1f, 0.5f).setEaseOutQuart();
        logos.LeanAlpha(0f, 1f).setEaseOutQuart();

        CloseSettingsAnimation();
    }

    public void OpenCredit()
    {
        buttonPanelCanvasGroup.LeanAlpha(0f, 0.5f).setEaseOutQuart();
        logos.LeanAlpha(0f, 1f).setEaseOutQuart();

        OpenCreditAnimation();
    }

    public void CloseCredit()
    {
        buttonPanelCanvasGroup.LeanAlpha(1f, 0.5f).setEaseOutQuart();
        logos.LeanAlpha(0f, 1f).setEaseOutQuart();

        CloseCreditAnimation();
    }

    public void OpenQuit()
    {
        buttonPanelCanvasGroup.LeanAlpha(0f, 0.5f).setEaseOutQuart();
        logos.LeanAlpha(0f, 1f).setEaseOutQuart();

        OpenQuitAnimation();
    }

    public void CloseQuit()
    {
        buttonPanelCanvasGroup.LeanAlpha(1f, 0.5f).setEaseOutQuart();
        logos.LeanAlpha(0f, 1f).setEaseOutQuart();

        CloseQuitAnimation();
    }

    public void Quit()
    {
        Debug.Log("Apps exit successfully");
        Application.Quit();
    }
    private void OpenSelectionModeAnimation()
    {
        selectionModePanel.gameObject.SetActive(true);
        selectionPanelCanvasGroup = selectionModePanel.GetComponent<CanvasGroup>();
        selectionPanelCanvasGroup.LeanAlpha(1f, 0.5f).setEaseInQuart();
    }

    private void CloseSelectionModeAnimation()
    {
        selectionPanelCanvasGroup = selectionModePanel.GetComponent<CanvasGroup>();
        selectionPanelCanvasGroup.LeanAlpha(0f, 0.5f).setEaseInQuart();
        selectionModePanel.gameObject.SetActive(false);
    }

    private void OpenSettingsAnimation()
    {
        settingsPanel.gameObject.SetActive(true);
        settingsPanelCanvasGroup = settingsPanel.GetComponent<CanvasGroup>();
        settingsPanelCanvasGroup.LeanAlpha(1f, 0.5f).setEaseInQuart();
    }

    private void CloseSettingsAnimation()
    {
        settingsPanelCanvasGroup = settingsPanel.GetComponent<CanvasGroup>();
        settingsPanelCanvasGroup.LeanAlpha(0f, 0.5f).setEaseOutQuart();
        settingsPanel.gameObject.SetActive(false);
    }

    private void OpenCreditAnimation()
    {
        creditPanel.gameObject.SetActive(true);
        creditPanelCanvasGroup = creditPanel.GetComponent<CanvasGroup>();
        creditPanelCanvasGroup.LeanAlpha(1f, 0.5f).setEaseInQuart();
    }

    private void CloseCreditAnimation()
    {
        creditPanelCanvasGroup = creditPanel.GetComponent<CanvasGroup>();
        creditPanelCanvasGroup.LeanAlpha(0f, 0.5f).setEaseOutQuart();
        creditPanel.gameObject.SetActive(false);
    }

    private void OpenQuitAnimation()
    {
        quitPanel.gameObject.SetActive(true);
        quitPanelCanvasGroup = quitPanel.GetComponent<CanvasGroup>();
        quitPanelCanvasGroup.LeanAlpha(1f, 0.5f).setEaseInQuart();
    }

    private void CloseQuitAnimation()
    {
        quitPanelCanvasGroup = quitPanel.GetComponent<CanvasGroup>();
        quitPanelCanvasGroup.LeanAlpha(0f, 0.5f).setEaseOutQuart();
        quitPanel.gameObject.SetActive(false);
    }

}
