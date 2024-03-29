using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class StoneMover : MonoBehaviour
{
    [SerializeField] private float speedByKeyboard;
    
    private Camera cam;
    private Rigidbody2D rb;
    
    private bool isHoldingMouse;
    private bool isMouseInScreen;
    private bool isHoldingKeyboard;

    private float keyboardDelta;

    private State currentState;

    private enum State
    {
        ControlEnabled,
        ControlDisabled
    }

    private void ChangeState(State state)
    {
        currentState = state;

        switch (currentState)
        {
            case State.ControlEnabled:
                break;
            case State.ControlDisabled:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public void OnMoveByMouse(InputAction.CallbackContext context)
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

    public void OnMoveByKeyboard(InputAction.CallbackContext context)
    {
        keyboardDelta = context.ReadValue<float>();
        if (context.started)
        {
            isHoldingKeyboard = true;
        }
        else if (context.canceled)
        {
            isHoldingKeyboard = false;
        }
    }

    private void Awake()
    {
        cam = Camera.main;
        rb = GetComponent<Rigidbody2D>();
        
        currentState = State.ControlDisabled;
    }

    private void OnEnable()
    {
        EnvironmentEventBus.OnMouseEnterScreen.Subscribe(Handle_OnMouseEnterScreen);
        EnvironmentEventBus.OnMouseExitScreen.Subscribe(Handle_OnMouseExitScreen);
        
        EnvironmentEventBus.OnPause.Subscribe(Handle_OnPause);
        EnvironmentEventBus.OnRunning.Subscribe(Handle_OnRunning);
    }

    private void OnDisable()
    {
        EnvironmentEventBus.OnMouseEnterScreen.Unsubscribe(Handle_OnMouseEnterScreen);
        EnvironmentEventBus.OnMouseExitScreen.Unsubscribe(Handle_OnMouseExitScreen);
        
        EnvironmentEventBus.OnPause.Subscribe(Handle_OnPause);
        EnvironmentEventBus.OnRunning.Subscribe(Handle_OnRunning);
    }

    private void Update()
    {
        if (currentState == State.ControlDisabled) return;
        
        if (!isHoldingMouse && !isHoldingKeyboard) return;
        
        if (isHoldingMouse && isMouseInScreen)
        {
            // Debug.Log("moving by mouse");
            Vector3 mousePos = Input.mousePosition;
            // mousePos.z = Camera.main.nearClipPlane; Fix z-coordinate.
            var position = transform.position;
            position = new Vector3(cam.ScreenToWorldPoint(mousePos).x, position.y,
                position.z);
            transform.position = position;
        }
        else if (isHoldingKeyboard)
        {
            // Debug.Log("moving by keyboard");
            Vector2 force = new Vector2(speedByKeyboard * keyboardDelta, 0);
            rb.AddForce(force);
            // Debug.Log("force added");
        }
        
        EnvironmentEventBus.OnStoneMove.Publish();
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

    private void Handle_OnPause()
    {
        ChangeState(State.ControlDisabled);
    }

    private void Handle_OnRunning()
    {
        ChangeState(State.ControlEnabled);
    }
}
