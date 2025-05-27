using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrapFill : MonoBehaviour
{
    /// <summary>
    /// Tiempo máximo que puede alcanzar la carga de la trampa.
    /// </summary>
    public int maxTrapTime;

    /// <summary>
    /// Tiempo actual de carga de la trampa.
    /// </summary>
    public float currTrapTime;

    /// <summary>
    /// Velocidad a la que decae la carga de la trampa por segundo.
    /// </summary>
    private float decayTrapTime;

    /// <summary>
    /// Cantidad de carga que se incrementa al soltar la tecla 'E'.
    /// </summary>
    private float pressEcharge = 2.5f;

    /// <summary>
    /// Imagen UI que muestra visualmente la carga de la trampa.
    /// </summary>
    public Image fill;

    /// <summary>
    /// Objeto del juego que representa la interfaz o visualización de la carga de la trampa.
    /// </summary>
    [SerializeField] private GameObject trapFill;

    /// <summary>
    /// Método llamado al iniciar el script. Desactiva el objeto visual de la carga para ocultarlo inicialmente.
    /// </summary>
    private void Start()
    {
        trapFill.SetActive(false);
    }

    /// <summary>
    /// Se llama a intervalos fijos para actualizar la lógica del llenado y decaimiento de la trampa.
    /// </summary>
    void FixedUpdate()
    {
        // Asegura que currTrapTime se mantenga dentro de los límites válidos.
        if (currTrapTime < 0 || currTrapTime >= maxTrapTime)
        {
            currTrapTime = 0;
        }

        // Si el jugador suelta la tecla E, incrementa la carga de la trampa.
        if (Input.GetKeyUp(KeyCode.E))
        {
            currTrapTime += pressEcharge;
        }

        // Aplica el decaimiento constante a la carga.
        currTrapTime -= decayTrapTime;

        // Actualiza la barra de llenado visual con el valor actual.
        getCurrentFill();
    }

    /// <summary>
    /// Actualiza la barra UI para reflejar la carga actual de la trampa como un valor entre 0 y 1.
    /// </summary>
    void getCurrentFill()
    {
        float fillAmount = (float)currTrapTime / (float)maxTrapTime;
        fill.fillAmount = fillAmount;
    }
}
