using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineDrawer : MonoBehaviour
{
    public GameObject linePrefabs;
    public GameObject a;
    public GameObject b;
    private LineRenderer line;
    [SerializeField, Range(0.1f, 1f)]
    private float width;
    void Start()
    {
        line = linePrefabs.GetComponent<LineRenderer>();
        line.positionCount = 2;
        line.startWidth = line.endWidth = width;
    }

    void Update()
    {
        line.SetPosition(0, a.transform.position);
        line.SetPosition(1, b.transform.position);
    }
}
