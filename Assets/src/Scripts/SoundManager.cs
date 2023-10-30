using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    [Header("Music")]
    private const int CountMusicVariables = 2;
    [SerializeField] private AudioClip[] songs;
    [SerializeField] private AudioClip gameOver;

    [Header("Effects")]
    private const int CountEffectsVariables = 4;
    [SerializeField] private AudioClip buttonClick;
    [SerializeField] private AudioClip[] stoneMovements;
    [SerializeField] private AudioClip[] wallHits;
    [SerializeField] private AudioClip[] powerUps;

    [Header("UI")]
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Slider effectsVolumeSlider;

    [Header("AudioSources")]
    [SerializeField] private AudioSource songsAudioSource;
    [SerializeField] private AudioSource gameOverAudioSource;
    [SerializeField] private AudioSource buttonClickAudioSource;
    [SerializeField] private AudioSource stoneMovementsAudioSource;
    [SerializeField] private AudioSource wallHitsAudioSource;
    [SerializeField] private AudioSource powerUpsAudioSource;
    
    private AudioSource[] musicAudioSources;
    private AudioSource[] effectsAudioSources;

    private void Awake()
    {
        musicAudioSources = new AudioSource[CountMusicVariables];
        effectsAudioSources = new AudioSource[CountEffectsVariables];
        DontDestroyOnLoad(this);
    }

    private void OnEnable()
    {
        musicVolumeSlider.onValueChanged.AddListener(Handle_onMusicVolumeChanged);
        effectsVolumeSlider.onValueChanged.AddListener(Handle_onEffectsVolumeChanged);
        
        EnvironmentEventBus.OnGameSceneLoad.Subscribe(Handle_OnGameSceneLoad);
        EnvironmentEventBus.OnGameOver.Subscribe(Handle_OnGameOver);
        EnvironmentEventBus.OnStoneCollideObstacle.Subscribe(Handle_OnStoneCollideObstacle);
        EnvironmentEventBus.OnStoneMove.Subscribe(Handle_OnStoneMove);
        EnvironmentEventBus.OnPowerUpPickUp.Subscribe(Handle_OnPowerUpPickUp);
        EnvironmentEventBus.OnStoneCollideWall.Subscribe(Handle_OnStoneCollideWall);
    }

    private void OnDisable()
    {
        musicVolumeSlider.onValueChanged.RemoveListener(Handle_onMusicVolumeChanged);
        effectsVolumeSlider.onValueChanged.RemoveListener(Handle_onEffectsVolumeChanged);
        
        EnvironmentEventBus.OnGameSceneLoad.Unsubscribe(Handle_OnGameSceneLoad);
        EnvironmentEventBus.OnGameOver.Unsubscribe(Handle_OnGameOver);
        EnvironmentEventBus.OnStoneCollideObstacle.Unsubscribe(Handle_OnStoneCollideObstacle);
        EnvironmentEventBus.OnStoneMove.Unsubscribe(Handle_OnStoneMove);
        EnvironmentEventBus.OnPowerUpPickUp.Unsubscribe(Handle_OnPowerUpPickUp);
        EnvironmentEventBus.OnStoneCollideWall.Unsubscribe(Handle_OnStoneCollideWall);
    }

    private void Handle_onMusicVolumeChanged(float value)
    {
        ChangeMusicVolume(value);
    }
    
    private void Handle_onEffectsVolumeChanged(float value)
    {
        ChangeEffectsVolume(value);
    }
    
    private void Handle_OnGameSceneLoad()
    {
        
    }
    
    private void Handle_OnGameOver()
    {
        
    }

    private void Handle_OnStoneCollideObstacle()
    {
        
    }

    private void Handle_OnStoneMove()
    {
        
    }

    private void Handle_OnPowerUpPickUp()
    {
        
    }
    
    private void Handle_OnStoneCollideWall()
    {
        
    }

    private void ChangeMusicVolume(float value)
    {
        foreach (AudioSource audioSource in musicAudioSources)
        {
            audioSource.volume = value;
        }
    }

    private void ChangeEffectsVolume(float value)
    {
        foreach (AudioSource audioSource in effectsAudioSources)
        {
            audioSource.volume = value;
        }
    }
}
