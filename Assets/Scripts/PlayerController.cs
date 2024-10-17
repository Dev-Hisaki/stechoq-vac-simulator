using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
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
    private Vector2 currentLookInput;
    private Vector2 lookInputSmoothVelocity;
    public float lookSmoothTime = 0.1f;

    [Header("Interaction Settings")]
    public GameObject informationButton;
    public GameObject informationCanvas;
    public float maxDistance = 5f;
    public LayerMask interactableLayers;
    public Button interactButton;
    private CanvasGroup informationCanvasGroup;
    private CanvasGroup informationButtonCanvasGroup;
    private Interactable currentInteractable;
    private AssetId asetId;

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

    [Header("Questing System")]
    public GameObject minigameObject;
    private Minigames minigame;
    public GameObject goalsManagerObject;
    private GoalsManager goalsManager;
    bool firstQuest = true;
    int sceneId;

    [Header("Interactable Objects")]
    public GameObject gunting;
    private Vector3 guntingDefaultPosition;
    public GameObject foamKotak;
    public GameObject bigDresser;
    public GameObject hole;
    public GameObject VAC;

    public int RightFingerId
    {
        get { return rightFingerId; }
        set { rightFingerId = value; }
    }
    public int LeftFingerId
    {
        get { return leftFingerId; }
        set { leftFingerId = value; }
    }


    void Start()
    {
        guntingDefaultPosition = gunting.transform.position;
        minigame = minigameObject.GetComponent<Minigames>();
        if (foamKotak == null || bigDresser == null || hole == null || VAC == null)
        {
            Debug.LogWarning("is null");
        }
        sceneId = SceneManager.GetActiveScene().buildIndex;
        if (informationButton.GetComponent<CanvasGroup>() == null)
        {
            informationButton.AddComponent<CanvasGroup>();
        }

        if (analog.GetComponent<CanvasGroup>() == null)
        {
            analog.AddComponent<CanvasGroup>();
        }

        informationButtonCanvasGroup = informationButton.GetComponent<CanvasGroup>();
        if (informationCanvas != null)
        {
            informationCanvasGroup = informationCanvas.GetComponent<CanvasGroup>();
        }
        analogCanvasGroup = analog.GetComponent<CanvasGroup>();
        analogPos = analog.GetComponent<RectTransform>();

        if (sceneId == 2)
        {
            goalsManager = goalsManagerObject.GetComponent<GoalsManager>();
        }
        else
        {
            goalsManager = null;
        }

        leftFingerId = -1;
        rightFingerId = -1;

        halfScreenWidth = Screen.width / 2;
        halfScreenHeight = Screen.height / 2;

        touchArea = new Rect(0, 0, halfScreenWidth, halfScreenHeight);

        innerAnalog = analog.transform.GetChild(0).gameObject;
        informationButtonCanvasGroup.alpha = 0.25f;
        analogCanvasGroup.alpha = 0.25f;

        analogDefaultPos = analogPos.position;

        firstQuest = true;
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
            asetId = hit.collider.GetComponent<AssetId>();
        }
        else
        {
            currentInteractable = null;
            informationButtonCanvasGroup.alpha = 0.25f;
        }
        interactButton.interactable = currentInteractable != null;
    }

    public void PickupObject()
    {
        int id = asetId.getId;
        int handItemId = -1;

        if (pickPoint.transform.childCount == 0)
        {
            if (currentInteractable != null)
            {
                currentInteractable.SpawnInHand(id);
                currentInteractable.transform.SetParent(pickPoint.transform);
                currentInteractable.transform.localPosition = Vector3.zero;
                handItemId = id;
                Debug.Log("HandItemId: " + handItemId + " || ID: " + id);

                if (handItemId == 1 && id == 1) goalsManager.Completed();
                if (handItemId == 2 && id == 2) goalsManager.Completed();
            }
            return;
        }

        if (pickPoint.transform.childCount > 0)
        {
            if (currentInteractable != null && id == 2)
            {
                Debug.LogWarning(currentInteractable + " || " + id);

                Instantiate(gunting, guntingDefaultPosition, Quaternion.identity);
                minigame.FoamCutting(foamKotak);

                Transform guntingInHand = pickPoint.transform.GetChild(0);
                Destroy(guntingInHand.gameObject);
            }
            else
            {
                Debug.LogWarning(currentInteractable + " || " + id);
            }
        }
    }

    public void ShowInformation()
    {
        if (currentInteractable)
        {
            int id = asetId.getId;
            currentInteractable.OnInteraction(id);
            informationCanvasGroup.LeanAlpha(1f, 0.5f).setEaseInOutQuart();
        }
    }

    void GetTouchInput()
    {
        for (int i = 0; i < Input.touchCount; i++)
        {
            Touch touch = Input.GetTouch(i);

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
                    if (firstQuest == true && sceneId == 2)
                    {
                        goalsManager.Completed();
                        firstQuest = false;
                    }
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
        currentLookInput = Vector2.SmoothDamp(currentLookInput, lookInput, ref lookInputSmoothVelocity, lookSmoothTime);
        camPitch = Mathf.Clamp(camPitch - lookInput.y, -90f, 90f);
        Quaternion targetCamRotation = Quaternion.Euler(camPitch, 0, 0);
        cameraObject.localRotation = Quaternion.Slerp(cameraObject.localRotation, targetCamRotation, Time.deltaTime * camSensitivity);
        transform.Rotate(transform.up, lookInput.x);
    }

    void Move()
    {
        Vector2 moveDirection = moveInput.normalized * moveSpeed * Time.deltaTime;
        characterController.Move(transform.right * moveDirection.x + transform.forward * moveDirection.y);
    }
}
