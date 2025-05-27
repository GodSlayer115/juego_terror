using System.Collections;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Comportamiento de un enemigo con IA que patrulla una zona, detecta al jugador dentro de un rango,
/// lo persigue y lo ataca si está dentro del alcance.
/// </summary>
public class AiEnemigo : MonoBehaviour
{
    /// <summary>
    /// Referencia al componente NavMeshAgent que gestiona la navegación del enemigo.
    /// </summary>
    public NavMeshAgent monstruo;

    /// <summary>
    /// Cantidad de daño que inflige el enemigo por ataque.
    /// </summary>
    public int damage;

    /// <summary>
    /// Velocidad del enemigo mientras patrulla.
    /// </summary>
    public float velocidadCaminando = 1.5f;

    /// <summary>
    /// Velocidad del enemigo al perseguir al jugador.
    /// </summary>
    public float velocidadCorriendo = 4.5f;

    /// <summary>
    /// Distancia máxima a la que el enemigo puede detectar al jugador.
    /// </summary>
    public float rango = 10f;

    /// <summary>
    /// Distancia mínima a la que el enemigo puede atacar al jugador.
    /// </summary>
    public float rangoAtaque = 2f;

    private Transform objetivo;
    private Health playerHP;
    private Collider coll;

    /// <summary>
    /// Componente de animaciones del enemigo.
    /// </summary>
    public Animator animator;

    private bool persiguiendo = false;
    private bool patrullando = true;
    private bool atacando = false;
    private Coroutine patrullaRoutine;

    /// <summary>
    /// Inicializa referencias a componentes del jugador y del propio enemigo.
    /// Comienza la rutina de patrullaje.
    /// </summary>
    void Start()
    {
        patrullaRoutine = StartCoroutine(Patrullar());

        animator = GetComponent<Animator>();
        objetivo = GameObject.FindWithTag("camilo").transform;
        playerHP = GameObject.FindWithTag("camilo").GetComponent<Health>();
        coll = GetComponentInChildren<Collider>();
    }

    /// <summary>
    /// Lógica principal del comportamiento del enemigo:
    /// - Detecta al jugador dentro de un rango.
    /// - Persigue al jugador.
    /// - Ataca si está lo suficientemente cerca.
    /// - Cambia entre patrullaje y persecución.
    /// </summary>
    void Update()
    {
        float distancia = Vector3.Distance(transform.position, objetivo.position);

        // Si el jugador está dentro del rango de detección
        if (distancia < rango)
        {
            if (!persiguiendo)
            {
                persiguiendo = true;
                patrullando = false;

                // Detiene la rutina de patrullaje si se inicia la persecución
                if (patrullaRoutine != null)
                    StopCoroutine(patrullaRoutine);

                monstruo.speed = velocidadCorriendo;
            }

            // Persigue al jugador
            monstruo.SetDestination(objetivo.position);

            // Si el jugador está dentro del rango de ataque, inicia el ataque
            if (distancia <= rangoAtaque)
            {
                if (!atacando)
                    StartCoroutine(Atacar());
            }
            else
            {
                // Si se aleja, resetea el trigger de ataque
                if (atacando)
                {
                    animator.ResetTrigger("Atacar");
                }
            }
        }
        else
        {
            // Si el jugador está fuera del rango, reanuda patrullaje
            if (persiguiendo)
            {
                persiguiendo = false;
                patrullando = true;
                patrullaRoutine = StartCoroutine(Patrullar());
            }
        }

        // Actualiza la animación de movimiento según la velocidad
        animator.SetFloat("Velocidad", monstruo.velocity.magnitude);
    }

    /// <summary>
    /// Corrutina que hace que el enemigo se mueva a posiciones aleatorias dentro de su área de patrullaje.
    /// </summary>
    /// <returns>Retorna una espera aleatoria entre movimientos.</returns>
    IEnumerator Patrullar()
    {
        while (patrullando)
        {
            // Genera un punto aleatorio alrededor del enemigo
            Vector3 puntoAleatorio = Random.insideUnitSphere * 10f + transform.position;
            puntoAleatorio.y = transform.position.y;

            NavMeshHit hit;

            // Valida que el punto esté dentro del NavMesh
            if (NavMesh.SamplePosition(puntoAleatorio, out hit, 10f, NavMesh.AllAreas))
            {
                monstruo.speed = velocidadCaminando;
                monstruo.SetDestination(hit.position);
            }

            // Espera antes de moverse al siguiente punto
            yield return new WaitForSeconds(Random.Range(4f, 7f));
        }
    }

    /// <summary>
    /// Corrutina que ejecuta el ataque del enemigo al jugador:
    /// - Se detiene.
    /// - Mira hacia el jugador.
    /// - Ejecuta animación de ataque.
    /// </summary>
    /// <returns>Retorna tras una espera de 1.5 segundos para simular duración del ataque.</returns>
    IEnumerator Atacar()
    {
        atacando = true;
        monstruo.isStopped = true;

        // Mira hacia el jugador antes de atacar
        Vector3 direccion = (objetivo.position - transform.position).normalized;
        direccion.y = 0;

        if (direccion != Vector3.zero)
            transform.rotation = Quaternion.LookRotation(direccion);

        Debug.Log("¡Atacando al jugador!");
        animator.SetTrigger("Atacar");

        yield return new WaitForSeconds(1.5f); // Simula tiempo de ataque

        monstruo.isStopped = false;
        atacando = false;
    }

    /// <summary>
    /// Detecta colisión del trigger del enemigo con otros objetos.
    /// Si se detecta al jugador durante un ataque, se aplica daño.
    /// </summary>
    /// <param name="other">Collider del objeto que entra en contacto.</param>
    private void OnTriggerEnter(Collider other)
    {
        if (atacando && other.gameObject.CompareTag("camilo"))
        {
            Debug.Log("GOLPE");
            playerHP.health -= damage; // Aplica daño al jugador
        }
    }

    /// <summary>
    /// Detecta colisiones físicas durante patrullaje (por ejemplo, con obstáculos).
    /// Si se detecta una colisión que no sea con el jugador, cambia la dirección.
    /// </summary>
    /// <param name="collision">Objeto con el que se colisiona.</param>
    private void OnCollisionEnter(Collision collision)
    {
        if (patrullando && !collision.gameObject.CompareTag("camilo"))
        {
            Debug.Log("Colisión detectada, cambiando de dirección.");
            StartCoroutine(CambiarDireccionPatrulla());
        }
    }

    /// <summary>
    /// Corrutina para cambiar la dirección de patrullaje después de una colisión con un obstáculo.
    /// </summary>
    /// <returns>Retorna tras una breve espera y establece un nuevo destino aleatorio.</returns>
    IEnumerator CambiarDireccionPatrulla()
    {
        yield return new WaitForSeconds(0.1f);

        Vector3 puntoAleatorio = Random.insideUnitSphere * 10f + transform.position;
        puntoAleatorio.y = transform.position.y;

        NavMeshHit hit;
        if (NavMesh.SamplePosition(puntoAleatorio, out hit, 10f, NavMesh.AllAreas))
        {
            monstruo.SetDestination(hit.position);
        }
    }

    /// <summary>
    /// Dibuja visualmente en el editor los rangos de detección y ataque.
    /// Útil para ajustar parámetros durante el diseño del nivel.
    /// </summary>
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, rango); // Rango de visión

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, rangoAtaque); // Rango de ataque
    }
}
