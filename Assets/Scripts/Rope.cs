using UnityEngine;

public class Rope : MonoBehaviour
{
    public GameObject[] segmentPoints;
    private LineRenderer line;

    private void Start()
    {
        line = GetComponent<LineRenderer>();
        line.positionCount = segmentPoints.Length;
    }

    private void Update()
    {
        for (int i = 0; i < segmentPoints.Length; i++)
        {
            line.SetPosition(i, segmentPoints[i].transform.position);
        }
    }
}
