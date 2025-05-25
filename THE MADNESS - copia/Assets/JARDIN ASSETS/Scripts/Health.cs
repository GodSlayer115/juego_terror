using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    public int health = 100;
    public AudioSource sounds;
    private Rigidbody[] rigRb;
    private Collider[] rigColl;
    private Rigidbody rbPlayer;
    private PlayerMovement PlM;
    private Animator animator;
    private bool isDead = false;

    private void Start()
    {
        rbPlayer = GetComponent<Rigidbody>();
        rigColl = GetComponentsInChildren<Collider>();
        rigRb = GetComponentsInChildren<Rigidbody>();
        PlM = GetComponent<PlayerMovement>();
        animator = GetComponent<Animator>();
        
        rbPlayer.isKinematic = false;
    }

    void Update()
    {
        if(health> 100) {health = 100;}
        if (health <= 0 && !isDead)
        {
            isDead = true; // evita múltiples ejecuciones
            PlM.enabled = false;
            animator.enabled = false;
           
            sounds.Play();
            StartCoroutine(DeathSequence());
        }
    }

    IEnumerator DeathSequence()
    {
        yield return new WaitForSeconds(sounds.clip.length);
        SceneManager.LoadScene("GAMEOVER");
    }

    public void damaged(AudioClip clip) 
    {
        sounds.PlayOneShot(clip);
    } 
}
