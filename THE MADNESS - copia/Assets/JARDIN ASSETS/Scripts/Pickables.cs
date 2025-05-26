using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rorb : MonoBehaviour
{
    public GameObject player;
    public Health health;
    public PlayerMovement fpsController;
    public float speedBuffTime;
    public int speedBuff;
    public float jumpBuffTime;
    public float jumpBuff;
    public AudioSource orbing;
    public AudioClip pick;
    public int heal;

    void Start()
    {
        player = GameObject.FindWithTag("camilo");
        health = player.GetComponent<Health>();
        fpsController = player.GetComponent<PlayerMovement>();
    }
   
    private void OnTriggerEnter(Collider other)
    {
       if (other.gameObject.CompareTag("camilo")) 
        {
           
            health.health += heal;
            fpsController.StartCoroutine(fpsController.jumpBuff(jumpBuffTime, jumpBuff));
            fpsController.StartCoroutine(fpsController.speedBuff(speedBuffTime, speedBuff));
            gameObject.GetComponent<Renderer>().enabled = false;
            orbing.PlayOneShot(pick);

            // Destruye el orbe después de que el sonido comience
            Destroy(gameObject, pick.length);
        }
       
    }
}
