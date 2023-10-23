using System;
using UnityEngine;

public class GameStateMachine : MonoBehaviour
{
    private State currentState;
    
    private enum State
    {
        Pause,
        Running,
        GameOver
    }

    private void Awake()
    {
        currentState = State.Pause;
    }

    private void OnEnable()
    {
        EnvironmentEventBus.OnMouseDownScreen.Subscribe(Handle_OnMouseDownScreen);
        EnvironmentEventBus.OnStoneCollidedObstacle.Subscribe(Handle_OnStoneCollidedObstacle);
    }

    private void OnDisable()
    {
        EnvironmentEventBus.OnMouseDownScreen.Unsubscribe(Handle_OnMouseDownScreen);
        EnvironmentEventBus.OnStoneCollidedObstacle.Unsubscribe(Handle_OnStoneCollidedObstacle);
    }

    private void Handle_OnMouseDownScreen()
    {
        ChangeState(State.Running);
    }
    
    private void Handle_OnStoneCollidedObstacle()
    {
        ChangeState(State.GameOver);
    }
    
    private void ChangeState(State state)
    {
        currentState = state;

        switch (currentState)
        {
            case State.Pause:
                Debug.Log("Pause");
                EnvironmentEventBus.OnPause.Publish();
                break;
            case State.Running:
                Debug.Log("Running");
                EnvironmentEventBus.OnRunning.Publish();
                break;
            case State.GameOver:
                Debug.Log("GameOver");
                EnvironmentEventBus.OnGameOver.Publish();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}
