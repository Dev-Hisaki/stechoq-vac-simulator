using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Drawing : MonoBehaviour
{
    public RectTransform canvasTransform;
    public GameObject linePrefab;
    private GameObject currentLine;
    private RectTransform lineRect;
    private Vector2 prevPosition;
    private bool isDrawing = false;

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 touchPosition;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasTransform, touch.position, Camera.main, out touchPosition);

            if (touch.phase == TouchPhase.Began)
            {
                StartNewLine(touchPosition);
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                DrawLine(touchPosition);
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                isDrawing = false;
            }
        }
    }

    void StartNewLine(Vector2 startPosition)
    {
        currentLine = Instantiate(linePrefab, canvasTransform);
        lineRect = currentLine.GetComponent<RectTransform>();

        prevPosition = startPosition;
        isDrawing = true;
    }

    void DrawLine(Vector2 currentPosition)
    {
        if (isDrawing)
        {
            float distance = Vector2.Distance(prevPosition, currentPosition);
            Vector2 direction = (currentPosition - prevPosition).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            lineRect.sizeDelta = new Vector2(distance, 5f);
            lineRect.anchoredPosition = (prevPosition + currentPosition) / 2;
            lineRect.localRotation = Quaternion.Euler(0, 0, angle);

            prevPosition = currentPosition;
        }
    }
}
