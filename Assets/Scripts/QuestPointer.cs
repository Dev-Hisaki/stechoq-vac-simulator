using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class QuestPointer : MonoBehaviour
{
    public Transform target, target2;
    public TextMeshProUGUI missionText;
    public float offset = 1.25f;
    private string[] mission = {
            "Hancurkan objek pertama dengan menekan [Z]",
            "Hancurkan objek kedua dengan menekan tombol [Z]"
            };
    private int missionIndex;

    private void Start()
    {
        missionIndex = 0;
        SetText();
    }

    void Update()
    {
        if (target != null)
        {
            Vector3 targetPosition = target.position + Vector3.up * offset;
            transform.position = targetPosition;
        }
        else if (target2 != null)
        {
            target = target2;
        }
        else
        {
            Destroy(gameObject);
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            DestroyObject(target);
        }
    }

    void DestroyObject(Transform transformObject)
    {
        Destroy(transformObject.gameObject);
        missionIndex++;
        SetText();
    }

    void SetText()
    {
        missionText.text = mission[missionIndex];
    }
}
