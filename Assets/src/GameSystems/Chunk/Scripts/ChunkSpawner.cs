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
    
    private Transform lastInstantiatedChunk;
    private int lastInstantiatedChunkIndex;
    private int currentCountOfChunks;

    private State currentState;

    private enum State
    {
        Spawning,
        NotSpawning
    }

    private void ChangeState(State state)
    {
        currentState = state;

        switch (currentState)
        {
            case State.Spawning:
                InstantiateRandomChunkNoRepetitions(maxSimultaneousChunks - currentCountOfChunks);
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
        lastInstantiatedChunk = GetComponent<Transform>();
        lastInstantiatedChunkIndex = -1;
    }

    private void OnEnable()
    {
        EnvironmentEventBus.OnPause.Subscribe(Handle_OnPause);
        EnvironmentEventBus.OnRunning.Subscribe(Handle_OnRunning);
        EnvironmentEventBus.OnGameOver.Subscribe(Handle_OnGameOver);
        EnvironmentEventBus.OnChunkDestroy.Subscribe(Handle_OnChunkDestroy);
    }

    private void OnDisable()
    {
        EnvironmentEventBus.OnPause.Unsubscribe(Handle_OnPause);
        EnvironmentEventBus.OnRunning.Unsubscribe(Handle_OnRunning);
        EnvironmentEventBus.OnGameOver.Unsubscribe(Handle_OnGameOver);
        EnvironmentEventBus.OnChunkDestroy.Unsubscribe(Handle_OnChunkDestroy);
    }

    private void Handle_OnPause()
    {
        ChangeState(State.NotSpawning);
    }

    private void Handle_OnRunning()
    {
        ChangeState(State.Spawning);
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
    
    private void InstantiateRandomChunkNoRepetitions()
    {
        int newChunkIndex = Random.Range(0, chunksPrefabs.Length);
        while (chunksPrefabs.Length > 1 && newChunkIndex == lastInstantiatedChunkIndex)
        {
            newChunkIndex = Random.Range(0, chunksPrefabs.Length);
        }
        lastInstantiatedChunk = Instantiate(chunksPrefabs[newChunkIndex],
            lastInstantiatedChunk.position + new Vector3(0f, cam.orthographicSize * 2, 0f), Quaternion.identity,
            chunkSpawnParent).GetComponent<Transform>();
        lastInstantiatedChunkIndex = newChunkIndex;
        currentCountOfChunks++;
    }

    private void InstantiateRandomChunkNoRepetitions(int count)
    {
        for (int _ = 0; _ < count; _++)
            InstantiateRandomChunkNoRepetitions();
    }

    private void InstantiateRandomChunk()
    {
        lastInstantiatedChunk = Instantiate(chunksPrefabs[Random.Range(0, chunksPrefabs.Length)],
            lastInstantiatedChunk.position + new Vector3(0f, cam.orthographicSize * 2, 0f), Quaternion.identity,
            chunkSpawnParent).GetComponent<Transform>();
        currentCountOfChunks++;
    }

    private void InstantiateRandomChunk(int count)
    {
        for (int _ = 0; _ < count; _++)
            InstantiateRandomChunk();
    }
}
