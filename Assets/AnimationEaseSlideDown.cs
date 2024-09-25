using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEaseSlideDown : MonoBehaviour
{
    private void Awake()
    {
        transform.LeanMoveLocal(new Vector2(0, 395), 1).setEaseOutQuart();
    }
}
