using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class TimeScaleSlider : MonoBehaviour
{
    private Slider slider;

    private void Awake()
    {
        slider = GetComponent<Slider>();
    }

    private void OnEnable()
    {
        slider.onValueChanged.AddListener(ChangeTimeScale);
    }

    private void OnDisable()
    {
        slider.onValueChanged.RemoveListener(ChangeTimeScale);
    }

    private void ChangeTimeScale(float value)
    {
        Time.timeScale = value;
    }
}
