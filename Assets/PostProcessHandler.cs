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
    
    public void TEST_StartLensDistortion()
    {
        StartCoroutine(LensDistortion());
    }

    private void Awake()
    {
        postProcessVolume.profile.TryGetSettings(out lensDistortion);
    }

    private void OnEnable()
    {
        EnvironmentEventBus.OnAnnihilationRunning.Subscribe(Handle_OnAnnihilationRunning);
    }

    private void OnDisable()
    {
        EnvironmentEventBus.OnAnnihilationRunning.Unsubscribe(Handle_OnAnnihilationRunning);
    }

    private void Handle_OnAnnihilationRunning()
    {
        StartCoroutine(LensDistortion());
    }
    
    private IEnumerator LensDistortion()
    {
        float elapsedTime = 0f;
        float maxCurveTime = lensDistortionCurve.keys[lensDistortionCurve.length - 1].time;

        while (elapsedTime <= maxCurveTime)
        {
            lensDistortion.intensity.Override(lensDistortionCurve.Evaluate(elapsedTime));
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }
}
