using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Controla la transición entre escenas mostrando una pantalla de carga con una barra de progreso.
/// </summary>
public class LoadScena : MonoBehaviour
{
    /// <summary>
    /// Panel de UI que se muestra durante la carga de la escena.
    /// </summary>
    public GameObject panelCarga;

    /// <summary>
    /// Barra de progreso que indica visualmente el avance de la carga de la escena.
    /// </summary>
    public Slider barraCarga;

    /// <summary>
    /// Inicia el proceso de cambio de escena mostrando el panel de carga.
    /// </summary>
    /// <param name="nombre">Nombre de la escena que se desea cargar.</param>
    public void CambiarEscena(string nombre)
    {
        Debug.Log("Cargando: " + nombre);
        StartCoroutine(CargarEscena(nombre));
    }

    /// <summary>
    /// Corrutina que gestiona la carga asíncrona de la escena y actualiza la barra de progreso.
    /// </summary>
    /// <param name="nombre">Nombre de la escena a cargar.</param>
    /// <returns>Un enumerador para el control de la corrutina.</returns>
    private IEnumerator CargarEscena(string nombre)
    {
        if (panelCarga != null)
        {
            panelCarga.SetActive(true); // Muestra la pantalla de carga
        }

        // Reinicia la barra de carga
        if (barraCarga != null)
        {
            barraCarga.value = 0f;
        }

        // Restablece la escala de tiempo por si estaba pausado
        Time.timeScale = 1f;

        // Espera un poco para mostrar el panel de carga
        yield return new WaitForSeconds(0.5f);

        AsyncOperation operacion = SceneManager.LoadSceneAsync(nombre);
        operacion.allowSceneActivation = false;

        while (!operacion.isDone)
        {
            float progreso = Mathf.Clamp01(operacion.progress / 0.9f); // Normaliza el progreso

            if (barraCarga != null)
            {
                barraCarga.value = progreso;
            }

            // Espera hasta que esté lista la activación
            if (operacion.progress >= 0.9f)
            {
                if (barraCarga != null)
                {
                    barraCarga.value = 1f;
                }

                Debug.Log("esperando2");
                yield return new WaitForSeconds(0.2f);

                operacion.allowSceneActivation = true;
                break;
            }

            yield return null;
        }
    }

    /// <summary>
    /// Detecta colisiones con otros objetos y cambia la escena usando el nombre del tag del objeto colisionado.
    /// </summary>
    /// <param name="other">Collider del objeto que entra en contacto.</param>
    private void OnTriggerEnter(Collider other)
    {
        string nombre = other.tag;
        CambiarEscena(nombre);
    }
}
