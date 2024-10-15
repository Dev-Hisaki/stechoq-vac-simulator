using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Goals
{
    public int id;
    [TextArea(1, 2)]
    public string Instructions;
}
