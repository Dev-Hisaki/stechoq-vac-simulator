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
                    Quaternion objectRotation = Quaternion.Euler(7f, -15f, 80f);
                    item.transform.rotation = objectRotation;
                    Instantiate(item, spawnUIPoint.transform.position, objectRotation, spawnUIPoint.transform);
                }
            }
        }
    }

    public void DropItemInHand(int id)
    {
        if (spawnUIPoint.transform.childCount > 0)
        {
            foreach (GameObject item in items)
            {
                Debug.Log(id);
                AssetId itemId = item.GetComponent<AssetId>();
                if (item != null && itemId.getId == id)
                {
                    Debug.Log("Item Dropped");
                    Instantiate(item, spawnUIPoint.transform.position + offset, Quaternion.identity);
                }
            }
        }
        else
        {
            Debug.Log("No item in hand");
        }
    }

    void UpdateContentSizeFitter(TextMeshProUGUI textMeshPro)
    {
        RectTransform textAreaRectTransform = textMeshPro.GetComponentInParent<RectTransform>();
        LayoutRebuilder.ForceRebuildLayoutImmediate(textAreaRectTransform);
    }
}
