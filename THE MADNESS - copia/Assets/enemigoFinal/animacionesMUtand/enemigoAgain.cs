using UnityEngine;

public class enemigoAgain : MonoBehaviour
{
    public int rutina;
    public float cronometro;
    public Animator animator;
    public Quaternion angulo;
    public float grado;
    public GameObject target;

    public float rangoDeteccion = 10f;
    public float distanciaAtaque = 1.5f;
    public float tiempoEntreAtaques = 1.5f;
    private float temporizadorAtaque;
    private bool puedeAtacar = true;

    public float velocidadPatrulla = 1f;
    public float velocidadPersecucion = 2f;
    private bool direccionElegida = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        target = GameObject.FindWithTag("camilo");


    }

    void Update()
    {
        if (target == null) return;

        float distancia = Vector3.Distance(transform.position, target.transform.position);

        temporizadorAtaque += Time.deltaTime;
        if (temporizadorAtaque >= tiempoEntreAtaques)
        {
            puedeAtacar = true;
        }

        if (distancia <= rangoDeteccion)
        {
            if (distancia <= distanciaAtaque)
            {
                Atacar();
            }
            else
            {
                PerseguirObjetivo();
            }
        }
        else
        {
            Patrullar();
        }
    }

    void Patrullar()
    {
        cronometro += Time.deltaTime;

        if (cronometro >= 4)
        {
            rutina = Random.Range(0, 2);
            cronometro = 0;
            direccionElegida = false;
        }

        switch (rutina)
        {
            case 0:
                animator.SetBool("walk", false);
                break;

            case 1:
                if (!direccionElegida)
                {
                    grado = Random.Range(0, 360);
                    angulo = Quaternion.Euler(0, grado, 0);
                    direccionElegida = true;
                }

                transform.rotation = Quaternion.RotateTowards(transform.rotation, angulo, 120f * Time.deltaTime);
                transform.Translate(Vector3.forward * velocidadPatrulla * Time.deltaTime);
                animator.SetBool("walk", true);
                break;
        }

        animator.SetBool("run", false);
        animator.SetBool("attack", false);
    }

    void PerseguirObjetivo()
    {
        Vector3 direccion = target.transform.position - transform.position;
        direccion.y = 0;

        Quaternion rotacion = Quaternion.LookRotation(direccion);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotacion, 180f * Time.deltaTime);

        transform.Translate(Vector3.forward * velocidadPersecucion * Time.deltaTime);

        animator.SetBool("walk", false);
        animator.SetBool("run", true);
        animator.SetBool("attack", false);
    }

    void Atacar()
    {
        animator.SetBool("run", false);
        animator.SetBool("walk", false);

        Vector3 direccion = target.transform.position - transform.position;
        direccion.y = 0;
        Quaternion rotacion = Quaternion.LookRotation(direccion);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotacion, 180f * Time.deltaTime);

        if (puedeAtacar)
        {
            animator.SetTrigger("attack"); // Usa Trigger si tu animación lo requiere
            puedeAtacar = false;
            temporizadorAtaque = 0f;
        }
    }
}