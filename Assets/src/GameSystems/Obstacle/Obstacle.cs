using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (!col.gameObject.TryGetComponent(out StoneProperties stoneProperties)) return;

        if (stoneProperties.IsShielded)
            EnvironmentEventBus.OnStoneCollideObstacleWithShield.Publish();
        else
            EnvironmentEventBus.OnStoneCollideObstacle.Publish();
    }
}
