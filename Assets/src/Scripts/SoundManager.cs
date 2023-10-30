using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    [Header("Music")]
    private const int CountMusicVariables = 2;
    [SerializeField] private AudioClip[] songs;
    [SerializeField] private AudioClip gameOver;

    [Header("Effects")]
    private const int CountEffectsVariables = 5;
    [SerializeField] private AudioClip buttonClick;
    [SerializeField] private AudioClip[] stoneShatterings;
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
    [SerializeField] private AudioSource stoneShatteringAudioSource;
    [SerializeField] private AudioSource stoneMovementsAudioSource;
    [SerializeField] private AudioSource wallHitsAudioSource;
    [SerializeField] private AudioSource powerUpsAudioSource;
    
    private AudioSource[] musicAudioSources;
    private AudioSource[] effectsAudioSources;

    public void PlayButtonClick()
    {
        buttonClickAudioSource.Play();
    }

    private void PlayRandomSong()
    {
        songsAudioSource.clip = songs[Random.Range(0, songs.Length)];
        songsAudioSource.Play();
    }

    private void PlayGameOver()
    {
        gameOverAudioSource.Play();
    }

    private void PlayRandomStoneShattering()
    {
        stoneShatteringAudioSource.clip = stoneShatterings[Random.Range(0, stoneShatterings.Length)];
        stoneShatteringAudioSource.Play();
    }
    
    private void PlayRandomStoneMovement()
    {
        stoneMovementsAudioSource.clip = stoneMovements[Random.Range(0, stoneMovements.Length)];
        stoneMovementsAudioSource.Play();
    }

    private void PlayRandomWallHit()
    {
        wallHitsAudioSource.clip = wallHits[Random.Range(0, wallHits.Length)];
        wallHitsAudioSource.Play();
    }

    private void PlayRandomPowerUp()
    {
        powerUpsAudioSource.clip = powerUps[Random.Range(0, powerUps.Length)];
        wallHitsAudioSource.Play();
    }
    
    private void Awake()
    {
        musicAudioSources = new AudioSource[CountMusicVariables];
        effectsAudioSources = new AudioSource[CountEffectsVariables];
        InitSoloAudioSources();
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
        PlayRandomSong();
    }
    
    private void Handle_OnGameOver()
    {
        PlayGameOver();
    }

    private void Handle_OnStoneCollideObstacle()
    {
        PlayRandomStoneShattering();
    }

    private void Handle_OnStoneMove()
    {
        PlayRandomStoneMovement();
    }

    private void Handle_OnPowerUpPickUp()
    {
        PlayRandomPowerUp();
    }
    
    private void Handle_OnStoneCollideWall()
    {
        PlayRandomWallHit();
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

    private void InitSoloAudioSources() // Solo AudioSource means, that this AudioSource is playing only one AudioClip.
    {
        gameOverAudioSource.clip = gameOver;
        buttonClickAudioSource.clip = buttonClick;
    }
}
