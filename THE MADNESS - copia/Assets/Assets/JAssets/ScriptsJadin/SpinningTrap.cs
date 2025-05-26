using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningTrap : MonoBehaviour
{
    /// <summary>
    /// Referencia al objeto jugador para acceder a su salud.
    /// </summary>
    [SerializeField] private GameObject player;

    /// <summary>
    /// Referencia al componente Health del jugador para modificar su salud.
    /// </summary>
    private Health hp;

    /// <summary>
    /// Cantidad de daño que inflige la trampa al jugador.
    /// </summary>
    [SerializeField] private int damage;

    /// <summary>
    /// Sonido que se reproduce cuando el jugador recibe daño (herido).
    /// </summary>
    [SerializeField] private AudioClip hurt;

    /// <summary>
    /// Sonido adicional (por ejemplo, sonido de ataque o corte).
    /// </summary>
    [SerializeField] private AudioClip slash;

    /// <summary>
    /// Referencia al Rigidbody del objeto (no se usa actualmente).
    /// </summary>
    private Rigidbody rb;

    /// <summary>
    /// Inicializa referencias necesarias al iniciar el juego.
    /// Obtiene el componente Health del jugador para poder modificar su salud.
    /// </summary>
    void Start()
    {
        hp = player.GetComponent<Health>();
    }

    /// <summary>
    /// Detecta la colisión con el jugador y aplica daño y efectos sonoros.
    /// </summary>
    /// <param name="collision">Collider que colisiona con la trampa</param>
    void OnTriggerEnter(Collider collision)
    {
        // Verifica que el objeto colisionado sea el jugador (con etiqueta "camilo")
        if (collision.gameObject.CompareTag("camilo"))
        {
            if (hp != null)
            {
                // Resta salud al jugador
                hp.health -= damage;

                // Reproduce sonidos de daño
                hp.damaged(hurt);
                hp.damaged(slash);
            }
        }
    }
}
