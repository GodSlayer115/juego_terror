using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Controlador del enemigo que gestiona su comportamiento, incluyendo navegación, daño por contacto y aura, animaciones y efectos de sonido.
/// </summary>
public class EnemyController : MonoBehaviour
{
    [Header("Movement and Combat Stats")]
    [SerializeField] private float speed = 3f;               ///< Velocidad de movimiento del enemigo
    [SerializeField] private int damage = 10;                ///< Daño aplicado en contacto físico
    [SerializeField] private int aura = 5;                   ///< Daño por aura al permanecer cerca del jugador
    [SerializeField] private float pushBackForce = 15f;      ///< Fuerza aplicada para empujar al jugador

    [Header("References")]
    private GameObject player;                               ///< Referencia al jugador en la escena
    private Health playerHealth;                             ///< Componente Health del jugador
    private Rigidbody rb;                                    ///< Rigidbody propio para física y empujes
    private NavMeshAgent navMeshAgent;                       ///< Componente NavMeshAgent para navegación IA
    private Animator animator;                               ///< Animator para animaciones del enemigo

    [Header("Aura Cooldown")]
    private bool auraCD = false;                             ///< Controla cooldown del daño por aura
    [SerializeField] private float auraCooldownTime;         ///< Tiempo de cooldown del aura

    [Header("Audio Clips and Source")]
    [SerializeField] private AudioClip hurt;                 ///< Clip de sonido al herir al jugador
    [SerializeField] private AudioClip bite;                 ///< Clip de sonido de mordida
    [SerializeField] private List<AudioClip> clipWakeUp;     ///< Clips para el despertar del enemigo
    [SerializeField] private List<AudioClip> clipScream;     ///< Clips para gritos del enemigo
    [SerializeField] private List<AudioClip> clipChase;      ///< Clips para sonidos de persecución
    [SerializeField] private AudioSource zombieAudio;        ///< AudioSource para reproducir sonidos

    /// <summary>
    /// Inicialización al iniciar el juego.
    /// </summary>
    void Start()
    {
        player = GameObject.FindWithTag("camilo");
        if (player != null)
        {
            playerHealth = player.GetComponent<Health>();
        }

        rb = GetComponent<Rigidbody>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        navMeshAgent.speed = speed;
        navMeshAgent.enabled = false;

        StartCoroutine(StartSequence());
    }

    /// <summary>
    /// Secuencia inicial que reproduce sonidos y activa al enemigo.
    /// </summary>
    /// <returns><c>IEnumerator</c> para activar el enemigo tras la introducción.</returns>
    IEnumerator StartSequence()
    {
        int i = Random.Range(0, clipWakeUp.Count);
        int i2 = Random.Range(0, clipScream.Count);
        int i3 = Random.Range(0, clipChase.Count);

        zombieAudio.PlayOneShot(clipWakeUp[i]);
        yield return new WaitForSeconds(3f);

        zombieAudio.PlayOneShot(clipScream[i2]);
        animator.SetTrigger("Scream");
        yield return new WaitForSeconds(2f);

        zombieAudio.clip = clipChase[i3];
        zombieAudio.Play();
        zombieAudio.loop = true;
        animator.SetTrigger("Run");

        navMeshAgent.enabled = true;
    }

    /// <summary>
    /// Actualiza el destino del enemigo para perseguir al jugador.
    /// </summary>
    void Update()
    {
        if (navMeshAgent.enabled && player != null)
        {
            navMeshAgent.SetDestination(player.transform.position);
        }
    }

    /// <summary>
    /// Detecta colisión con el jugador y aplica daño junto con un empuje físico.
    /// </summary>
    /// <param name="collision">Colisión detectada.</param>
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("camilo"))
        {
            if (playerHealth != null)
            {
                playerHealth.health -= damage;
                playerHealth.damaged(hurt);
                playerHealth.damaged(bite);
            }

            Vector3 pushDirection = (transform.position - collision.transform.position).normalized;
            rb.AddForce(pushDirection * pushBackForce, ForceMode.Impulse);
        }
    }

    /// <summary>
    /// Aplica daño por aura mientras el jugador esté dentro del trigger.
    /// </summary>
    /// <param name="other">Collider que permanece dentro del trigger.</param>
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("camilo") && !auraCD)
        {
            if (playerHealth != null)
            {
                playerHealth.health -= aura;
                playerHealth.damaged(hurt);
                auraCD = true;
                StartCoroutine(AuraCD());
            }
        }
    }

    /// <summary>
    /// Activa el cooldown tras aplicar daño por aura.
    /// </summary>
    /// <returns>Espera por <paramref name="auraCooldownTime"/> segundos.</returns>
    private IEnumerator AuraCD()
    {
        yield return new WaitForSeconds(auraCooldownTime);
        auraCD = false;
    }
}
