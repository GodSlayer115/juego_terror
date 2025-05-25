    using System.Collections;
    using UnityEngine;
    using UnityEngine.AI;

    public class AiEnemigo : MonoBehaviour
    {
        public NavMeshAgent monstruo;
        public float velocidadCaminando = 1.5f;
        public float velocidadCorriendo = 4.5f;
        public float rango = 10f; // Rango de detección
        public float rangoAtaque = 2f; // Rango para atacar
        public Transform objetivo;
        public Animator animator;

        private bool persiguiendo = false;
        private bool patrullando = true;
        private bool atacando = false;
        private Coroutine patrullaRoutine;

        void Start()
        {
            patrullaRoutine = StartCoroutine(Patrullar());
            animator = GetComponent<Animator>();
        }

        void Update()
        {
            float distancia = Vector3.Distance(transform.position, objetivo.position);

            if (distancia < rango)
            {
                if (!persiguiendo)
                {
                    persiguiendo = true;
                    patrullando = false;

                    if (patrullaRoutine != null)
                        StopCoroutine(patrullaRoutine);

                    monstruo.speed = velocidadCorriendo;
                }

                monstruo.SetDestination(objetivo.position);

                // Atacar si está dentro del rango de ataque
                if (distancia <= rangoAtaque)
                {
                    if (!atacando)
                        StartCoroutine(Atacar());
                }
                else
                {
                    if (atacando)
                    {
                        // Salió del rango de ataque, cancelamos animación si es necesario
                        animator.ResetTrigger("Atacar"); // Si usas Trigger
                        
                    }
                }

            }
            else
            {
                if (persiguiendo)
                {
                    persiguiendo = false;
                    patrullando = true;

                    patrullaRoutine = StartCoroutine(Patrullar());
                }
            }

            // Actualizar animación de movimiento
            animator.SetFloat("Velocidad", monstruo.velocity.magnitude);
        }

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

    IEnumerator Atacar()
    {
        atacando = true;
        monstruo.isStopped = true;

        // Mirar al objetivo
        Vector3 direccion = (objetivo.position - transform.position).normalized;
        direccion.y = 0;
        if (direccion != Vector3.zero)
            transform.rotation = Quaternion.LookRotation(direccion);

        Debug.Log("¡Atacando al jugador!");
        animator.SetTrigger("Atacar"); // ✅ solo usa esto

        // Esperar duración de la animación
        yield return new WaitForSeconds(1.5f);

        monstruo.isStopped = false;
        atacando = false;
    }



    private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, rango);

            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, rangoAtaque);
        }
    }
