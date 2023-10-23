using System;
using UnityEngine;

public class Chunk : MonoBehaviour
{
    private const float Speed = 2f;
    
    private Camera cam;

    private State currentState;
    
    private enum State
    {
        Moving,
        NotMoving
    }

    private void ChangeState(State state)
    {
        currentState = state;

        switch (currentState)
        {
            case State.Moving:
                break;
            case State.NotMoving:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
    
    private void Awake()
    {
        cam = Camera.main;
        currentState = State.NotMoving;
    }

    private void OnEnable()
    {
        EnvironmentEventBus.OnPause.Subscribe(Handle_OnPause);
        EnvironmentEventBus.OnRunning.Subscribe(Handle_OnRunning);
        EnvironmentEventBus.OnGameOver.Subscribe(Handle_OnGameOver);
    }

    private void OnDisable()
    {
        EnvironmentEventBus.OnPause.Unsubscribe(Handle_OnPause);
        EnvironmentEventBus.OnRunning.Unsubscribe(Handle_OnRunning);
        EnvironmentEventBus.OnGameOver.Unsubscribe(Handle_OnGameOver);
    }

    private void Handle_OnPause()
    {
        ChangeState(State.NotMoving);
    }

    private void Handle_OnRunning()
    {
        ChangeState(State.Moving);
    }
    
    private void Handle_OnGameOver()
    {
        ChangeState(State.NotMoving);
    }

    private void Update()
    {
        if (currentState is State.NotMoving) return;
        
        transform.position += Vector3.down * (Speed * Time.deltaTime);
        
        if (transform.position.y <= -cam.orthographicSize * 2)
        {
            EnvironmentEventBus.OnChunkDestroy.Publish();
            Destroy(gameObject);
        }
    }
}
