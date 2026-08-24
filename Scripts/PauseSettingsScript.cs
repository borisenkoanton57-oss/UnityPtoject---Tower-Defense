using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;

public class PauseSettingsScript : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField] private AudioMixer mixer;

    [Header("Sliders")]
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    private void OnEnable()
    {
        masterSlider.value = PlayerPrefs.GetFloat("MasterVolume", 1f);
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 1f);
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume", 1f);

        masterSlider.onValueChanged.AddListener(SetMaster);
        musicSlider.onValueChanged.AddListener(SetMusic);
        sfxSlider.onValueChanged.AddListener(SetSFX);

        SetMaster(masterSlider.value);
        SetMusic(musicSlider.value);
        SetSFX(sfxSlider.value);
    }

    private void OnDisable()
    {
        masterSlider.onValueChanged.RemoveListener(SetMaster);
        musicSlider.onValueChanged.RemoveListener(SetMusic);
        sfxSlider.onValueChanged.RemoveListener(SetSFX);
    }

    private void SetMaster(float value)
    {
        value = Mathf.Clamp(value, 0.0001f, 1f);

        mixer.SetFloat("MasterVolume", Mathf.Log10(value) * 20);

        PlayerPrefs.SetFloat("MasterVolume", value);
        PlayerPrefs.Save();
    }

    private void SetMusic(float value)
    {
        value = Mathf.Clamp(value, 0.0001f, 1f);

        mixer.SetFloat("MusicVolume", Mathf.Log10(value) * 20);

        PlayerPrefs.SetFloat("MusicVolume", value);
        PlayerPrefs.Save();
    }

    private void SetSFX(float value)
    {
        value = Mathf.Clamp(value, 0.0001f, 1f);

        mixer.SetFloat("SFXVolume", Mathf.Log10(value) * 20);

        PlayerPrefs.SetFloat("SFXVolume", value);
        PlayerPrefs.Save();
    }
}
