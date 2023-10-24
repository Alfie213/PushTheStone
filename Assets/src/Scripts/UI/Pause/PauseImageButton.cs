using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image), typeof(Button))]
public class PauseImageButton : MonoBehaviour
{
    public void OnPauseUIClick()
    {
        EnvironmentEventBus.OnPauseUIClick.Publish();
    }
}
