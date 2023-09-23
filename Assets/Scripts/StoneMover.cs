using UnityEngine;
using UnityEngine.InputSystem;

public class StoneMover : MonoBehaviour
{
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

    private void Update()
    {
        if (isHoldingMouse)
        {
            Debug.Log("holding");
        }
        else
        {
            Debug.Log("not holding");
        }
    }
}
