using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [SerializeField] private AudioMixer mixer;
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource fxSource;
    [SerializeField] private AudioSource uiSource;
    [SerializeField] private AudioClip sceneMusic;
    [SerializeField] private AudioClip clickSound;
    [SerializeField] private AudioClip gameOverSound;
    [SerializeField] private AudioClip victorySound;
    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private AudioClip shootSound;
    [SerializeField] private AudioClip hurtSound;
    [SerializeField] private AudioClip enemyDeathSound;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        ApplySavedVolumes();

        if (sceneMusic != null && musicSource != null)
        {
            musicSource.clip = sceneMusic;
            musicSource.loop = true;
            musicSource.Play();
        }

  
        foreach (Button b in FindObjectsByType<Button>(FindObjectsInactive.Include, FindObjectsSortMode.None))
            b.onClick.AddListener(PlayClick);
    }

    public void ApplySavedVolumes()
    {
        SetVolume("MasterVolume", PlayerPrefs.GetFloat("MasterVolume", 0.8f));
        SetVolume("MusicVolume", PlayerPrefs.GetFloat("MusicVolume", 0.8f));
        SetVolume("FXVolume", PlayerPrefs.GetFloat("FXVolume", 0.8f));
        SetVolume("UIVolume", PlayerPrefs.GetFloat("UIVolume", 0.8f));
    }

    public void SetVolume(string parameter, float value01)
    {
        float dB = Mathf.Log10(Mathf.Clamp(value01, 0.0001f, 1f)) * 20f;
        mixer.SetFloat(parameter, dB);
    }

    public void PlayFX(AudioClip clip)
    {
        if (clip != null && fxSource != null)
            fxSource.PlayOneShot(clip);
    }

    public void PlayClick()
    {
        if (clickSound != null && uiSource != null)
            uiSource.PlayOneShot(clickSound);
    }

    public void PlayJump() { PlayFX(jumpSound); }
    public void PlayShoot() { PlayFX(shootSound); }
    public void PlayHurt() { PlayFX(hurtSound); }
    public void PlayEnemyDeath() { PlayFX(enemyDeathSound); }

    public void PlayGameOver()
    {
        if (musicSource != null)
            musicSource.Stop();
        PlayFX(gameOverSound);
    }

    public void PlayVictory()
    {
        if (musicSource != null)
            musicSource.Stop();
        PlayFX(victorySound);
    }
}