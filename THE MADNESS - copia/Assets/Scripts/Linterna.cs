using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// Controla la funcionalidad de una linterna, permitiendo encenderla y apagarla con una tecla,
/// y reproducir sonidos asociados.
/// </summary>
public class Linterna : MonoBehaviour
{
    /// <summary>
    /// Luz de la linterna que será encendida o apagada.
    /// </summary>
    [SerializeField] private Light luzlinterna;

    /// <summary>
    /// Fuente de audio utilizada para reproducir sonidos al alternar la linterna.
    /// </summary>
    public AudioSource soundLinterna;

    /// <summary>
    /// Verifica cada frame si se ha presionado la tecla <c>F</c> para alternar la linterna.
    /// </summary>
    void Update()
    {
        if (Input.GetKeyDown("f"))
        {
            if (luzlinterna.enabled == true)
            {
                luzlinterna.enabled = false;
                soundLinterna.Play();
            }
            else if (luzlinterna.enabled == false)
            {
                luzlinterna.enabled = true;
                soundLinterna.Play();
            }
        }
    }

    /// <summary>
    /// Reproduce un sonido específico relacionado con la linterna.
    /// </summary>
    /// <param name="clip">El <paramref name="clip"/> de audio que se reproducirá una vez.</param>
    public void lighted(AudioClip clip)
    {
        soundLinterna.PlayOneShot(clip);
    }
}
