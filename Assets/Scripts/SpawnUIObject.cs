using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SpawnUIObject : MonoBehaviour
{
    public GameObject spawnUIPoint;
    public GameObject[] items;

    [Header("Text Desc Properties")]
    public TextMeshProUGUI itemNameText;
    public TextMeshProUGUI itemDescText;

    public void SpawnUIObjectAtPoint(int id)
    {
        if (spawnUIPoint.transform.childCount == 0)
        {
            foreach (GameObject item in items)
            {
                Debug.Log(id);
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
            foreach (GameObject item in items)
            {
                Debug.Log(id);
                AssetId itemId = item.GetComponent<AssetId>();
                if (item != null && itemId.getId == id)
                {
                    Instantiate(item, spawnUIPoint.transform.position, Quaternion.identity, spawnUIPoint.transform);
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
