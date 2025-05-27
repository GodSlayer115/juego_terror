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
    /// Detecta cuando el jugador entra en contacto con el orbe.
    /// Aplica curación, buffs temporales y efectos visuales/sonoros, 
    /// y destruye el objeto tras ser recogido.
    /// </summary>
    /// <param name="other">Collider del objeto que entra en contacto con el orbe.</param>
    private void OnTriggerEnter(Collider other)
    {
        // Verifica si el objeto que entró en contacto tiene la etiqueta del jugador
        if (other.gameObject.CompareTag("camilo"))
        {
            // Aumenta la salud del jugador con el valor definido en 'heal'
            health.health += heal;

            // Inicia una corrutina que aplica un buff temporal al salto
            fpsController.StartCoroutine(fpsController.jumpBuff(jumpBuffTime, jumpBuff));

            // Inicia una corrutina que aplica un buff temporal a la velocidad de movimiento
            fpsController.StartCoroutine(fpsController.speedBuff(speedBuffTime, speedBuff));

            // Desactiva el renderizador del orbe para que desaparezca visualmente tras recogerlo
            gameObject.GetComponent<Renderer>().enabled = false;

            // Reproduce un sonido al recoger el orbe
            orbing.PlayOneShot(pick);

            // Informa al GameManager para actualizar el contador de orbes rojos recogidos
            GameManager.Instance.Contador("orbesRojos");

            // Destruye el objeto (orbe) una vez que el sonido haya terminado
            Destroy(gameObject, pick.length);
        }
    }

}
