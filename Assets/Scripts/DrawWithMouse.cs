using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawWithTouch : MonoBehaviour
{
    private LineRenderer line;
    private Vector3 prevPosition;
    [SerializeField]
    private float minDistance = 0.1f;
    [SerializeField, Range(0.1f, 1f)]
    private float width = 0.1f;

    void Start()
    {
        line = GetComponent<LineRenderer>();
        line.positionCount = 1;
        line.startWidth = line.endWidth = width;
        prevPosition = transform.position;
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Began)
            {
                Vector3 currentPosition = Camera.main.ScreenToWorldPoint(touch.position);
                currentPosition.z = 0f;

                if (Vector3.Distance(currentPosition, prevPosition) > minDistance)
                {
                    if (prevPosition == transform.position)
                    {
                        Debug.Log("Not Lined");
                        line.SetPosition(0, currentPosition);
                    }
                    else
                    {
                        Debug.Log("Lined");
                        line.positionCount++;
                        line.SetPosition(line.positionCount - 1, currentPosition);
                    }
                    prevPosition = currentPosition;
                }
            }
        }
    }
}
