using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public float speed = 3f;
    public int damage = 10;
    public int aura = 5;
    public float pushBackForce = 5f;
    public AudioSource hurtScream;
    private GameObject player;
    private Health playerHealth;
    private Rigidbody rb;
    private NavMeshAgent navMeshAgent;
    private Animator animator;
    private bool auraCD = false;
    public float auraCooldownTime;

    void Start()
    {
        player = GameObject.FindWithTag("Player"); // Asegúrate de que el jugador tenga el tag "Player"
        if (player != null)
        {
            playerHealth = player.GetComponent<Health>();
        }
        rb = GetComponent<Rigidbody>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        
        animator = GetComponent<Animator>();

        navMeshAgent.speed = speed;
        navMeshAgent.enabled = false; // Desactivar al inicio

        StartCoroutine(StartSequence());
    }
    IEnumerator StartSequence()
    {
        
        yield return new WaitForSeconds(5f); // Ajusta según la duración real de la animación

        // Animación de grito
        animator.SetTrigger("Scream");
        yield return new WaitForSeconds(4f); // Ajusta según duración

        animator.SetTrigger("Run");
        // Activar NavMeshAgent
        navMeshAgent.enabled = true;
    }


    void Update()
    {
        if (navMeshAgent.enabled && player != null)
        {
            navMeshAgent.SetDestination(player.transform.position);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (playerHealth != null)
            {
                playerHealth.health -= damage;
                hurtScream.Play();
            }
            Vector3 pushDirection = (transform.position - collision.transform.position).normalized;
            rb.AddForce(pushDirection * pushBackForce, ForceMode.Impulse);
        }
    }

    private void OnTriggerStay(Collider other)
    {

        if (other.gameObject.CompareTag("Player") && !auraCD)
        {
            if (playerHealth != null)
            {
                playerHealth.health -= aura; // Aplica el daño de aura
                hurtScream.Play();
                auraCD = true;
                StartCoroutine(AuraCD());
            }
        }
    }

    private IEnumerator AuraCD()
    {
        yield return new WaitForSeconds(auraCooldownTime);
        auraCD = false;
    }
}