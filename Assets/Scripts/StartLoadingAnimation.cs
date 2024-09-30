using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartLoadingAnimation : MonoBehaviour
{
    public Slider loadingSlider;
    // Start is called before the first frame update
    void Start()
    {
        loadingSlider.value = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (loadingSlider.value <= 100)
        {
            loadingSlider.value += 0.01f;
        }
    }
}
