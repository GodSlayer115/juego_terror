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

    private PlayerMovement PlM;
    private Animator animator;
    private bool isDead = false;
    public Transform ragdollHead;

    private void Start()
    {
        
        rigColl = GetComponentsInChildren<Collider>();
        rigRb = GetComponentsInChildren<Rigidbody>();
        PlM = GetComponent<PlayerMovement>();
        animator = GetComponent<Animator>();
        SetRagdollState(false);
       
    }

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
    void Update()
    {
        if(health> 100) {health = 100;}
        if (health <= 0 && !isDead)
        {
            isDead = true; // evita múltiples ejecuciones
            PlM.enabled = false;
            animator.enabled = false;
            SetRagdollState(true);
            Camera.main.transform.SetParent(ragdollHead);
            Camera.main.transform.localPosition = Vector3.zero;
            Camera.main.transform.localRotation = Quaternion.identity;
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
        if(isDead) return;
        sounds.PlayOneShot(clip);
    } 
}
