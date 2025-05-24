using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class BearTrap : MonoBehaviour
{
    public GameObject player;
    public PlayerMovement movement;
    private Animator animator;
    public AudioSource trapSound;
    public float traptime;
    public GameObject panel;
   [SerializeField] private TrapFill trapFill;

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
            movement.enabled = false;
            trapSound.Play();
            panel.SetActive(true);
        }
    }

    private void Update()
    {
        if (trapFill.currTrapTime >= trapFill.maxTrapTime)
        {
            panel.SetActive(false);
            movement.enabled=true;
            Destroy(gameObject);
        }
    }
}
