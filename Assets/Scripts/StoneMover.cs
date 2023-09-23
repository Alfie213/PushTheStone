using UnityEngine;
using UnityEngine.InputSystem;

public class StoneMover : MonoBehaviour
{
    private Camera cam;
    
    private bool isHoldingMouse;
    private bool isMouseInScreen;

    public void OnMoveByMouse(InputAction.CallbackContext context)
    {
        Debug.Log(context.ReadValue<float>());
        // if (context.started)
        // {
        //     isHoldingMouse = true;
        // }
        // else if (context.canceled)
        // {
        //     isHoldingMouse = false;
        // }
    }

    public void OnMoveByKeyboard(InputAction.CallbackContext context)
    {
        Debug.Log(context.ReadValue<float>());
    }

    private void Awake()
    {
        cam = Camera.main;
    }

    private void OnEnable()
    {
        EnvironmentEventBus.OnMouseEnterScreen.Subscribe(Handle_OnMouseEnterScreen);
        EnvironmentEventBus.OnMouseExitScreen.Subscribe(Handle_OnMouseExitScreen);
    }

    private void OnDisable()
    {
        EnvironmentEventBus.OnMouseEnterScreen.Unsubscribe(Handle_OnMouseEnterScreen);
        EnvironmentEventBus.OnMouseExitScreen.Unsubscribe(Handle_OnMouseExitScreen);
    }

    private void Update()
    {
        if (!isHoldingMouse || !isMouseInScreen) return;

        Vector3 mousePos = Input.mousePosition;
        // mousePos.z = Camera.main.nearClipPlane; Fix z-coordinate.
        var position = transform.position;
        position = new Vector3(cam.ScreenToWorldPoint(mousePos).x, position.y,
            position.z);
        transform.position = position;
    }

    private void Handle_OnMouseEnterScreen()
    {
        // Debug.Log("true");
        isMouseInScreen = true;
    }

    private void Handle_OnMouseExitScreen()
    {
        // Debug.Log("false");
        isMouseInScreen = false;
    }
}
