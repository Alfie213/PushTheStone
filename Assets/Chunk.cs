using UnityEngine;

public class Chunk : MonoBehaviour
{
    private const float Speed = 2f;
    
    private Camera cam;

    private void Awake()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        transform.position += Vector3.down * (Speed * Time.deltaTime);
        
        if (transform.position.y <= -cam.orthographicSize * 2)
        {
            EnvironmentEventBus.OnChunkDestroy.Publish();
            Destroy(gameObject);
        }
    }
}
