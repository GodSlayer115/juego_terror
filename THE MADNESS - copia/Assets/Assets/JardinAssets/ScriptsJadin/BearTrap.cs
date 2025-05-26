using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class BearTrap : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private PlayerMovement movement;
    private Animator animator;
    public AudioSource trapSound;
   [SerializeField] private GameObject panel;
   [SerializeField] private TrapFill trapFill;
    private bool active = false;

    void Start()
    {
        movement = player.GetComponent<PlayerMovement>();
        animator = gameObject.GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("colision con" + other.gameObject.tag);
        if (other.gameObject.CompareTag("camilo")) 
        {
            active = true;
            animator.SetTrigger("Trap");
            movement.enabled = false;
            trapSound.Play();
            panel.SetActive(true);
        }
    }

    private void FixedUpdate()
    {
        if (active && trapFill.currTrapTime >= trapFill.maxTrapTime)
        {
            trapFill.currTrapTime = 0;
            panel.SetActive(false);
            movement.enabled=true;
            Destroy(gameObject);
        }
    }
}
