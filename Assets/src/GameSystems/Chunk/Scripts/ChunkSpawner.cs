using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class ChunkSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] chunksPrefabs;
    [SerializeField] private int maxSimultaneousChunks;

    [Header("SpawnParent")] [SerializeField]
    private Transform chunkSpawnParent;

    private Camera cam;
    
    private GameObject lastInstantiatedChunk;
    private Transform lastInstantiatedChunkTransform;
    private int lastInstantiatedChunkIndex;
    private int currentCountOfChunks;

    private State currentState;

    private enum State
    {
        DefaultSpawning,
        AnnihilationSpawning,
        NotSpawning
    }

    private void ChangeState(State state)
    {
        currentState = state;

        switch (currentState)
        {
            case State.DefaultSpawning:
                int missingCountDefaultChunks = maxSimultaneousChunks - currentCountOfChunks;
                InstantiateRandomChunksNoRepetitions(missingCountDefaultChunks);
                break;
            case State.AnnihilationSpawning:
                int missingCountAnnihilationChunks = maxSimultaneousChunks - currentCountOfChunks;
                InstantiateRandomChunksNoRepetitions(missingCountAnnihilationChunks);
                break;
            case State.NotSpawning:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
    
    private void Awake()
    {
        cam = Camera.main;
        lastInstantiatedChunkTransform = GetComponent<Transform>();
        lastInstantiatedChunkIndex = -1;
    }

    private void OnEnable()
    {
        EnvironmentEventBus.OnPause.Subscribe(Handle_OnPause);
        EnvironmentEventBus.OnGameStart.Subscribe(Handle_OnRunning);
        EnvironmentEventBus.OnGameOver.Subscribe(Handle_OnGameOver);
        EnvironmentEventBus.OnChunkDestroy.Subscribe(Handle_OnChunkDestroy);
        EnvironmentEventBus.OnAnnihilationRunning.Subscribe(Handle_OnAnnihilationRunning);
    }

    private void OnDisable()
    {
        EnvironmentEventBus.OnPause.Unsubscribe(Handle_OnPause);
        EnvironmentEventBus.OnGameStart.Unsubscribe(Handle_OnRunning);
        EnvironmentEventBus.OnGameOver.Unsubscribe(Handle_OnGameOver);
        EnvironmentEventBus.OnChunkDestroy.Unsubscribe(Handle_OnChunkDestroy);
        EnvironmentEventBus.OnAnnihilationRunning.Unsubscribe(Handle_OnAnnihilationRunning);
    }

    private void Handle_OnPause()
    {
        ChangeState(State.NotSpawning);
    }

    private void Handle_OnRunning()
    {
        ChangeState(State.DefaultSpawning);
    }
    
    private void Handle_OnGameOver()
    {
        ChangeState(State.NotSpawning);
    }
    
    private void Handle_OnChunkDestroy()
    {
        currentCountOfChunks--;
        InstantiateRandomChunkNoRepetitions();
    }

    private void Handle_OnAnnihilationRunning()
    {
        ChangeState(State.AnnihilationSpawning);
    }
    
    private void InstantiateRandomChunkNoRepetitions()
    {
        int newChunkIndex = Random.Range(0, chunksPrefabs.Length);
        while (chunksPrefabs.Length > 1 && newChunkIndex == lastInstantiatedChunkIndex)
        {
            newChunkIndex = Random.Range(0, chunksPrefabs.Length);
        }

        lastInstantiatedChunk = Instantiate(chunksPrefabs[newChunkIndex],
            lastInstantiatedChunkTransform.position + new Vector3(0f, cam.orthographicSize * 2, 0f),
            Quaternion.identity,
            chunkSpawnParent);
        lastInstantiatedChunkTransform = lastInstantiatedChunk.transform;
        lastInstantiatedChunkIndex = newChunkIndex;
        currentCountOfChunks++;

        if (currentState is State.AnnihilationSpawning)
        {
            Obstacle[] obstacleComponents = lastInstantiatedChunk.GetComponentsInChildren<Obstacle>();
            foreach (Obstacle obstacleComponent in obstacleComponents)
            {
                obstacleComponent.ChangeState(Obstacle.State.Annihilation);
            }
        }
        
        EnvironmentEventBus.OnChunkInstantiate.Publish(lastInstantiatedChunk);
    }

    private void InstantiateRandomChunksNoRepetitions(int count)
    {
        for (int _ = 0; _ < count; _++)
            InstantiateRandomChunkNoRepetitions();
    }

    private void InstantiateRandomChunk()
    {
        lastInstantiatedChunk = Instantiate(chunksPrefabs[Random.Range(0, chunksPrefabs.Length)],
            lastInstantiatedChunkTransform.position + new Vector3(0f, cam.orthographicSize * 2, 0f),
            Quaternion.identity,
            chunkSpawnParent);
        lastInstantiatedChunkTransform = lastInstantiatedChunk.transform;
        currentCountOfChunks++;
        EnvironmentEventBus.OnChunkInstantiate.Publish(lastInstantiatedChunk);
    }

    private void InstantiateRandomChunk(int count)
    {
        for (int _ = 0; _ < count; _++)
            InstantiateRandomChunk();
    }
}
