using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fire : MonoBehaviour
{
    
    private GameObject player;
    [SerializeField]
    private int daño;

    void Start()
    {
        player = GameObject.FindWithTag("camilo");
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("camilo"))
        {
            player.GetComponent<Health>().health -= daño;
        }
    }
}