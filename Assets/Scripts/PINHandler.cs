using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PINHandler : MonoBehaviour
{
    #region PIN Field
    [SerializeField] private byte[] desiredPin = new byte[4];
    [SerializeField] private TMP_InputField digits;
    [SerializeField] private Button[] buttons;
    private byte[] pin = new byte[4];
    private int currentIndex = 0;
    #endregion

    PanelManager manager;
    GameObject mainCanvas, loading, login, mainPanel;

    void Start()
    {
        manager = GetComponent<PanelManager>();
        login = GetComponent<PanelManager>().login;
        mainPanel = GetComponent<PanelManager>().mainPanel;

        if (manager == null || login == null || mainPanel == null)
        {
            Debug.LogWarning("Something(s) in PINHandler Script is Missing(s)");
            return;
        }

        if (digits != null)
        {
            digits.text = "0000";
            HighlightCurrentDigit();
        }
        else
        {
            Debug.LogWarning("TMP_InputField for PIN display is not assigned.");
        }
    }

    public void Add(int num)
    {
        if (num >= 0 && num <= 9 && currentIndex < pin.Length)
        {
            Debug.Log(num);
            pin[currentIndex] = (byte)num;
            UpdatePINDisplay();
            currentIndex++;

            if (currentIndex < pin.Length)
            {
                HighlightCurrentDigit();
            }
            else
            {
                CheckPIN();
            }
        }
        else
        {
            Debug.LogWarning("Invalid PIN entry or PIN is full.");
        }
    }

    public void ClearPIN()
    {
        for (int i = 0; i < pin.Length; i++)
        {
            pin[i] = 0;
        }
        currentIndex = 0;
        digits.text = "0000";
        HighlightCurrentDigit();
    }

    private void UpdatePINDisplay()
    {
        char[] displayChars = digits.text.ToCharArray();

        for (int i = 0; i <= currentIndex; i++)
        {
            displayChars[i] = (char)('0' + pin[i]);
        }

        digits.text = new string(displayChars);
    }

    private void HighlightCurrentDigit()
    {
        digits.caretPosition = currentIndex;
    }

    private void CheckPIN()
    {
        bool isMatch = true;
        for (int i = 0; i < pin.Length; i++)
        {
            if (pin[i] != desiredPin[i])
            {
                isMatch = false;
                break;
            }
        }

        if (isMatch)
        {
            Debug.Log("PIN Correct!");
            StartCoroutine(DestroyWithDelay());

        }
        else
        {
            Debug.Log("PIN Incorrect!");
            StartCoroutine(ClearPINWithDelay());
        }
    }

    private IEnumerator ClearPINWithDelay()
    {
        SetButtonsInteractable(false);
        yield return new WaitForSeconds(1);
        FindObjectOfType<VACAudioManager>().Play("Error");
        ClearPIN();
        SetButtonsInteractable(true);
    }

    private IEnumerator DestroyWithDelay()
    {
        SetButtonsInteractable(false);
        yield return new WaitForSeconds(1);
        FindObjectOfType<VACAudioManager>().Play("Success");
        Destroy(login);
        mainPanel.SetActive(true);
    }

    private void SetButtonsInteractable(bool state)
    {
        foreach (Button button in buttons)
        {
            button.interactable = state;
        }
    }
}
