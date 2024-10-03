using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetId : MonoBehaviour
{
    [Header("Item ID")]
    [SerializeField] private int id;
    [SerializeField] private string itemName;
    [TextArea(1, 10)]
    [SerializeField] private string desc;

    public int getId
    {
        get
        {
            return id;
        }
    }

    public string getName
    {
        get
        {
            return itemName;
        }
    }

    public string getDesc
    {
        get
        {
            return desc;
        }
    }
}
