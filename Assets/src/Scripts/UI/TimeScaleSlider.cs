using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class TimeScaleSlider : MonoBehaviour
{
    private const int DefaultTimeScale = 1;
    
    private Slider slider;

    private void Awake()
    {
        slider = GetComponent<Slider>();
    }

    private void OnEnable()
    {
        SetDefaultTimeScale();
        slider.onValueChanged.AddListener(ChangeTimeScale);
    }

    private void OnDisable()
    {
        SetDefaultTimeScale();
        slider.onValueChanged.RemoveListener(ChangeTimeScale);
    }

    private void ChangeTimeScale(float value)
    {
        Time.timeScale = value;
    }

    private void SetDefaultTimeScale()
    {
        Time.timeScale = DefaultTimeScale;
    }
}
