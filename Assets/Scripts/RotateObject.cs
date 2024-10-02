using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    private Quaternion defaultRotation;

    void Start()
    {
        defaultRotation = transform.rotation;
    }

    void Update()
    {
        transform.Rotate(Vector3.forward, 1.0f, Space.Self);
    }

    void OnDisable()
    {
        transform.rotation = defaultRotation;
    }
}
