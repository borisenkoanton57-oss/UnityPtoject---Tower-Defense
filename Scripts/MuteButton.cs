using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using System.Collections;

public class MuteButton : MonoBehaviour
{

    public AudioMixer mixer;

    public Image icon;
    public Sprite soundOn;
    public Sprite soundOff;

    private bool isMuted;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isMuted = PlayerPrefs.GetInt("Muted", 0) == 1;

        ApplyMute();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleMute()
    {
        isMuted = !isMuted;

        PlayerPrefs.SetInt("Muted", isMuted ? 1 : 0);
        PlayerPrefs.Save();

        ApplyMute();
    }

     void ApplyMute()
    {
        if (isMuted)
        {
            mixer.SetFloat("MasterVolume", -80f);
            icon.sprite = soundOff;
        }
        else
        {
            float volume = PlayerPrefs.GetFloat("MasterVolume", 1f);
            mixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20);
            icon.sprite = soundOn;
        }
    }
}
