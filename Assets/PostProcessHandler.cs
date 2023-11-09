using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostProcessHandler : MonoBehaviour
{
    [Header("Volume")]
    [SerializeField] private PostProcessVolume postProcessVolume;

    [Header("LensDistortion")]
    [SerializeField] private AnimationCurve lensDistortionCurve;
    
    private LensDistortion lensDistortion; 

    private void Awake()
    {
        postProcessVolume.profile.TryGetSettings(out lensDistortion);
        // lensDistortion.intensity.Override(20f);
    }

    public void StartDistortion()
    {
        StartCoroutine(Distortion());
    }

    private IEnumerator Distortion()
    {
        float passTime = 0f;
        float maxCurveTime = lensDistortionCurve.keys[lensDistortionCurve.length - 1].time;

        while (passTime <= maxCurveTime)
        {
            float value = lensDistortionCurve.Evaluate(passTime);
            lensDistortion.intensity.Override(value);
            passTime += Time.deltaTime;
            Debug.Log(passTime);
            Debug.Log("changed");
            yield return new WaitForEndOfFrame();
        }
    }
}
