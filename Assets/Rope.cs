using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    public Transform startObject;  // Objek awal
    public Transform endObject;    // Objek akhir
    public Transform rope;         // Cylinder untuk tali

    void Update()
    {
        // Hitung posisi tengah di antara dua objek
        Vector3 middlePosition = (startObject.position + endObject.position) / 2;
        rope.position = middlePosition;

        // Hitung jarak antara dua objek
        float distance = Vector3.Distance(startObject.position, endObject.position);

        // Setel skala cylinder agar sesuai panjangnya
        rope.localScale = new Vector3(rope.localScale.x, distance / 2, rope.localScale.z);

        // Rotasi tali agar menghadap dari satu objek ke objek lainnya
        rope.LookAt(endObject.position);
    }
}
