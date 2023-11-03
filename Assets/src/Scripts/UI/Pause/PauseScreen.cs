using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image), typeof(Button))]
public class PauseScreen : MonoBehaviour
{
    [SerializeField, Min(0f)] private float delay;
    
    [Header("Pause")]
    [SerializeField] private GameObject pauseButton;
    [SerializeField] private TextMeshProUGUI countdown;
    
    [Header("Countdown")]
    [SerializeField] private GameObject countdownScreen;
    [SerializeField] private GameObject pauseScreen;

    private const float CountdownUpdateDelay = 1f;

    private Button button;

    private void OnEnable()
    {
        button.onClick.AddListener(Handle_OnClick);
    }

    private void OnDisable()
    {
        button.onClick.RemoveListener(Handle_OnClick);
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

        while (timeLeft > 0)
        {
            yield return new WaitForSeconds(CountdownUpdateDelay);
            timeLeft -= 1;
            countdown.text = timeLeft.ToString();
        }
        
        countdownScreen.SetActive(false);
        pauseButton.SetActive(true);
        
        EnvironmentEventBus.OnUnpause.Publish();
    }
}