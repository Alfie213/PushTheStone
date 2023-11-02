using System;
using System.Collections;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int CurrentScore => Convert.ToInt32(currentScore);
    
    [SerializeField] private float defaultScoreMultiplier;
    
    [Header("ScoreBooster")]
    [SerializeField] private float scoreBoosterMultiplier;
    [SerializeField] private float boostDuration;

    private float currentScore;
    private State currentState;

    private Coroutine endBoost;

    private enum State
    {
        DefaultCounting,
        BoostCounting,
        NotCounting
    }

    private void ChangeState(State state)
    {
        currentState = state;

        switch (currentState)
        {
            case State.DefaultCounting:
                break;
            case State.NotCounting:
                break;
            case State.BoostCounting:
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
        EnvironmentEventBus.OnPause.Subscribe(Handle_OnPause);
        EnvironmentEventBus.OnRunning.Subscribe(Handle_OnRunning);
        EnvironmentEventBus.OnGameOver.Subscribe(Handle_OnGameOver);
        EnvironmentEventBus.OnScoreBoosterPickUp.Subscribe(Handle_OnScoreBoosterPickUp);
    }

    private void OnDisable()
    {
        EnvironmentEventBus.OnPause.Unsubscribe(Handle_OnPause);
        EnvironmentEventBus.OnRunning.Unsubscribe(Handle_OnRunning);
        EnvironmentEventBus.OnGameOver.Unsubscribe(Handle_OnGameOver);
        EnvironmentEventBus.OnScoreBoosterPickUp.Unsubscribe(Handle_OnScoreBoosterPickUp);
    }

    private void Update()
    {
        switch (currentState)
        {
            case State.DefaultCounting:
                currentScore += defaultScoreMultiplier * Time.deltaTime;
                break;
            case State.BoostCounting:
                currentScore += scoreBoosterMultiplier * Time.deltaTime;
                break;
            case State.NotCounting:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void Handle_OnPause()
    {
        ChangeState(State.NotCounting);
    }

    private void Handle_OnRunning()
    {
        ChangeState(State.DefaultCounting);
    }
    
    private void Handle_OnGameOver()
    {
        ChangeState(State.NotCounting);
    }

    private void Handle_OnScoreBoosterPickUp()
    {
        if (currentState == State.BoostCounting)
        {
            StopCoroutine(endBoost);
            endBoost = StartCoroutine(EndBoost());
        }
        else
        {
            ChangeState(State.BoostCounting);
            endBoost = StartCoroutine(EndBoost());
        }
    }

    private IEnumerator EndBoost()
    {
        yield return new WaitForSeconds(boostDuration);
        ChangeState(State.DefaultCounting);
    }
}
