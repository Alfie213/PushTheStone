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
    
    private LensDistortion annihilationLensDistortion;
    
    public void TEST_StartLensDistortion()
    {
        StartCoroutine(LensDistortion());
    }

    private void Awake()
    {
        annihilationProfile.TryGetSettings(out annihilationLensDistortion);
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
        ApplyAnnihilationProfile();
    }
    
    private void ApplyRunningProfile()
    {
        postProcessVolume.profile = runningProfile;
    }
    
    private void ApplyAnnihilationProfile()
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
            annihilationLensDistortion.intensity.Override(lensDistortionCurve.Evaluate(elapsedTime));
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }
}
