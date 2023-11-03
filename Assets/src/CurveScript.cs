using UnityEngine;

public class CurveScript : MonoBehaviour
{
    [SerializeField] private AnimationCurve animationCurve;

    private float currentTime;
    private float maxCurveTime;

    private void Awake()
    {
        currentTime = 0f;
        maxCurveTime = animationCurve.keys[animationCurve.length - 1].time;
    }

    private void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime >= maxCurveTime)
            currentTime = 0f;
        
        // Debug.Log(animationCurve.keys[animationCurve.length - 1].time);
        // Debug.Log(animationCurve.keys[animationCurve.length - 1].value);
        Debug.Log(animationCurve.Evaluate(currentTime));
        Debug.Log("-----------------------------");
    }
}
