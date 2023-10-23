using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image), typeof(Button))]
public class PauseImageButton : MonoBehaviour
{
    public void OnPauseUIClick()
    {
        EnvironmentEventBus.OnPauseUIClick.Publish();
    }

    private void OnEnable()
    {
        EnvironmentEventBus.OnPauseUIClick.Subscribe(dbg);
    }
    
    private void OnDisable()
    {
        EnvironmentEventBus.OnPauseUIClick.Unsubscribe(dbg);
    }

    private void dbg()
    {
        Debug.Log("click");
    }
}
