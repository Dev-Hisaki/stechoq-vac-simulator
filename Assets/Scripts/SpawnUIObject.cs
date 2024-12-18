using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SpawnUIObject : MonoBehaviour
{
    public GameObject spawnUIPoint;
    public GameObject[] items;
    public Vector3 offset = Vector3.zero;

    [Header("Text Desc Properties")]
    public TextMeshProUGUI itemNameText;
    public TextMeshProUGUI itemDescText;

    public void SpawnUIObjectAtPoint(int id)
    {
        if (spawnUIPoint.transform.childCount == 0)
        {
            foreach (GameObject item in items)
            {
                AssetId itemId = item.GetComponent<AssetId>();
                if (item != null && itemId.getId == id)
                {
                    itemNameText.text = itemId.getName.ToString();
                    itemDescText.text = itemId.getDesc.ToString();
                    UpdateContentSizeFitter(itemDescText);
                    Instantiate(item, spawnUIPoint.transform.position, Quaternion.identity, spawnUIPoint.transform);
                }
            }
        }
    }

    public void SpawnObjectOnHand(int id)
    {
        if (spawnUIPoint.transform.childCount == 0)
        {
            if (id == 0)
            {
                Debug.Log("VAC");
                return;
            }
            else
            {
                foreach (GameObject item in items)
                {
                    AssetId itemId = item.GetComponent<AssetId>();
                    if (item != null && itemId.getId == id)
                    {
                        Quaternion objectRotation = Quaternion.Euler(7f, -15f, 80f);
                        item.transform.rotation = objectRotation;
                        Instantiate(item, spawnUIPoint.transform.position, objectRotation, spawnUIPoint.transform);
                    }
                }
            }
        }
    }

    void UpdateContentSizeFitter(TextMeshProUGUI textMeshPro)
    {
        RectTransform textAreaRectTransform = textMeshPro.GetComponentInParent<RectTransform>();
        LayoutRebuilder.ForceRebuildLayoutImmediate(textAreaRectTransform);
    }
}
