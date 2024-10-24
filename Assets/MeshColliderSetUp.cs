using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshColliderSetUp : MonoBehaviour
{
    public GameObject meshSourceObject;
    private MeshCollider meshCollider;

    void Start()
    {
        meshCollider = gameObject.AddComponent<MeshCollider>();

        if (meshSourceObject != null)
        {
            SkinnedMeshRenderer skinnedMeshRenderer = meshSourceObject.GetComponent<SkinnedMeshRenderer>();

            if (skinnedMeshRenderer != null)
            {
                meshCollider.sharedMesh = skinnedMeshRenderer.sharedMesh;
            }
            else
            {
                Debug.LogError("SkinnedMeshRenderer tidak ditemukan pada objek sumber.");
            }
        }
        else
        {
            Debug.LogError("Objek sumber tidak diatur.");
        }
    }
}
