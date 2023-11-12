using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostProcessHandler : MonoBehaviour
{
    [Header("Volume")]
    [SerializeField] private PostProcessVolume postProcessVolume;

    [Header("LensDistortion")]
    [SerializeField] private AnimationCurve lensDistortionCurve;

    [Header("PostProcessProfiles")]
    [SerializeField] private PostProcessProfile runningProfile;
    [SerializeField] private PostProcessProfile annihilationProfile;

    private AutoExposure autoExposure;
    private ColorGrading colorGrading;
    private Grain grain;
    private LensDistortion lensDistortion;
    private Vignette vignette; 
    
    public void TEST_StartLensDistortion()
    {
        StartCoroutine(LensDistortion());
    }

    private void Awake()
    {
        postProcessVolume.profile.TryGetSettings(out autoExposure);
        postProcessVolume.profile.TryGetSettings(out colorGrading);
        postProcessVolume.profile.TryGetSettings(out grain);
        postProcessVolume.profile.TryGetSettings(out lensDistortion);
        postProcessVolume.profile.TryGetSettings(out vignette);
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
        ApplyAnnihilationPostProcessEffects();
    }
    
    private void ApplyAnnihilationPostProcessEffects()
    {
        postProcessVolume.profile = annihilationProfile;
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

    private void ApplyRunningPostProcessEffects()
    {
        
    }
}
