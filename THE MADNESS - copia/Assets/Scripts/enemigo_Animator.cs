using UnityEngine;
using UnityEngine.AI;

public class enemigo_Animator : MonoBehaviour
{
    public int rutina;
    public float cronometro;
    public Quaternion angulo;
    public Animator animator;
    public float grado;
    public GameObject target;
    public NavMeshAgent agente;
    public float rangoVision = 15f;
    public float distanciaAtaque = 2f;
    public float tiempoEntreAtaques = 1.5f;

    private float temporizadorAtaque;
    private bool puedeAtacar = true;

    void Start()
    {
        animator = GetComponent<Animator>();
        target = GameObject.Find("camilo");
        agente = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        comportamiento();
        temporizadorAtaque += Time.deltaTime;

        if (!puedeAtacar && temporizadorAtaque >= tiempoEntreAtaques)
        {
            puedeAtacar = true;
            agente.isStopped = false;
            animator.SetBool("attack", false); 
        }
    }

    public void comportamiento()
    {
        float distancia = Vector3.Distance(transform.position, target.transform.position);

        if (distancia < rangoVision)
        {
         
            agente.isStopped = false;
            agente.SetDestination(target.transform.position);

            if (distancia > distanciaAtaque)
            {
                animator.SetBool("walk", false);
                animator.SetBool("run", true);
                animator.SetBool("attack", false);
            }
            else
            {
               
                animator.SetBool("run", false);
                animator.SetBool("walk", false);
                agente.isStopped = true;

                if (puedeAtacar)
                {
                    animator.SetBool("attack", true);
                    puedeAtacar = false;
                    temporizadorAtaque = 0f;
                }
            }
        }
        else
        {
        
            cronometro += Time.deltaTime;

            if (cronometro >= 4)
            {
                rutina = Random.Range(0, 2);
                cronometro = 0;

                if (rutina == 1)
                {
                    grado = Random.Range(0, 360);
                    angulo = Quaternion.Euler(0, grado, 0);
                }
            }

            if (rutina == 0)
            {
                animator.SetBool("walk", false);
                agente.isStopped = true;
            }

            if (rutina == 1)
            {
                animator.SetBool("walk", true);

                Vector3 rayOrigin = transform.position + Vector3.up * 0.5f;
                Vector3 forward = transform.forward;

                bool frontBlocked = Physics.Raycast(rayOrigin, forward, 1f);
                Vector3 groundCheckPos = transform.position + forward * 0.5f + Vector3.up * 0.1f;
                bool hasGround = Physics.Raycast(groundCheckPos, Vector3.down, 1.5f);

                if (frontBlocked || !hasGround)
                {
                    Vector3 rightDir = Quaternion.Euler(0, 90, 0) * forward;
                    bool rightBlocked = Physics.Raycast(rayOrigin, rightDir, 1f);
                    bool rightGround = Physics.Raycast(transform.position + rightDir * 0.5f + Vector3.up * 0.1f, Vector3.down, 1.5f);

                    if (!rightBlocked && rightGround)
                    {
                        angulo = Quaternion.LookRotation(rightDir);
                    }
                    else
                    {
                        Vector3 leftDir = Quaternion.Euler(0, -90, 0) * forward;
                        bool leftBlocked = Physics.Raycast(rayOrigin, leftDir, 1f);
                        bool leftGround = Physics.Raycast(transform.position + leftDir * 0.5f + Vector3.up * 0.1f, Vector3.down, 1.5f);

                        if (!leftBlocked && leftGround)
                        {
                            angulo = Quaternion.LookRotation(leftDir);
                        }
                        else
                        {
                            animator.SetBool("walk", false);
                            return;
                        }
                    }
                }

                transform.rotation = Quaternion.RotateTowards(transform.rotation, angulo, 120 * Time.deltaTime);
                transform.Translate(Vector3.forward * 1.5f * Time.deltaTime);
            }

            animator.SetBool("run", false);
            animator.SetBool("attack", false); // Se asegura que esté apagado
        }
    }
}
