using UnityEngine;
using UnityEngine.UI;

public class StartLoadingAnimation : MonoBehaviour
{
    [SerializeField, Range(0f, 3f)] private float loadingTime = 0.1f;
    [SerializeField] private Slider loadingSlider;

    void Start()
    {
        if (loadingSlider)
        {
            loadingSlider.value = 0f;
        }
        else
        {
            Debug.LogWarning("Loading Slider is Null");
            return;
        }
    }

    void Update()
    {
        if (loadingSlider.value < 1f)
        {
            loadingSlider.value += Time.deltaTime / loadingTime;
        }
    }
}
