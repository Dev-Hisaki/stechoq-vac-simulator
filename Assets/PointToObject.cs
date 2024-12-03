using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointToObject : MonoBehaviour
{
    public Transform target;
    public float offset = 1.25f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            Vector3 targetPosition = target.position + Vector3.up * offset;
            transform.position = targetPosition;
        }
    }

    public void ChangeTarget(Transform newTarget)
    {
        target = newTarget;
        if (target != null)
        {
            Vector3 targetPosition = target.position + Vector3.up * offset;
            transform.position = targetPosition;
        }
    }

}
