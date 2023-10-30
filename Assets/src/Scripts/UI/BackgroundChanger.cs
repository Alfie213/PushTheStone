using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class BackgroundChanger : MonoBehaviour
{
    [SerializeField] private Sprite[] backgrounds;
    [SerializeField] private SpriteRenderer background;
    [SerializeField] private Mode mode;

    private enum Mode
    {
        RandomEveryGame
    }

    private void Start()
    {
        switch (mode)
        {
            case Mode.RandomEveryGame:
                background.sprite = backgrounds[Random.Range(0, backgrounds.Length)];
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}
