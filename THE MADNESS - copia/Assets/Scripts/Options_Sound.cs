using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;

public class Options_Sound : MonoBehaviour
{
    public TMP_Dropdown graphicsDropdown;
    public Slider masterVol, musicVol, soundVol;
    public AudioMixer MainAudioMixer;

    public void ChangeGraphicQuality()
    {
        QualitySettings.SetQualityLevel(graphicsDropdown.value);
    }

    public void ChangeMasterVolume()
    {
        MainAudioMixer.SetFloat("MasterVol", masterVol.value);
    }

    public void ChangeSoundVolume()
    {
        MainAudioMixer.SetFloat("SoundVol", soundVol.value);
    }

    public void ChangeMusicVolume()
    {
        MainAudioMixer.SetFloat("MusicVol", musicVol.value);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
