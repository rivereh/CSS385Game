using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer mainMixer;
    public AudioMixer sfxMixer;
    public Slider volumeSlider;
    public Slider sfxSlider;
    public Toggle fullscreenToggle;
    public TMP_Dropdown qualityDropdown;

    void Start()
    {
        if (PlayerPrefs.HasKey("Volume"))
            setVolume(PlayerPrefs.GetFloat("Volume"));

        if (PlayerPrefs.HasKey("sfxVolume"))
            setSfxVolume(PlayerPrefs.GetFloat("sfxVolume"));

        if (PlayerPrefs.HasKey("IsFullscreen"))
            SetFullscreen(PlayerPrefs.GetInt("IsFullscreen") == 1 ? true : false);

        if (PlayerPrefs.HasKey("QualityIndex"))
            SetQuality(PlayerPrefs.GetInt("QualityIndex"));
    }

    public void setVolume(float volume)
    {
        mainMixer.SetFloat("volume", volume);
        PlayerPrefs.SetFloat("Volume", volume);
        volumeSlider.value = volume;
    }

    public void setSfxVolume(float volume)
    {
        sfxMixer.SetFloat("sfxvolume", volume);
        PlayerPrefs.SetFloat("sfxVolume", volume);
        sfxSlider.value = volume;
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        if (isFullscreen)
            Screen.SetResolution (Screen.currentResolution.width, Screen.currentResolution.height, true);
        PlayerPrefs.SetInt("IsFullscreen", isFullscreen ? 1 : 0);
        fullscreenToggle.isOn = isFullscreen;
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        PlayerPrefs.SetInt("QualityIndex", qualityIndex);
        qualityDropdown.value = qualityIndex;
    }
}
