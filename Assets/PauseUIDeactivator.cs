using UnityEngine;

public class PauseUIDeactivator : MonoBehaviour
{
    [SerializeField] private GameObject pauseUI;

    private void OnEnable()
    {
        EnvironmentEventBus.OnMouseDownScreen.Subscribe(Handle_OnMouseDownScreen);
    }

    private void OnDisable()
    {
        EnvironmentEventBus.OnMouseDownScreen.Unsubscribe(Handle_OnMouseDownScreen);
    }

    private void Handle_OnMouseDownScreen()
    {
        pauseUI.SetActive(false);
    }
}
