using System;
using UnityEngine;

public class GameStateMachine : MonoBehaviour
{
    #region Test
    public void TEST_EnableAnnihilation()
    {
        ChangeState(State.AnnihilationRunning);
    }
    #endregion
    

    private State currentState;
    
    private enum State
    {
        Pause,
        DefaultRunning,
        AnnihilationRunning,
        GameOver
    }

    private void Awake()
    {
        currentState = State.Pause;
    }

    private void OnEnable()
    {
        EnvironmentEventBus.OnUnpause.Subscribe(Handle_OnPauseUIClick);
        EnvironmentEventBus.OnStoneCollideObstacle.Subscribe(Handle_OnStoneCollidedObstacle);
    }

    private void OnDisable()
    {
        EnvironmentEventBus.OnUnpause.Unsubscribe(Handle_OnPauseUIClick);
        EnvironmentEventBus.OnStoneCollideObstacle.Unsubscribe(Handle_OnStoneCollidedObstacle);
    }

    private void Handle_OnPauseUIClick()
    {
        EnvironmentEventBus.OnGameStart.Publish();
        ChangeState(State.DefaultRunning);
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
                // Debug.Log("Pause");
                EnvironmentEventBus.OnPause.Publish();
                break;
            case State.DefaultRunning:
                // Debug.Log("DefaultRunning");
                EnvironmentEventBus.OnDefaultRunning.Publish();
                break;
            case State.AnnihilationRunning:
                // Debug.Log("AnnihilationRunning");
                EnvironmentEventBus.OnAnnihilationRunning.Publish();
                break;
            case State.GameOver:
                // Debug.Log("GameOver");
                EnvironmentEventBus.OnGameOver.Publish();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public void Pause()
    {
        ChangeState(State.Pause);
    }
}
