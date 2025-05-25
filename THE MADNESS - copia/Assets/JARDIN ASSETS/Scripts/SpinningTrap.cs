using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningTrap : MonoBehaviour
{
    private GameObject player;
    private Health hp;
    public int damage;
    public AudioClip hurt;
    public AudioClip slash;
    private Rigidbody rb;
    public float pushBackForce = 100f;

    // Start is called before the first frame update
    void Start()
    {
      player = GameObject.FindWithTag("Player");  
      hp = player.GetComponent<Health>();
      rb = player.GetComponent<Rigidbody>();
    }
    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            
            if (hp != null)
            {
                hp.health -= damage;
                hp.damaged(hurt);
                hp.damaged(slash);
            }
            Vector3 pushDirection = (transform.position - collision.transform.position).normalized;
            rb.AddForce(pushDirection * pushBackForce, ForceMode.Impulse);
        }
    }
  
}
