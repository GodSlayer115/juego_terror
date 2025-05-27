using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Controla el sistema de salud del jugador.
/// Activa el modo ragdoll al morir y reproduce sonido.
/// </summary>
public class Health : MonoBehaviour
{
    /// <summary>
    /// Salud actual del jugador.
    /// </summary>
    public int health = 100;

    /// <summary>
    /// Fuente de audio para reproducir sonidos de daño o muerte.
    /// </summary>
    public AudioSource sounds;

    /// <summary>
    /// Rigidbodies del cuerpo del jugador usados para el ragdoll.
    /// </summary>
    private Rigidbody[] rigRb;

    /// <summary>
    /// Colliders del cuerpo del jugador usados para el ragdoll.
    /// </summary>
    private Collider[] rigColl;

    /// <summary>
    /// Componente de movimiento del jugador.
    /// </summary>
    private PlayerMovement PlM;

    /// <summary>
    /// Componente de animación del jugador.
    /// </summary>
    private Animator animator;

    /// <summary>
    /// Indica si el jugador ya ha muerto.
    /// </summary>
    private bool isDead = false;

    /// <summary>
    /// Referencia a la cabeza del ragdoll, usada para posicionar la cámara tras la muerte.
    /// </summary>
    public Transform ragdollHead;

    /// <summary>
    /// Inicializa referencias y desactiva el ragdoll.
    /// </summary>
    private void Start()
    {
        rigColl = GetComponentsInChildren<Collider>();
        rigRb = GetComponentsInChildren<Rigidbody>();
        PlM = GetComponent<PlayerMovement>();
        animator = GetComponent<Animator>();
        SetRagdollState(false);
    }

    /// <summary>
    /// Activa o desactiva el estado ragdoll del personaje.
    /// </summary>
    /// <param name="state">True para activar ragdoll, false para desactivarlo.</param>
    public void SetRagdollState(bool state)
    {
        foreach (Rigidbody rb in rigRb)
        {
            rb.isKinematic = !state;
            rb.GetComponent<Collider>().enabled = state;
        }

        if (animator != null)
            animator.enabled = !state;
    }

    /// <summary>
    /// Revisa constantemente si el jugador ha muerto para activar el ragdoll y la secuencia de muerte.
    /// </summary>
    private void Update()
    {
        if (health > 100) health = 100;

        if (health <= 0 && !isDead)
        {
            isDead = true;
            PlM.enabled = false;
            animator.enabled = false;
            SetRagdollState(true);

            // Reubica la cámara en la cabeza del ragdoll
            Camera.main.transform.SetParent(ragdollHead);
            Camera.main.transform.localPosition = Vector3.zero;
            Camera.main.transform.localRotation = Quaternion.identity;

            sounds.Play();
            StartCoroutine(DeathSequence());
        }
    }

    /// <summary>
    /// Ejecuta la secuencia final tras la muerte y cambia a la escena de "Game Over".
    /// </summary>
    private IEnumerator DeathSequence()
    {
        yield return new WaitForSeconds(sounds.clip.length);
        SceneManager.LoadScene("GAMEOVER");
    }

    /// <summary>
    /// Reproduce un sonido de daño si el jugador no está muerto.
    /// </summary>
    /// <param name="clip">Clip de sonido a reproducir.</param>
    public void damaged(AudioClip clip)
    {
        if (isDead) return;
        sounds.PlayOneShot(clip);
    }
}
