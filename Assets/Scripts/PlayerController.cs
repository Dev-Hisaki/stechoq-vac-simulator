using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

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

    [Header("Movement Setting")]
    public CharacterController characterController;
    public float moveSpeed;
    public GameObject analog;
    private GameObject innerAnalog;

    Vector2 moveTouchStartPosition;
    Vector2 moveInput;

    // Start is called before the first frame update
    void Start()
    {
        // Nilai -1 buat ngasih tau Unity bahwa finger 
        leftFingerId = -1;
        rightFingerId = -1;

        // hitung setengah screen sekali saja
        halfScreenWidth = Screen.width / 2;
        halfScreenHeight = Screen.height / 2;

        // Area di seperempat bagian kiri bawah
        touchArea = new Rect(0, 0, halfScreenWidth, halfScreenHeight);

        analog.SetActive(false);
        innerAnalog = analog.transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        GetTouchInput();

        if (rightFingerId != -1)
        {
            LookAround();
        }

        if (leftFingerId != -1)
        {
            // Ada masalah disini kalau misalkan analog dilepas terus
            // diteken tanpa input arah, bakalan maju
            Move();
            analog.SetActive(true);
        }
        else
        {
            analog.SetActive(false);
        }
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
                        innerAnalog.transform.position = touch.position;
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
        // Vertical (pitch) rotation
        camPitch = Mathf.Clamp(camPitch - lookInput.y, -90f, 90f);
        cameraObject.localRotation = Quaternion.Euler(camPitch, 0, 0);

        // Horizontal (yaw) rotation
        transform.Rotate(transform.up, lookInput.x);
    }

    void Move()
    {
        Vector2 moveDirection = moveInput.normalized * moveSpeed * Time.deltaTime;
        characterController.Move(transform.right * moveDirection.x + transform.forward * moveDirection.y);
    }
}
