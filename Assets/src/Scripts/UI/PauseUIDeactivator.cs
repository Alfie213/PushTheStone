using UnityEngine;

public class PauseUIDeactivator : MonoBehaviour
{
    [SerializeField] private GameObject pauseUI;

    private void OnEnable()
    {
        EnvironmentEventBus.OnPauseUIClick.Subscribe(Handle_OnPauseUIClick);
    }

    private void OnDisable()
    {
        EnvironmentEventBus.OnPauseUIClick.Unsubscribe(Handle_OnPauseUIClick);
    }

    private void Handle_OnPauseUIClick()
    {
        pauseUI.SetActive(false);
    }
}
