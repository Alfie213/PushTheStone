using System;
using UnityEngine;
using UnityEngine.UI;

public class AnnihilationScoreManager : MonoBehaviour
{
    [SerializeField] private Slider annihilationScaleSlider;

    [SerializeField] private float defaultAnnihilationScoreIncreasingMultiplier;
    [SerializeField] private float defaultAnnihilationScoreDecreasingMultiplier;

    private float currentAnnihilationScore;
    private State currentState;

    private enum State
    {
        NotCounting,
        Increasing,
        Decreasing
    }

    private void ChangeState(State state)
    {
        currentState = state;

        switch (currentState)
        {
            case State.NotCounting:
                break;
            case State.Increasing:
                break;
            case State.Decreasing:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void Awake()
    {
        currentAnnihilationScore = 0f;
        currentState = State.NotCounting;
    }

    private void OnEnable()
    {
        EnvironmentEventBus.OnGameStart.Subscribe(Handle_OnGameStart);
        EnvironmentEventBus.OnPause.Subscribe(Handle_OnPause);
        EnvironmentEventBus.OnGameOver.Subscribe(Handle_OnGameOver);
    }

    private void OnDisable()
    {
        EnvironmentEventBus.OnGameStart.Unsubscribe(Handle_OnGameStart);
        EnvironmentEventBus.OnPause.Unsubscribe(Handle_OnPause);
        EnvironmentEventBus.OnGameOver.Unsubscribe(Handle_OnGameOver);
    }

    private void Update()
    {
        switch(currentState)
        {
            case State.NotCounting:
                break;
            case State.Increasing:
                currentAnnihilationScore += Time.deltaTime * defaultAnnihilationScoreIncreasingMultiplier;
                CheckAnnihilationScoreIncreasedToPlusOne();
                break;
            case State.Decreasing:
                currentAnnihilationScore -= Time.deltaTime * defaultAnnihilationScoreDecreasingMultiplier;
                CheckAnnihilationScoreDecreasedToZero();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        annihilationScaleSlider.value = currentAnnihilationScore;
    }

    private void CheckAnnihilationScoreIncreasedToPlusOne()
    {
        if (currentAnnihilationScore < 1) return;
        
        EnvironmentEventBus.OnAnnihilationRunning.Publish();
        ChangeState(State.Decreasing);
    }

    private void CheckAnnihilationScoreDecreasedToZero()
    {
        if (currentAnnihilationScore > 0) return;
        
        EnvironmentEventBus.OnDefaultRunning.Publish();
        ChangeState(State.Increasing);
    }

    private void Handle_OnPause()
    {
        ChangeState(State.NotCounting);
    }

    private void Handle_OnGameStart()
    {
        ChangeState(State.Increasing);
    }

    private void Handle_OnGameOver()
    {
        ChangeState(State.NotCounting);
    }
}
