using UnityEngine;

public class GameOverUIActivator : MonoBehaviour
{
    [SerializeField] private GameObject gameOverUI;

    private void OnEnable()
    {
        EnvironmentEventBus.OnGameOver.Subscribe(Handle_OnGameOver);
    }

    private void OnDisable()
    {
        EnvironmentEventBus.OnGameOver.Unsubscribe(Handle_OnGameOver);
    }

    private void Handle_OnGameOver()
    {
        gameOverUI.SetActive(true);
    }
}
