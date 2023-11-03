using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] private ScoreManager scoreManager;
    [SerializeField] private TextMeshProUGUI scoreText;

    private void Update()
    {
        scoreText.text = scoreManager.CurrentScore.ToString();
    }
}
