using System;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private int annihilationScore;
    
    private State currentState;
    
    public enum State
    {
        Default,
        Annihilation
    }

    public void ChangeState(State state)
    {
        currentState = state;

        switch (state)
        {
            case State.Default:
                break;
            case State.Annihilation:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(state), state, null);
        }
    }

    private void OnEnable()
    {
        EnvironmentEventBus.OnDefaultRunning.Subscribe(Handle_OnDefaultRunning);
        EnvironmentEventBus.OnAnnihilationRunning.Subscribe(Handle_OnAnnihilationRunning);
    }

    private void OnDisable()
    {
        EnvironmentEventBus.OnDefaultRunning.Unsubscribe(Handle_OnDefaultRunning);
        EnvironmentEventBus.OnAnnihilationRunning.Unsubscribe(Handle_OnAnnihilationRunning);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.gameObject.TryGetComponent(out StoneProperties stoneProperties)) return;

        switch (currentState)
        {
            case State.Default:
                if (stoneProperties.IsShielded)
                    EnvironmentEventBus.OnStoneCollideObstacleWithShield.Publish();
                else
                    EnvironmentEventBus.OnStoneCollideObstacle.Publish();
                break;
            case State.Annihilation:
                EnvironmentEventBus.OnStoneCollideObstacleAnnihilation.Publish(transform.position, annihilationScore);
                Destroy(gameObject);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void Handle_OnDefaultRunning()
    {
        ChangeState(State.Default);
    }

    private void Handle_OnAnnihilationRunning()
    {
        ChangeState(State.Annihilation);
    }
}
