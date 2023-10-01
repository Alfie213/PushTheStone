using System;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int Score => Convert.ToInt32(score);
    
    [SerializeField] private float scoreMultiplier;

    private float score;
    private State state;

    private enum State
    {
        Counting,
        NotCounting
    }

    private void Awake()
    {
        score = 0f;
        state = State.Counting;
    }

    private void Update()
    {
        if (state == State.Counting)
        {
            score += scoreMultiplier * Time.deltaTime;
        }
    }
}
