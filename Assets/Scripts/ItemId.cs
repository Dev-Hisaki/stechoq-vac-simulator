using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemId : MonoBehaviour
{
    [Header("Item ID")]
    [SerializeField]
    private int id;

    public int getId
    {
        get { return id; }
    }
}
