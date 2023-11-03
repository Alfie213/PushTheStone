using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PauseScreen : MonoBehaviour
{
    [SerializeField, Min(0f)] private float delay;
    
    [Header("Pause")]
    [SerializeField] private GameObject pauseScreen;
    [SerializeField] private Button pauseTapToPlayButton;
    [SerializeField] private GameObject pauseButton;
    
    [Header("Countdown")]
    [SerializeField] private GameObject countdownScreen;
    [SerializeField] private TextMeshProUGUI countdownText;
    
    private const float CountdownUpdateDelay = 1f;

    private void OnEnable()
    {
        pauseTapToPlayButton.onClick.AddListener(Handle_OnClick);
    }

    private void OnDisable()
    {
        pauseTapToPlayButton.onClick.RemoveListener(Handle_OnClick);
    }

    public void Handle_OnClick()
    {
        StartCoroutine(UnpauseWithDelay());
    }

    private IEnumerator UnpauseWithDelay()
    {
        pauseScreen.SetActive(false);
        countdownScreen.SetActive(true);

        int timeLeft = Convert.ToInt32(delay);
        countdownText.text = timeLeft.ToString();

        while (timeLeft > 0)
        {
            yield return new WaitForSeconds(CountdownUpdateDelay);
            timeLeft -= 1;
            countdownText.text = timeLeft.ToString();
        }
        
        countdownScreen.SetActive(false);
        pauseButton.SetActive(true);
        
        EnvironmentEventBus.OnUnpause.Publish();
    }
}