using UnityEngine;

public class PowerUpModelActivator : MonoBehaviour
{
    [SerializeField] private GameObject shield;
    
    private void OnEnable()
    {
        EnvironmentEventBus.OnShieldPickUp.Subscribe(Handle_OnShieldPickUp);
    }

    private void OnDisable()
    {
        EnvironmentEventBus.OnShieldPickUp.Unsubscribe(Handle_OnShieldPickUp);
    }

    private void Handle_OnShieldPickUp()
    {
        shield.SetActive(true);
    }
}
