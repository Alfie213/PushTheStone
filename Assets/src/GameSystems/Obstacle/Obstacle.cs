using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.TryGetComponent(out StoneMover _)) return;
        
        Debug.Log("GameOver");
    }
}
