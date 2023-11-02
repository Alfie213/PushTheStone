using UnityEngine;

public class Wall : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (!col.gameObject.TryGetComponent(out StoneProperties _)) return;
        
        EnvironmentEventBus.OnStoneCollideWall.Publish();
    }
}
