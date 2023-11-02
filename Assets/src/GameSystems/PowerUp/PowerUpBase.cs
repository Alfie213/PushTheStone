using UnityEngine;

public abstract class PowerUpBase : MonoBehaviour
{
    protected abstract void PowerUp();
    
    protected void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.gameObject.TryGetComponent(out StoneMover _)) return;
        
        PowerUp();
        EnvironmentEventBus.OnPowerUpPickUp.Publish();
        Destroy(gameObject);
    }
}
