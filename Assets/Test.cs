using System.Linq;
using UnityEngine;

public class Test : MonoBehaviour
{
    private void Awake()
    {
        Transform[] gos = FindObjectsOfType<Transform>()
            .Where(go => go.gameObject.layer == LayerMask.NameToLayer("LensDistortion")).ToArray();
        foreach (var transform in gos)
        {
            Debug.Log(transform.name);
        }
    }
}
