using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class ChunkSpawner : MonoBehaviour
{
    [SerializeField] private int maxSimultaneousChunks;
    
    [Header("ChunksPrefabs")]
    [SerializeField] private GameObject[] runningModeChunksPrefabs;
    [SerializeField] private GameObject[] annihilationModeChunksPrefabs;

    [Header("SpawnParent")] [SerializeField]
    private Transform chunkSpawnParent;

    private Camera cam;
    
    private GameObject lastInstantiatedChunk;
    private Transform lastInstantiatedChunkTransform;
    private int lastInstantiatedChunkIndex;
    private int currentCountOfChunks;

    private int missingChunksCount => maxSimultaneousChunks - currentCountOfChunks;

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
                InstantiateRandomChunksNoRepetitions(missingChunksCount);
                break;
            case State.AnnihilationSpawning:
                InstantiateRandomChunksNoRepetitions(missingChunksCount);
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
        if (currentState is State.DefaultSpawning)
        {
            InstantiateRandomRunningChunkNoRepetitions();
        }
        else if (currentState is State.AnnihilationSpawning)
        {
            InstantiateRandomAnnihilationChunkNoRepetitions();
        }
    }

    private void InstantiateRandomRunningChunkNoRepetitions()
    {
        int newChunkIndex = Random.Range(0, runningModeChunksPrefabs.Length);
        while (runningModeChunksPrefabs.Length > 1 && newChunkIndex == lastInstantiatedChunkIndex) // Avoid repetitions if Lenght > 1.
        {
            newChunkIndex = Random.Range(0, runningModeChunksPrefabs.Length);
        }
        
        lastInstantiatedChunk = Instantiate(runningModeChunksPrefabs[newChunkIndex],
            lastInstantiatedChunkTransform.position + new Vector3(0f, cam.orthographicSize * 2, 0f),
            Quaternion.identity,
            chunkSpawnParent);
        lastInstantiatedChunkTransform = lastInstantiatedChunk.transform;
        lastInstantiatedChunkIndex = newChunkIndex;
        currentCountOfChunks++;
        
        Obstacle[] obstacleComponents = lastInstantiatedChunk.GetComponentsInChildren<Obstacle>();
        foreach (Obstacle obstacleComponent in obstacleComponents)
        {
            obstacleComponent.ChangeState(Obstacle.State.Default);
        }

        EnvironmentEventBus.OnChunkInstantiate.Publish(lastInstantiatedChunk);
    }
    
    private void InstantiateRandomAnnihilationChunkNoRepetitions()
    {
        int newChunkIndex = Random.Range(0, annihilationModeChunksPrefabs.Length);
        while (annihilationModeChunksPrefabs.Length > 1 && newChunkIndex == lastInstantiatedChunkIndex) // Avoid repetitions if Lenght > 1.
        {
            newChunkIndex = Random.Range(0, annihilationModeChunksPrefabs.Length);
        }
        
        lastInstantiatedChunk = Instantiate(annihilationModeChunksPrefabs[newChunkIndex],
            lastInstantiatedChunkTransform.position + new Vector3(0f, cam.orthographicSize * 2, 0f),
            Quaternion.identity,
            chunkSpawnParent);
        lastInstantiatedChunkTransform = lastInstantiatedChunk.transform;
        lastInstantiatedChunkIndex = newChunkIndex;
        currentCountOfChunks++;
        
        Obstacle[] obstacleComponents = lastInstantiatedChunk.GetComponentsInChildren<Obstacle>();
        foreach (Obstacle obstacleComponent in obstacleComponents)
        {
            obstacleComponent.ChangeState(Obstacle.State.Annihilation);
        }
        
        EnvironmentEventBus.OnChunkInstantiate.Publish(lastInstantiatedChunk);
    }

    private void InstantiateRandomChunksNoRepetitions(int count)
    {
        for (int _ = 0; _ < count; _++)
            InstantiateRandomChunkNoRepetitions();
    }

    // private void InstantiateRandomChunk()
    // {
    //     lastInstantiatedChunk = Instantiate(runningModeChunksPrefabs[Random.Range(0, runningModeChunksPrefabs.Length)],
    //         lastInstantiatedChunkTransform.position + new Vector3(0f, cam.orthographicSize * 2, 0f),
    //         Quaternion.identity,
    //         chunkSpawnParent);
    //     lastInstantiatedChunkTransform = lastInstantiatedChunk.transform;
    //     currentCountOfChunks++;
    //     EnvironmentEventBus.OnChunkInstantiate.Publish(lastInstantiatedChunk);
    // }
    //
    // private void InstantiateRandomChunk(int count)
    // {
    //     for (int _ = 0; _ < count; _++)
    //         InstantiateRandomChunk();
    // }
}
