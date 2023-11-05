using System;
using System.Collections.Generic;
using UnityEngine;

public class ChunkMover : MonoBehaviour
{
    [SerializeField, Min(1f)] private float defaultChunkSpeed;
    [SerializeField, Min(2f)] private float annihilationChunkSpeed;
    
    private List<GameObject> chunks;
    private Camera cam;
    
    private State currentState;
    private float currentChunkSpeed;
    
    private enum State
    {
        DefaultMoving,
        AnnihilationMoving,
        NotMoving
    }

    private void ChangeState(State state)
    {
        currentState = state;

        switch (currentState)
        {
            case State.DefaultMoving:
                currentChunkSpeed = defaultChunkSpeed;
                break;
            case State.NotMoving:
                currentChunkSpeed = 0f;
                break;
            case State.AnnihilationMoving:
                currentChunkSpeed = annihilationChunkSpeed;
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
        EnvironmentEventBus.OnGameStart.Subscribe(Handle_OnRunning);
        EnvironmentEventBus.OnGameOver.Subscribe(Handle_OnGameOver);
        EnvironmentEventBus.OnChunkInstantiate.Subscribe(Handle_OnChunkInstantiate);
        EnvironmentEventBus.OnDefaultRunning.Subscribe(Handle_OnDefaultRunning);
        EnvironmentEventBus.OnAnnihilationRunning.Subscribe(Handle_OnAnnihilationRunning);
    }

    private void OnDisable()
    {
        EnvironmentEventBus.OnPause.Unsubscribe(Handle_OnPause);
        EnvironmentEventBus.OnGameStart.Unsubscribe(Handle_OnRunning);
        EnvironmentEventBus.OnGameOver.Unsubscribe(Handle_OnGameOver);
        EnvironmentEventBus.OnChunkInstantiate.Unsubscribe(Handle_OnChunkInstantiate);
        EnvironmentEventBus.OnDefaultRunning.Unsubscribe(Handle_OnDefaultRunning);
        EnvironmentEventBus.OnAnnihilationRunning.Unsubscribe(Handle_OnAnnihilationRunning);
    }

    private void Update()
    {
        if (currentState is State.NotMoving) return;

        List<GameObject> chunksToRemove = new List<GameObject>();

        foreach (GameObject chunk in chunks)
        {
            chunk.transform.position += Vector3.down * (currentChunkSpeed * Time.deltaTime);
            
            if (chunk.transform.position.y <= -cam.orthographicSize * 2)
            {
                chunksToRemove.Add(chunk);
            }
        }

        foreach (GameObject chunk in chunksToRemove)
        {
            chunks.Remove(chunk);
            Destroy(chunk);
            EnvironmentEventBus.OnChunkDestroy.Publish();
        }
    }

    private void Handle_OnPause()
    {
        ChangeState(State.NotMoving);
    }

    private void Handle_OnRunning()
    {
        ChangeState(State.DefaultMoving);
    }
    
    private void Handle_OnGameOver()
    {
        ChangeState(State.NotMoving);
    }
    
    private void Handle_OnChunkInstantiate(GameObject chunk)
    {
        chunks.Add(chunk);
    }

    private void Handle_OnDefaultRunning()
    {
        ChangeState(State.DefaultMoving);
    }

    private void Handle_OnAnnihilationRunning()
    {
        ChangeState(State.AnnihilationMoving);
    }
}
