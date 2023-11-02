using UnityEngine;

public abstract class PowerUpBase : MonoBehaviour
{
    protected abstract void PowerUp();
    
    protected virtual void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.gameObject.TryGetComponent(out StoneProperties _)) return;
        
        PowerUp();
        EnvironmentEventBus.OnPowerUpPickUp.Publish();
        Destroy(gameObject);
    }
}
