using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rorb : MonoBehaviour
{
    /// <summary>
    /// Referencia al objeto jugador para acceder a sus componentes.
    /// </summary>
    public GameObject player;

    /// <summary>
    /// Componente Health del jugador para modificar su vida.
    /// </summary>
    public Health health;

    /// <summary>
    /// Componente PlayerMovement del jugador para aplicar buffs temporales.
    /// </summary>
    public PlayerMovement fpsController;

    /// <summary>
    /// Duración en segundos del buff de velocidad.
    /// </summary>
    public float speedBuffTime;

    /// <summary>
    /// Valor del incremento temporal de velocidad que otorga el orbe.
    /// </summary>
    public int speedBuff;

    /// <summary>
    /// Duración en segundos del buff de salto.
    /// </summary>
    public float jumpBuffTime;

    /// <summary>
    /// Valor del incremento temporal de salto que otorga el orbe.
    /// </summary>
    public float jumpBuff;

    /// <summary>
    /// AudioSource usado para reproducir sonidos relacionados al orbe.
    /// </summary>
    public AudioSource orbing;

    /// <summary>
    /// Clip de audio que se reproduce al recoger el orbe.
    /// </summary>
    public AudioClip pick;

    /// <summary>
    /// Cantidad de vida que el orbe restaura al ser recogido.
    /// </summary>
    public int heal;

    /// <summary>
    /// Inicializa las referencias a los componentes del jugador.
    /// </summary>
    void Start()
    {
        player = GameObject.FindWithTag("camilo");
        health = player.GetComponent<Health>();
        fpsController = player.GetComponent<PlayerMovement>();
    }

    /// <summary>
    /// Detecta la colisión con el jugador para aplicar el efecto del orbe.
    /// </summary>
    /// <param name="other">Collider que interactúa con el orbe</param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("camilo"))
        {
            // Aumenta la vida del jugador
            health.health += heal;

            // Aplica buff temporal de salto y velocidad
            fpsController.StartCoroutine(fpsController.jumpBuff(jumpBuffTime, jumpBuff));
            fpsController.StartCoroutine(fpsController.speedBuff(speedBuffTime, speedBuff));

            // Oculta el orbe visualmente para evitar múltiples recogidas
            gameObject.GetComponent<Renderer>().enabled = false;

            // Reproduce el sonido de recogida
            orbing.PlayOneShot(pick);

            // Destruye el orbe después de que el sonido termine
            Destroy(gameObject, pick.length);
        }
    }
}
