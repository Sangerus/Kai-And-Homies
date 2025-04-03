using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public Slider soundSlider;  // Sound effects volume
    public Slider musicSlider;  // Music volume
    public AudioSource musicSource;  // Background music
    public AudioSource[] soundSources;  // Sound effects sources

    void Start()
    {
        // Load saved settings
        float savedMusicVolume = PlayerPrefs.GetFloat("MusicVolume", 1f);
        float savedSoundVolume = PlayerPrefs.GetFloat("SoundVolume", 1f);

        // Apply values to sliders
        if (musicSlider != null)
        {
            musicSlider.value = savedMusicVolume;
            musicSlider.onValueChanged.AddListener(SetMusicVolume);
        }

        if (soundSlider != null)
        {
            soundSlider.value = savedSoundVolume;
            soundSlider.onValueChanged.AddListener(SetSoundVolume);
        }

        // Apply saved volume
        SetMusicVolume(savedMusicVolume);
        SetSoundVolume(savedSoundVolume);
    }

    void SetMusicVolume(float value)
    {
        if (musicSource != null)
        {
            musicSource.volume = value;  // Corrected: This only affects music
            PlayerPrefs.SetFloat("MusicVolume", value);
            PlayerPrefs.Save();
        }
    }

    void SetSoundVolume(float value)
    {
        foreach (var source in soundSources)
        {
            if (source != null)
            {
                source.volume = value;  // Corrected: This only affects sound effects
            }
        }
        PlayerPrefs.SetFloat("SoundVolume", value);
        PlayerPrefs.Save();
    }
}
