using UnityEngine;
using UnityEngine.InputSystem;

public class StoneMover : MonoBehaviour
{
    private Camera cam;
    private bool isHoldingMouse;

    public void OnMouseHold(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            isHoldingMouse = true;
        }
        else if (context.canceled)
        {
            isHoldingMouse = false;
        }
    }

    private void Awake()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        if (!isHoldingMouse) return;

        Vector3 mousePos = Input.mousePosition;
        // mousePos.z = Camera.main.nearClipPlane; Fix z-coordinate.
        var position = transform.position;
        position = new Vector3(cam.ScreenToWorldPoint(mousePos).x, position.y,
            position.z);
        transform.position = position;
    }
}
