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

    public int heal;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        health = player.GetComponent<Health>();
        fpsController = player.GetComponent<PlayerMovement>();
    }

    private void OnTriggerEnter(Collider other)
    {
       if (other.gameObject.CompareTag("Player")) 
        {
           
            health.health += heal;
            fpsController.StartCoroutine(fpsController.jumpBuff(jumpBuffTime, jumpBuff));
            fpsController.StartCoroutine(fpsController.speedBuff(speedBuffTime, speedBuff));
            
            Destroy(gameObject); // El orbe desaparece al recogerl
        }
       
    }
}
