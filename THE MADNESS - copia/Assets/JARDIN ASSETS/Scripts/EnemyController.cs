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
   
    private GameObject player;
    private Health playerHealth;
    private Rigidbody rb;
    private NavMeshAgent navMeshAgent;
    private Animator animator;
    private bool auraCD = false;
    public float auraCooldownTime;

    public AudioClip hurt;
    public AudioClip bite;
    public List<AudioClip> clipWakeUp;
    public List<AudioClip> clipScream;
    public List<AudioClip> clipChase;
    public AudioSource zombieAudio;

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
        int i = Random.Range(0, clipWakeUp.Count);
        int i2 = Random.Range(0, clipScream.Count);
        int i3 = Random.Range(0, clipChase.Count);
        zombieAudio.PlayOneShot(clipWakeUp[i]);
        yield return new WaitForSeconds(3f); // Ajusta según la duración real de la animación

        zombieAudio.PlayOneShot(clipScream[i2]);
        // Animación de grito
        animator.SetTrigger("Scream");
        yield return new WaitForSeconds(2f); // Ajusta según duración
        zombieAudio.clip = clipChase[i3];
        zombieAudio.Play();
        zombieAudio.loop = true;
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
            zombieAudio.PlayOneShot(bite);
            if (playerHealth != null)
            {
                playerHealth.health -= damage;
                playerHealth.damaged(hurt);
                
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
                playerHealth.damaged(hurt);
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