using System.Collections;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Controlador de IA para un enemigo que patrulla, persigue y ataca al jugador según la distancia.
/// </summary>
public class AiEnemigo : MonoBehaviour
{
    /// <summary>Agente de navegación del enemigo.</summary>
    public NavMeshAgent monstruo;

    /// <summary>Velocidad del enemigo al caminar (patrullar).</summary>
    public float velocidadCaminando = 1.5f;

    /// <summary>Velocidad del enemigo al correr (persecución).</summary>
    public float velocidadCorriendo = 4.5f;

    /// <summary>Distancia máxima para detectar al jugador.</summary>
    public float rango = 10f;

    /// <summary>Distancia mínima para iniciar el ataque.</summary>
    public float rangoAtaque = 2f;

    /// <summary>Referencia al transform del jugador.</summary>
    private Transform objetivo;

    /// <summary>Referencia al componente de salud del jugador.</summary>
    private Health playerHP;

    /// <summary>Componente Animator del enemigo.</summary>
    public Animator animator;

    /// <summary>Collider del enemigo (usado para detectar colisiones de ataque).</summary>
    private Collider coll;

    /// <summary>Indica si el enemigo está persiguiendo al jugador.</summary>
    private bool persiguiendo = false;

    /// <summary>Indica si el enemigo está patrullando.</summary>
    private bool patrullando = true;

    /// <summary>Indica si el enemigo está actualmente atacando.</summary>
    private bool atacando = false;

    /// <summary>Referencia a la rutina de patrullaje.</summary>
    private Coroutine patrullaRoutine;

    /// <summary>
    /// Inicializa referencias necesarias al comenzar.
    /// </summary>
    void Start()
    {
        patrullaRoutine = StartCoroutine(Patrullar());
        animator = GetComponent<Animator>();
        objetivo = GameObject.FindWithTag("camilo").transform;
        playerHP = objetivo.GetComponent<Health>();
        coll = GetComponentInChildren<Collider>();
    }

    /// <summary>
    /// Actualiza el estado del enemigo en cada frame (detección, persecución, ataque).
    /// </summary>
    void Update()
    {
        float distancia = Vector3.Distance(transform.position, objetivo.position);

        if (distancia < rango)
        {
            // Comenzar persecución
            if (!persiguiendo)
            {
                persiguiendo = true;
                patrullando = false;

                if (patrullaRoutine != null)
                    StopCoroutine(patrullaRoutine);

                monstruo.speed = velocidadCorriendo;
            }

            monstruo.SetDestination(objetivo.position);

            // Intentar atacar si está dentro del rango
            if (distancia <= rangoAtaque)
            {
                if (!atacando)
                    StartCoroutine(Atacar());
            }
            else
            {
                if (atacando)
                {
                    // Salió del rango de ataque
                    animator.ResetTrigger("Atacar");
                }
            }
        }
        else
        {
            // Volver a patrullar
            if (persiguiendo)
            {
                persiguiendo = false;
                patrullando = true;
                patrullaRoutine = StartCoroutine(Patrullar());
            }
        }

        // Animación según velocidad
        animator.SetFloat("Velocidad", monstruo.velocity.magnitude);
    }

    /// <summary>
    /// Rutina de patrullaje aleatorio cuando el jugador no está cerca.
    /// </summary>
    /// <returns>Una coroutine de patrullaje.</returns>
    IEnumerator Patrullar()
    {
        while (patrullando)
        {
            Vector3 puntoAleatorio = Random.insideUnitSphere * 10f + transform.position;
            puntoAleatorio.y = transform.position.y;

            NavMeshHit hit;
            if (NavMesh.SamplePosition(puntoAleatorio, out hit, 10f, NavMesh.AllAreas))
            {
                monstruo.speed = velocidadCaminando;
                monstruo.SetDestination(hit.position);
            }

            yield return new WaitForSeconds(Random.Range(4f, 7f));
        }
    }

    /// <summary>
    /// Rutina de ataque al jugador.
    /// </summary>
    /// <returns>Una coroutine del ataque.</returns>
    IEnumerator Atacar()
    {
        atacando = true;
        monstruo.isStopped = true;

        // Girar hacia el jugador
        Vector3 direccion = (objetivo.position - transform.position).normalized;
        direccion.y = 0;
        if (direccion != Vector3.zero)
            transform.rotation = Quaternion.LookRotation(direccion);

        Debug.Log("¡Atacando al jugador!");
        animator.SetTrigger("Atacar");

        // Esperar duración de la animación (ajustable)
        yield return new WaitForSeconds(1.5f);

        monstruo.isStopped = false;
        atacando = false;
    }

    /// <summary>
    /// Detecta si el jugador fue golpeado durante el ataque.
    /// </summary>
    /// <param name="other">Collider del objeto con el que colisionó.</param>
    private void OnTriggerEnter(Collider other)
    {
        if (atacando && other.gameObject.CompareTag("camilo"))
        {
            playerHP.health -= 1000; // Aplica daño
        }
    }

    /// <summary>
    /// Dibuja gizmos en la escena para visualizar el rango de detección y ataque.
    /// </summary>
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, rango);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, rangoAtaque);
    }
}
