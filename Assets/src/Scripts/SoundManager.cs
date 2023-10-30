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
    }

    private void OnDisable()
    {
        musicVolumeSlider.onValueChanged.RemoveListener(Handle_onMusicVolumeChanged);
        effectsVolumeSlider.onValueChanged.RemoveListener(Handle_onEffectsVolumeChanged);
    }

    private void Handle_onMusicVolumeChanged(float value)
    {
        musicVolume = value;
    }
    
    private void Handle_onEffectsVolumeChanged(float value)
    {
        effectsVolume = value;
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
