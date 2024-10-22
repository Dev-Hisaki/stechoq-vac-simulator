using UnityEngine;

public class Rope : MonoBehaviour
{
    public GameObject startPoint, endPoint;
    // public GameObject[] segmentPoints;
    private LineRenderer line;
    private void Start()
    {
        line = GetComponent<LineRenderer>();
        line.SetPosition(0, startPoint.transform.position);
        line.SetPosition(1, endPoint.transform.position);
        // int segmentSize = segmentPoints.Length;
        // for (int i = 0; i < segmentPoints.Length; i++)
        // {
        //     Debug.Log($"Line {i} set");
        //     line.SetPosition(i, segmentPoints[i].transform.position);
        // }
    }
}
