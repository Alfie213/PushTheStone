using UnityEngine;

public class PauseUISwitcher : MonoBehaviour
{
    [SerializeField] private GameObject pauseUI;

    private void OnEnable()
    {
        EnvironmentEventBus.OnPause.Subscribe(Handle_OnPause);
        EnvironmentEventBus.OnUnpause.Subscribe(Handle_OnPauseUIClick);
    }

    private void OnDisable()
    {
        EnvironmentEventBus.OnPause.Unsubscribe(Handle_OnPause);
        EnvironmentEventBus.OnUnpause.Unsubscribe(Handle_OnPauseUIClick);
    }

    private void Handle_OnPause()
    {
        SetActivePauseUI(true);
    }
    
    private void Handle_OnPauseUIClick()
    {
        SetActivePauseUI(false);
    }

    private void SetActivePauseUI(bool value)
    {
        pauseUI.SetActive(value);
    }
}
