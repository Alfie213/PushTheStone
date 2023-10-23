using System;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int CurrentScore => Convert.ToInt32(currentScore);
    
    [SerializeField] private float scoreMultiplier;

    private float currentScore;
    private State currentState;

    private enum State
    {
        Counting,
        NotCounting
    }

    private void ChangeState(State state)
    {
        currentState = state;

        switch (currentState)
        {
            case State.Counting:
                break;
            case State.NotCounting:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void Awake()
    {
        currentScore = 0f;
        currentState = State.NotCounting;
    }

    private void OnEnable()
    {
        EnvironmentEventBus.OnGameOver.Subscribe(Handle_OnGameOver);
    }

    private void OnDisable()
    {
        EnvironmentEventBus.OnGameOver.Unsubscribe(Handle_OnGameOver);
    }

    private void Update()
    {
        if (currentState == State.Counting)
        {
            currentScore += scoreMultiplier * Time.deltaTime;
        }
    }

    private void Handle_OnGameOver()
    {
        ChangeState(State.NotCounting);
    }
}
