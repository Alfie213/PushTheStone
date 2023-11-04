using System;
using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;

    private void OnEnable()
    {
        EnvironmentEventBus.OnScoreChange.Subscribe(Handle_OnScoreChange);
    }

    private void OnDisable()
    {
        EnvironmentEventBus.OnScoreChange.Unsubscribe(Handle_OnScoreChange);
    }

    private void Handle_OnScoreChange(float currentScore)
    {
        UpdateScoreText(currentScore);
    }

    private void UpdateScoreText(float currentScore)
    {
        scoreText.text = Convert.ToInt32(currentScore).ToString();
    }
}
