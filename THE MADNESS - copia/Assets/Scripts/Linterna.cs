using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Linterna : MonoBehaviour
{
    public Light luzlinterna;
    public AudioSource audioSource;


    // Start is called before the first frame update
    void Start()
    {
        //audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("f"))
        {
            if (audioSource != null)
            {
                audioSource.Play();
            }
            //if (luzlinterna.enabled == true)
            //{
            //    luzlinterna.enabled = false;
            //}
            //else if (luzlinterna.enabled == false)
            //{
            //    luzlinterna.enabled = true;
            //}
            luzlinterna.enabled = !luzlinterna.enabled;
        }
        
    }
}
