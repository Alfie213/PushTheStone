using System;
using UnityEngine;
using UnityEngine.UI;

public class GameStateMachine : MonoBehaviour
{
    [SerializeField] private Button DEBUG_EnableAnnihilationButton;

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

        if (DEBUG_EnableAnnihilationButton.IsActive())
        {
            DEBUG_EnableAnnihilationButton.onClick.AddListener(TEST_EnableAnnihilation);
        }
    }

    private void OnDisable()
    {
        EnvironmentEventBus.OnUnpause.Unsubscribe(Handle_OnPauseUIClick);
        EnvironmentEventBus.OnStoneCollideObstacle.Unsubscribe(Handle_OnStoneCollidedObstacle);
        
        if (DEBUG_EnableAnnihilationButton.IsActive())
        {
            DEBUG_EnableAnnihilationButton.onClick.RemoveListener(TEST_EnableAnnihilation);
        }
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
                break;
            case State.AnnihilationRunning:
                // Debug.Log("AnnihilationRunning");
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

    private void TEST_EnableAnnihilation()
    {
        ChangeState(State.AnnihilationRunning);
    }
}
