using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private int leftFingerId, rightFingerId;
    private float halfScreenWidth, halfScreenHeight;
    private Rect touchArea;

    [Header("Camera Settings")]
    public Transform cameraObject;
    public float camSensitivity;

    Vector2 lookInput;
    float camPitch;

    [Header("Interaction Settings")]
    public GameObject informationButton;
    public GameObject informationCanvas;
    public float maxDistance = 5f;
    public LayerMask interactableLayers;
    public Button interactButton;
    private CanvasGroup informationButtonCanvasGroup;
    private Interactable currentInteractable;

    [Header("Movement Setting")]
    public CharacterController characterController;
    public float moveSpeed;
    public GameObject analog;
    public GameObject pickPoint;
    private GameObject innerAnalog;
    private CanvasGroup analogCanvasGroup;
    private RectTransform analogPos;

    Vector2 moveTouchStartPosition;
    Vector2 analogDefaultPos;
    Vector2 moveInput;

    void Start()
    {
        if (informationButton.GetComponent<CanvasGroup>() == null)
        {
            informationButton.AddComponent<CanvasGroup>();
        }

        if (analog.GetComponent<CanvasGroup>() == null)
        {
            analog.AddComponent<CanvasGroup>();
        }

        informationButtonCanvasGroup = informationButton.GetComponent<CanvasGroup>();
        analogCanvasGroup = analog.GetComponent<CanvasGroup>();
        analogPos = analog.GetComponent<RectTransform>();

        leftFingerId = -1;
        rightFingerId = -1;

        halfScreenWidth = Screen.width / 2;
        halfScreenHeight = Screen.height / 2;

        touchArea = new Rect(0, 0, halfScreenWidth, halfScreenHeight);

        innerAnalog = analog.transform.GetChild(0).gameObject;
        informationButtonCanvasGroup.alpha = 0.25f;
        analogCanvasGroup.alpha = 0.25f;

        analogDefaultPos = analogPos.position;
    }

    void Update()
    {
        GetTouchInput();

        if (rightFingerId != -1)
        {
            LookAround();
        }

        if (leftFingerId != -1)
        {
            analogCanvasGroup.alpha = 1f;
            Move();
        }
        else
        {
            analogCanvasGroup.alpha = 0.25f;
        }

        InteractHandler();
    }

    void InteractHandler()
    {
        Debug.DrawRay(cameraObject.position, cameraObject.forward * maxDistance, Color.green);
        if (Physics.Raycast(cameraObject.position, cameraObject.forward, out RaycastHit hit, maxDistance, interactableLayers))
        {
            informationButtonCanvasGroup.alpha = 1f;
            currentInteractable = hit.collider.GetComponent<Interactable>();
        }
        else
        {
            informationButtonCanvasGroup.alpha = 0.25f;
        }
        interactButton.interactable = currentInteractable != null;
    }

    public void PickupObject()
    {
        if (currentInteractable) currentInteractable.OnInteraction();
        currentInteractable.transform.SetParent(pickPoint.transform);
        currentInteractable.transform.localPosition = Vector3.zero;
    }

    public void ShowInformation()
    {
        if (currentInteractable) currentInteractable.OnInteraction();
    }

    void GetTouchInput()
    {
        for (int i = 0; i < Input.touchCount; i++)
        {
            Touch touch = Input.GetTouch(i);

            // Cek setiap fase sentuhan
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    if (touchArea.Contains(touch.position) && leftFingerId == -1)
                    {
                        moveInput = Vector2.zero;
                        leftFingerId = touch.fingerId;

                        moveTouchStartPosition = touch.position;
                        analog.transform.position = moveTouchStartPosition;
                    }
                    else if (touch.position.x > halfScreenWidth && rightFingerId == -1)
                    {
                        rightFingerId = touch.fingerId;
                    }
                    break;
                case TouchPhase.Ended:
                case TouchPhase.Canceled:
                    if (touch.fingerId == leftFingerId)
                    {
                        leftFingerId = -1;
                        innerAnalog.transform.position = analog.transform.position;
                        analog.transform.position = analogDefaultPos;
                    }
                    else if (touch.fingerId == rightFingerId)
                    {
                        rightFingerId = -1;
                    }
                    break;
                case TouchPhase.Moved:
                    if (touch.fingerId == rightFingerId)
                    {
                        lookInput = touch.deltaPosition * camSensitivity * Time.deltaTime;
                    }
                    else if (touch.fingerId == leftFingerId)
                    {
                        moveInput = touch.position - moveTouchStartPosition;

                        float maxAnalogDistance = 50f;
                        Vector2 difference = touch.position - moveTouchStartPosition;

                        if (difference.magnitude > maxAnalogDistance)
                        {
                            difference = difference.normalized * maxAnalogDistance;
                        }

                        innerAnalog.transform.position = moveTouchStartPosition + difference;
                    }
                    break;
                case TouchPhase.Stationary:
                    if (touch.fingerId == rightFingerId)
                    {
                        lookInput = Vector2.zero;
                    }
                    break;
            }
        }
    }

    void LookAround()
    {
        camPitch = Mathf.Clamp(camPitch - lookInput.y, -90f, 90f);
        cameraObject.localRotation = Quaternion.Euler(camPitch, 0, 0);

        transform.Rotate(transform.up, lookInput.x);
    }

    void Move()
    {
        Vector2 moveDirection = moveInput.normalized * moveSpeed * Time.deltaTime;
        characterController.Move(transform.right * moveDirection.x + transform.forward * moveDirection.y);
    }
}
