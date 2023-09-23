using UnityEngine;

public class MouseDetector : MonoBehaviour
{
    private void OnMouseEnter()
    {
        EnvironmentEventBus.OnMouseEnterScreen.Publish();
    }

    private void OnMouseExit()
    {
        EnvironmentEventBus.OnMouseExitScreen.Publish();
    }
}
