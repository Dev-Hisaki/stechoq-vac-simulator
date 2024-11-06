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

    [Header("Camera Settings"), Tooltip("Configure Camera Settings")]
    public Transform cameraObject;
    [Range(0.1f, 1f)] public float lookSmoothTime = 0.1f;
    public float camSensitivity;
    Vector2 lookInput;
    float camPitch;
    private Vector2 currentLookInput;
    private Vector2 lookInputSmoothVelocity;

    [Header("Interaction Settings"), Tooltip("Corelated with canvas component on this project")]
    public GameObject informationButton;
    public GameObject informationCanvas;
    public float maxDistance = 5f;
    public LayerMask interactableLayers;
    public Button interactButton;
    private CanvasGroup informationCanvasGroup;
    private CanvasGroup informationButtonCanvasGroup;
    private Interactable currentInteractable;
    private AssetId asetId;

    [Header("Movement Setting"), Tooltip("Setting up movement of the player")]
    public CharacterController characterController;
    public float moveSpeed;
    public GameObject analog;
    public GameObject pickPoint;
    private GameObject innerAnalog;
    private CanvasGroup analogCanvasGroup;
    private RectTransform analogPos;
    private Vector2 moveTouchStartPosition;
    private Vector2 analogDefaultPos;
    private Vector2 moveInput;

    [Header("Questing System"), Tooltip("Many code got from questing folder")]
    public GameObject minigameObject;
    private Minigames minigame;
    public GameObject goalsManagerObject;
    private GoalsManager goalsManager;
    bool firstQuest = true;
    int sceneId;

    [Header("Interactable Objects"), Tooltip("Special settings for simulation room")]
    public GameObject gunting;
    private Vector3 guntingDefaultPosition;

    [Space(25)]
    public GameObject foamKotak;
    public GameObject foamPatient;

    [Space(25)]
    public GameObject bigDresser;
    public GameObject uncoverButton;
    public Material newCoverMaterial;
    public GameObject patientBigDresser;
    public GameObject patientHoledBigDresser;
    bool isCovered = true;

    [Space(25)]
    public GameObject selangAtVAC;
    public GameObject pipeline;

    [Space(25)]
    public GameObject smallDresser;

    int handItemId;

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
        if (analog.GetComponent<CanvasGroup>() == null) analog.AddComponent<CanvasGroup>();
        if (informationButton.GetComponent<CanvasGroup>() == null) informationButton.AddComponent<CanvasGroup>();

        sceneId = SceneManager.GetActiveScene().buildIndex;
        informationCanvasGroup = informationCanvas.GetComponent<CanvasGroup>();
        informationButtonCanvasGroup = informationButton.GetComponent<CanvasGroup>();
        analogPos = analog.GetComponent<RectTransform>();
        analogCanvasGroup = analog.GetComponent<CanvasGroup>();

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

        switch (sceneId)
        {
            case 0:
                Debug.Log("Main Menu");
                break;

            case 1:
                analogCanvasGroup = analog.GetComponent<CanvasGroup>();
                minigameObject = null;
                minigame = null;
                goalsManagerObject = null;
                goalsManager = null;
                gunting = null;
                guntingDefaultPosition = Vector3.zero;
                foamKotak = null;
                foamPatient = null;
                bigDresser = null;
                uncoverButton = null;
                selangAtVAC = null;
                patientBigDresser = null;
                patientHoledBigDresser = null;
                smallDresser = null;
                break;

            case 2:
                handItemId = -1;
                guntingDefaultPosition = gunting.transform.position;
                minigame = minigameObject.GetComponent<Minigames>();
                goalsManager = goalsManagerObject.GetComponent<GoalsManager>();

                if (foamKotak == null || bigDresser == null)
                {
                    Debug.LogWarning("is null");
                }

                break;

            default:
                goalsManager = null;
                break;
        }
    }

    void Update()
    {
        GetTouchInput();
        InteractHandler();

        if (rightFingerId != -1) LookAround();

        if (leftFingerId != -1)
        {
            analogCanvasGroup.alpha = 1f;
            Move();
        }
        else
        {
            analogCanvasGroup.alpha = 0.25f;
        }
    }

    void InteractHandler()
    {
        Debug.DrawRay(cameraObject.position, cameraObject.forward * maxDistance, Color.green);
        if (Physics.Raycast(cameraObject.position, cameraObject.forward, out RaycastHit hit, maxDistance, interactableLayers))
        {
            informationButtonCanvasGroup.alpha = 1f;
            currentInteractable = hit.collider.GetComponent<Interactable>();
            asetId = hit.collider.GetComponent<AssetId>();

            if (currentInteractable == null || asetId == null)
            {
                Debug.LogWarning("Current Interactable or Asset id not found");
            }
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
        int missionId = goalsManager.getMissionId;

        Debug.Log("HandItemId: " + handItemId + " || ID: " + id + " || Mission ID: " + missionId);

        if (pickPoint.transform.childCount == 0)
        {
            if (currentInteractable != null)
            {
                currentInteractable.SpawnInHand(id);
                currentInteractable.transform.SetParent(pickPoint.transform);
                currentInteractable.transform.localPosition = Vector3.zero;
                currentInteractable.transform.localScale = Vector3.one;
                handItemId = id;

                if (handItemId == 1 && missionId == 1 || missionId == 8) goalsManager.Completed();
                if (handItemId == 2 && missionId == 3) goalsManager.Completed();
                if (handItemId == 3 && missionId == 5)
                {
                    uncoverButton.SetActive(true);
                    goalsManager.Completed();
                }
                if (handItemId == 5 && missionId == 10) goalsManager.Completed();
                if (handItemId == 4 && missionId == 13) goalsManager.Completed();
            }
            else
            {
                Debug.Log("NULLLLLLL");
            }
            return;
        }

        if (pickPoint.transform.childCount > 0)
        {
            if (handItemId == 3 && missionId == 6)
            {
                minigame.BigDresserUncover(bigDresser, newCoverMaterial);
                isCovered = false;
            }

            if (handItemId == 4 && missionId == 14)
            {
                minigame.SmallDresserUncover(smallDresser, newCoverMaterial);
                isCovered = false;
            }

            if (missionId == 6) Destroy(uncoverButton.gameObject);
            if (currentInteractable != null)
            {
                Transform itemInHand = pickPoint.transform.GetChild(0);

                switch (handItemId)
                {
                    case 1: // Gunting
                        if (missionId == 2)
                        {
                            Debug.LogWarning(currentInteractable + " || " + id);

                            Instantiate(gunting, guntingDefaultPosition, Quaternion.identity);
                            minigame.FoamCutting(foamKotak);
                            Destroy(itemInHand.gameObject);
                        }

                        if (missionId == 9)
                        {
                            minigame.HoleCutting(patientBigDresser, patientHoledBigDresser);
                            Destroy(itemInHand.gameObject);
                        }
                        break;
                    case 2: // Foam
                        if (missionId == 4)
                        {
                            Debug.LogWarning(currentInteractable + " || " + id);

                            minigame.FoamApplying(foamPatient);
                            Destroy(itemInHand.gameObject);
                        }
                        break;
                    case 3: // Dresser Besar
                        if (isCovered)
                        {
                            Debug.LogWarning("Uncover dresser first");
                        }
                        else
                        {
                            if (missionId == 7)
                            {
                                minigame.BigDresserApplying(patientBigDresser);
                                Destroy(itemInHand.gameObject);
                                isCovered = true;
                            }
                        }
                        Debug.Log("Dresser Besar");
                        break;
                    case 4: // Dresser Kecil
                        if (missionId == 15)
                        {

                        }
                        Debug.Log("Dresser Kecil");
                        break;
                    case 5: // Selang
                        if (missionId == 11 && id == 0)
                        {
                            minigame.PipeToVAC(selangAtVAC);
                        }
                        if (missionId == 12)
                        {
                            minigame.PipeToPatient(pipeline, selangAtVAC);
                            Destroy(itemInHand.gameObject);
                        }
                        Debug.Log("Selang");
                        break;
                    default: // No Item in Hand
                        Debug.LogWarning("No Item in Hand");
                        break;
                }
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
                    if (touch.fingerId == rightFingerId) lookInput = Vector2.zero;
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
