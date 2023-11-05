using TMPro;
using UnityEngine;

public class CollideObstacleAnnihilationHandler : MonoBehaviour
{
    [SerializeField] private RectTransform scoreParent;
    
    private void OnEnable()
    {
        EnvironmentEventBus.OnStoneCollideObstacleAnnihilation.Subscribe(Handle_OnStoneCollideObstacleAnnihilation);
    }

    private void OnDisable()
    {
        EnvironmentEventBus.OnStoneCollideObstacleAnnihilation.Unsubscribe(Handle_OnStoneCollideObstacleAnnihilation);
    }

    private void Handle_OnStoneCollideObstacleAnnihilation(Vector3 obstaclePosition, int obstacleAnnihilationScore)
    {
        FlyingScore(obstaclePosition, obstacleAnnihilationScore);
    }

    private void FlyingScore(Vector3 obstaclePosition, int obstacleAnnihilationScore)
    {
        // Debug.Log("start");
        // Vector3 scorePosition = Camera.main.WorldToScreenPoint(obstaclePosition);
        //
        // GameObject scoreGameObject = new GameObject("Score", typeof(RectTransform), typeof(TextMeshProUGUI));
        // scoreGameObject.GetComponent<RectTransform>().position = scorePosition;
        // scoreGameObject.GetComponent<TextMeshProUGUI>().text = obstacleAnnihilationScore.ToString();
        // scoreGameObject.transform.SetParent(scoreParent);
        
        EnvironmentEventBus.OnFlyingScoreReachCurrentScore.Publish(obstacleAnnihilationScore);
    }
}
