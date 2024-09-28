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

    [Header("Logo Position")]
    public Vector2 endpos;
    public Vector2 buttonPanelTargetPos;

    private CanvasGroup logosCanvasGroup;
    private CanvasGroup buttonPanelCanvasGroup;
    private CanvasGroup selectionPanelCanvasGroup;
    private CanvasGroup settingsPanelCanvasGroup;
    private CanvasGroup creditPanelCanvasGroup;
    private CanvasGroup quitPanelCanvasGroup;
    void Start()
    {
        if (selectionModePanel.activeSelf || settingsPanel.activeSelf || creditPanel.activeSelf || quitPanel.activeSelf) { 
            selectionModePanel.SetActive(false);
            settingsPanel.SetActive(false);
            creditPanel.SetActive(false);
            quitPanel.SetActive(false);
        }
    }

    // Navigation
    public void EnterInformationMode() {
        SceneManager.LoadScene(1);
    }

    // Animation
    public void OpenSelectionMode()
    {
        buttonPanelCanvasGroup.LeanAlpha(0f, 0.5f).setEaseOutQuart();
        logos.LeanMoveLocal(endpos, 1f).setEaseOutQuart();

        OpenSelectionModeAnimation();
    }

    public void CloseSelectionMode()
    {
        buttonPanelCanvasGroup.LeanAlpha(1f, 0.5f).setEaseOutQuart();
        logos.LeanMoveLocal(new Vector2(650, 440), 1f).setEaseOutQuart();
        
        CloseSelectionModeAnimation();
    }

    public void OpenSettings() {
        buttonPanelCanvasGroup.LeanAlpha(0f, 0.5f).setEaseOutQuart();
        logos.LeanMoveLocal(endpos, 1f).setEaseOutQuart();

        OpenSettingsAnimation();
    }

    public void CloseSettings() {
        buttonPanelCanvasGroup.LeanAlpha(1f, 0.5f).setEaseOutQuart();
        logos.LeanMoveLocal(new Vector2(650, 440), 1f).setEaseOutQuart();

        CloseSettingsAnimation();
    }

    public void OpenCredit()
    {
        buttonPanelCanvasGroup.LeanAlpha(0f, 0.5f).setEaseOutQuart();
        logos.LeanMoveLocal(endpos, 1f).setEaseOutQuart();

        OpenCreditAnimation();
    }

    public void CloseCredit()
    {
        buttonPanelCanvasGroup.LeanAlpha(1f, 0.5f).setEaseOutQuart();
        logos.LeanMoveLocal(new Vector2(650, 440), 1f).setEaseOutQuart();

        CloseCreditAnimation();
    }

    public void OpenQuit()
    {
        buttonPanelCanvasGroup.LeanAlpha(0f, 0.5f).setEaseOutQuart();
        logos.LeanMoveLocal(endpos, 1f).setEaseOutQuart();

        OpenQuitAnimation();
    }

    public void CloseQuit()
    {
        buttonPanelCanvasGroup.LeanAlpha(1f, 0.5f).setEaseOutQuart();
        logos.LeanMoveLocal(new Vector2(650, 440), 1f).setEaseOutQuart();

        CloseQuitAnimation();
    }

    public void Quit() {
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
