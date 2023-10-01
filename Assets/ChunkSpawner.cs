using UnityEngine;

public class ChunkSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] chunks;
    [SerializeField] private int spawnedChunkCount;

    [Header("SpawnParent")] [SerializeField]
    private Transform chunkSpawnParent;

    private Camera cam;
    private Transform lastInstantiatedChunk;
    private int lastInstantiatedChunkIndex;

    private void Awake()
    {
        cam = Camera.main;
        lastInstantiatedChunk = GetComponent<Transform>();
        lastInstantiatedChunkIndex = -1;
    }

    private void OnEnable()
    {
        EnvironmentEventBus.OnChunkDestroy.Subscribe(Handle_OnChunkDestroy);
    }

    private void OnDisable()
    {
        EnvironmentEventBus.OnChunkDestroy.Unsubscribe(Handle_OnChunkDestroy);
    }

    private void Start()
    {
        for (int i = 0; i < spawnedChunkCount; i++)
            InstantiateRandomChunkNoRepetitions();
    }

    private void Handle_OnChunkDestroy()
    {
        InstantiateRandomChunkNoRepetitions();
    }
    
    private void InstantiateRandomChunkNoRepetitions()
    {
        int newChunkIndex = Random.Range(0, chunks.Length);
        while (chunks.Length > 1 && newChunkIndex == lastInstantiatedChunkIndex)
        {
            newChunkIndex = Random.Range(0, chunks.Length);
        }
        lastInstantiatedChunk = Instantiate(chunks[newChunkIndex],
            lastInstantiatedChunk.position + new Vector3(0f, cam.orthographicSize * 2, 0f), Quaternion.identity,
            chunkSpawnParent).GetComponent<Transform>();
        lastInstantiatedChunkIndex = newChunkIndex;
    }

    private void InstantiateRandomChunk()
    {
        lastInstantiatedChunk = Instantiate(chunks[Random.Range(0, chunks.Length)],
            lastInstantiatedChunk.position + new Vector3(0f, cam.orthographicSize * 2, 0f), Quaternion.identity,
            chunkSpawnParent).GetComponent<Transform>();
    }
}
