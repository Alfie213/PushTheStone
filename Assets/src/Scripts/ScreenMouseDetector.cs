using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class ScreenMouseDetector : MonoBehaviour
{
    private void OnMouseEnter()
    {
        EnvironmentEventBus.OnMouseEnterScreen.Publish();
    }

    private void OnMouseDown()
    {
        EnvironmentEventBus.OnMouseDownScreen.Publish();
    }
    
    private void OnMouseExit()
    {
        EnvironmentEventBus.OnMouseExitScreen.Publish();
    }
}
