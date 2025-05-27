using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Controla el comportamiento de una barra de vida (HP) visual usando una imagen con relleno.
/// </summary>
public class HPslider : MonoBehaviour
{
    /// <summary>
    /// Valor máximo de puntos de vida (HP) del jugador.
    /// </summary>
    public int maxHP;

    /// <summary>
    /// Valor actual de HP del jugador.
    /// </summary>
    public int currHP;

    /// <summary>
    /// Valor de HP actual extraído del objeto Health del jugador.
    /// </summary>
    public int playerHP;

    /// <summary>
    /// Referencia al componente de imagen que se utilizará para mostrar la barra de vida.
    /// </summary>
    public Image fill;

    /// <summary>
    /// Referencia al script <see cref="Health"/> que contiene el estado de vida del jugador.
    /// </summary>
    public Health playerHealth;

    /// <summary>
    /// Actualiza el valor de HP actual desde el script Health del jugador
    /// y actualiza el relleno de la imagen en cada fotograma.
    /// </summary>
    void Update()
    {
        playerHP = playerHealth.health;
        currHP = playerHP;
        getCurrentFill();
    }

    /// <summary>
    /// Calcula la proporción actual de HP respecto al máximo
    /// y actualiza el valor del relleno de la imagen de la barra de vida.
    /// </summary>
    void getCurrentFill()
    {
        float fillAmount = (float)currHP / (float)maxHP;
        fill.fillAmount = fillAmount;
    }
}
