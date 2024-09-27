using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEaseSlideDown : MonoBehaviour
{
    public Vector2 targetToMove;
    private void Awake()
    {
        StartAnimation();
    }

    void StartAnimation() {
        transform.LeanMoveLocal(targetToMove, 1).setEaseOutQuart();
    }
}
