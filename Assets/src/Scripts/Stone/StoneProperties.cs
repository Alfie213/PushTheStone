using UnityEngine;

public class StoneProperties : MonoBehaviour
{
    public bool IsShielded { get; private set; }

    [SerializeField] private GameObject shield;

    private void OnEnable()
    {
        EnvironmentEventBus.OnShieldPickUp.Subscribe(Handle_OnShieldPickUp);
        EnvironmentEventBus.OnStoneCollideObstacleWithShield.Subscribe(Handle_OnStoneCollideObstacleWithShield);
    }

    private void OnDisable()
    {
        EnvironmentEventBus.OnShieldPickUp.Unsubscribe(Handle_OnShieldPickUp);
        EnvironmentEventBus.OnStoneCollideObstacleWithShield.Unsubscribe(Handle_OnStoneCollideObstacleWithShield);
    }

    private void Handle_OnShieldPickUp()
    {
        IsShielded = true;
        shield.SetActive(true);
    }

    private void Handle_OnStoneCollideObstacleWithShield()
    {
        shield.SetActive(false);
        IsShielded = false;
    }
}
