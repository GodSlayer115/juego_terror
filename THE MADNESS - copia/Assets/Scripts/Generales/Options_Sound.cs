using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;

/// <summary>
/// Controla las opciones de audio y calidad gráfica del juego desde el menú de opciones.
/// </summary>
public class Options_Sound : MonoBehaviour
{
    /// <summary>
    /// Dropdown para seleccionar la calidad gráfica.
    /// </summary>
    public TMP_Dropdown graphicsDropdown;

    /// <summary>
    /// Slider para ajustar el volumen maestro.
    /// </summary>
    public Slider masterVol;

    /// <summary>
    /// Slider para ajustar el volumen de la música.
    /// </summary>
    public Slider musicVol;

    /// <summary>
    /// Slider para ajustar el volumen de los efectos de sonido.
    /// </summary>
    public Slider soundVol;

    /// <summary>
    /// Mezclador de audio principal que gestiona los niveles de volumen.
    /// </summary>
    public AudioMixer MainAudioMixer;

    /// <summary>
    /// Cambia la calidad gráfica del juego según el valor seleccionado en el dropdown.
    /// </summary>
    public void ChangeGraphicQuality()
    {
        QualitySettings.SetQualityLevel(graphicsDropdown.value);
    }

    /// <summary>
    /// Ajusta el volumen maestro en el mezclador de audio.
    /// </summary>
    public void ChangeMasterVolume()
    {
        MainAudioMixer.SetFloat("MasterVol", masterVol.value);
    }

    /// <summary>
    /// Ajusta el volumen de los efectos de sonido en el mezclador de audio.
    /// </summary>
    public void ChangeSoundVolume()
    {
        MainAudioMixer.SetFloat("SoundVol", soundVol.value);
    }

    /// <summary>
    /// Ajusta el volumen de la música en el mezclador de audio.
    /// </summary>
    public void ChangeMusicVolume()
    {
        MainAudioMixer.SetFloat("MusicVol", musicVol.value);
    }
}
