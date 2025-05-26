using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningTrap : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private Health hp;
    public int damage;
    public AudioClip hurt;
    public AudioClip slash;
    private Rigidbody rb;
    public float pushBackForce = 100f;

    // Start is called before the first frame update
    void Start()
    {
     
      hp = player.GetComponent<Health>();
      
    }
    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("camilo"))
        {
            
            if (hp != null)
            {
                hp.health -= damage;
                hp.damaged(hurt);
                hp.damaged(slash);
            }
            
        }
    }
  
}
