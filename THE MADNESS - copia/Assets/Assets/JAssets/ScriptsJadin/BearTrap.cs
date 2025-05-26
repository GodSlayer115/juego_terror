using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class BearTrap : MonoBehaviour
{
    /// <summary>
    /// Referencia al jugador para acceder a su movimiento.
    /// </summary>
    [SerializeField] private GameObject player;

    /// <summary>
    /// Componente PlayerMovement del jugador, usado para deshabilitar el movimiento cuando la trampa se activa.
    /// </summary>
    private PlayerMovement movement;

    /// <summary>
    /// Componente Animator del objeto trampa para controlar animaciones.
    /// </summary>
    private Animator animator;

    /// <summary>
    /// AudioSource para reproducir sonido de activación de la trampa.
    /// </summary>
    public AudioSource trapSound;

    /// <summary>
    /// Panel UI que aparece cuando el jugador queda atrapado.
    /// </summary>
    [SerializeField] private GameObject panel;

    /// <summary>
    /// Componente TrapFill que controla la barra de progreso para liberar al jugador.
    /// </summary>
    [SerializeField] private TrapFill trapFill;

    /// <summary>
    /// Estado que indica si la trampa está activa (jugador atrapado).
    /// </summary>
    private bool active = false;

    /// <summary>
    /// Inicializa referencias a componentes necesarios.
    /// </summary>
    void Start()
    {
        movement = player.GetComponent<PlayerMovement>();
        animator = gameObject.GetComponent<Animator>();
    }

    /// <summary>
    /// Detecta la colisión con el jugador y activa la trampa.
    /// </summary>
    /// <param name="other">Collider que entra en la trampa</param>
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Colisión con " + other.gameObject.tag);

        if (other.gameObject.CompareTag("camilo"))
        {
            active = true;

            // Dispara la animación de la trampa
            animator.SetTrigger("Trap");

            // Deshabilita el movimiento del jugador para simular que está atrapado
            movement.enabled = false;

            // Reproduce el sonido de activación de la trampa
            trapSound.Play();

            // Muestra el panel UI para que el jugador pueda interactuar para liberarse
            panel.SetActive(true);
        }
    }

    /// <summary>
    /// En cada frame fijo verifica si el jugador logró llenar la barra de progreso para liberarse.
    /// Si es así, resetea estado y destruye la trampa.
    /// </summary>
    private void FixedUpdate()
    {
        if (active && trapFill.currTrapTime >= trapFill.maxTrapTime)
        {
            // Resetea el tiempo acumulado de la trampa
            trapFill.currTrapTime = 0;

            // Oculta el panel de UI
            panel.SetActive(false);

            // Habilita de nuevo el movimiento del jugador
            movement.enabled = true;

            // Destruye la trampa para que no pueda volver a activarse
            Destroy(gameObject);
        }
    }
}
