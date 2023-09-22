using UnityEngine;
using UnityEngine.InputSystem;

public class StoneMover : MonoBehaviour
{
    private bool isHolding;

    public void OnMouseHold(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            isHolding = true;
        }
        else if (context.canceled)
        {
            isHolding = false;
        }
    }

    private void Update()
    {
        if (isHolding)
        {
            Debug.Log("holding");
        }
        else
        {
            Debug.Log("not holding");
        }
    }
}
