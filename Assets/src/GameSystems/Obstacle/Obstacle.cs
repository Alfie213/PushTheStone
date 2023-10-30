using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (!col.gameObject.TryGetComponent(out StoneMover _)) return;
        
        EnvironmentEventBus.OnStoneCollideObstacle.Publish();
    }
}
