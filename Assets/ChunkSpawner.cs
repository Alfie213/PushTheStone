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
    }

    private void Start()
    {
        for (int i = 0; i < spawnedChunkCount; i++)
            InstantiateRandomChunk();
    }

    private void InstantiateRandomChunk()
    {
        Instantiate(chunks[Random.Range(0, chunks.Length)], instantiatePosition, Quaternion.identity, chunkSpawnParent);
        IncreaseInstantiatePosition();
    }

    private void IncreaseInstantiatePosition()
    {
        instantiatePosition.y += cam.orthographicSize * 2;
    }

    private void DecreaseInstantiatePosition()
    {
        instantiatePosition.y -= cam.orthographicSize * 2;
    }
}
