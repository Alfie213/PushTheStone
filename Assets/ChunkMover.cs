using System;
using System.Collections.Generic;
using UnityEngine;

public class ChunkMover : MonoBehaviour
{
    [SerializeField] private float speed;
    
    private List<GameObject> chunks;
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
        chunks = new List<GameObject>();
        cam = Camera.main;
        currentState = State.NotMoving;
    }

    private void OnEnable()
    {
        EnvironmentEventBus.OnPause.Subscribe(Handle_OnPause);
        EnvironmentEventBus.OnRunning.Subscribe(Handle_OnRunning);
        EnvironmentEventBus.OnGameOver.Subscribe(Handle_OnGameOver);
        EnvironmentEventBus.OnChunkInstantiate.Subscribe(Handle_OnChunkInstantiate);
    }

    private void OnDisable()
    {
        EnvironmentEventBus.OnPause.Unsubscribe(Handle_OnPause);
        EnvironmentEventBus.OnRunning.Unsubscribe(Handle_OnRunning);
        EnvironmentEventBus.OnGameOver.Unsubscribe(Handle_OnGameOver);
        EnvironmentEventBus.OnChunkInstantiate.Unsubscribe(Handle_OnChunkInstantiate);
    }

    private void Update()
    {
        if (currentState is State.NotMoving) return;
        
        foreach (GameObject chunk in chunks)
        {
            chunk.transform.position += Vector3.down * (speed * Time.deltaTime);
            
            if (chunk.transform.position.y <= -cam.orthographicSize * 2)
            {
                chunks.Remove(chunk);
                Destroy(chunk);
                EnvironmentEventBus.OnChunkDestroy.Publish();
            }
        }
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
    
    private void Handle_OnChunkInstantiate(GameObject chunk)
    {
        chunks.Add(chunk);
    }
}
