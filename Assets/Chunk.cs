using UnityEngine;

public class Chunk : MonoBehaviour
{
    private Camera cam;

    private void Awake()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        if (transform.position.y <= -cam.orthographicSize * 2)
        {
            Destroy(this);
        }
    }
}
