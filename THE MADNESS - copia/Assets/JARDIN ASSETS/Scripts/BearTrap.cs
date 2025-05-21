using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BearTrap : MonoBehaviour
{
    public GameObject player;
    public PlayerMovement movement;
    private Animator animator;
    public AudioSource trapSound;
    public float traptime;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        movement = player.GetComponent<PlayerMovement>();
        animator = gameObject.GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("colision con" + other.gameObject.tag);
        if (other.gameObject.CompareTag("Player")) 
        {
            animator.SetTrigger("Trap");
            movement.StartCoroutine(movement.trapped(traptime));
            trapSound.Play();
            StartCoroutine(destroyTrap());
        }
    }
    private IEnumerator destroyTrap()
    {
        yield return new WaitForSeconds(traptime);
        Destroy(gameObject);
    }
}
