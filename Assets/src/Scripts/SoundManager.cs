using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    [Header("Music")]
    [SerializeField] private AudioClip[] songs;
    [SerializeField] private AudioClip gameOver;

    [Header("Effects")]
    [SerializeField] private AudioClip buttonClick;
    [SerializeField] private AudioClip[] stoneMovements;
    [SerializeField] private AudioClip[] wallHits;
    [SerializeField] private AudioClip[] powerUps;

    [Header("UI")]
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Slider effectsVolumeSlider;

    private AudioSource audioSource;

    private float musicVolume;
    private float effectsVolume;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
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
        musicVolume = value;
    }
    
    private void Handle_onEffectsVolumeChanged(float value)
    {
        effectsVolume = value;
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

    private void ApplyMusicSettings()
    {
        audioSource.volume = musicVolume;
    }

    private void ApplyEffectsSettings()
    {
        audioSource.volume = effectsVolume;
    }
}
