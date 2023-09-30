using UnityEngine;

public class ChunkSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] chunks;
    [SerializeField] private int spawnedChunkCount;

    [Header("SpawnParent")] [SerializeField]
    private Transform chunkSpawnParent;

    private Camera cam;
    private Vector3 instantiatePosition;

    private void Awake()
    {
        cam = Camera.main;
        instantiatePosition = new Vector3(0f, cam.orthographicSize * 2, 0f);
        Debug.Log(instantiatePosition);
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
            InstantiateRandomChunk();

        instantiatePosition = new Vector3(0f, cam.orthographicSize * 2, 0f);
    }

    private void Handle_OnChunkDestroy()
    {
        InstantiateRandomChunk();
        DecreaseInstantiatePosition();
    }
    
    private void InstantiateRandomChunk()
    {
        Instantiate(chunks[Random.Range(0, chunks.Length)], instantiatePosition, Quaternion.identity, chunkSpawnParent);
        IncreaseInstantiatePosition();
    }

    private void IncreaseInstantiatePosition()
    {
        instantiatePosition.y += cam.orthographicSize * 2;
        Debug.Log(instantiatePosition);
    }

    private void DecreaseInstantiatePosition()
    {
        instantiatePosition.y -= cam.orthographicSize * 2;
        Debug.Log(instantiatePosition);
    }
}
