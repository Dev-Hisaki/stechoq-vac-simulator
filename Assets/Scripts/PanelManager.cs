using System.Collections;
using UnityEngine;

public class PanelManager : MonoBehaviour
{
    #region Panels
    public GameObject mainCanvas, loading, login, mainPanel;
    private GameObject current;
    #endregion

    void Start()
    {
        if (mainCanvas == null || loading == null || login == null || mainPanel == null)
        {
            Debug.LogWarning("Something(s) missing or not assigned yet");
            return;
        }

        StartCoroutine(Loading());
    }

    public void NavigateTo(GameObject target)
    {
        if (current != null)
        {
            Destroy(current);
        }

        if (!target.activeSelf)
        {
            target.SetActive(true);
        }

        current = target;
    }

    IEnumerator Loading()
    {
        current = Instantiate(loading, mainCanvas.transform);
        yield return new WaitForSeconds(3);
        Destroy(current);
        current = login;
    }
}
