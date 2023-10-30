using UnityEngine;

public class ManMover : MonoBehaviour
{
    [SerializeField] private Transform stone;

    private const float XOffset = 0.05f;

    private void Update()
    {
        transform.position =
            new Vector3(stone.transform.position.x + XOffset, transform.position.y, transform.position.z);
    }
}
