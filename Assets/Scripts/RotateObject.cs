using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    private Quaternion defaultRotation;
    public Vector3 rotationDir = Vector3.zero;


    void Start()
    {
        defaultRotation = transform.rotation;
    }

    void Update()
    {
        transform.Rotate(rotationDir, 1.0f, Space.Self);
    }

    void OnDisable()
    {
        transform.rotation = defaultRotation;
    }
}
