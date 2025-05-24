using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Linterna : MonoBehaviour
{
    public Light luzlinterna;
    //public AudioClip soundLinterna;
    public AudioSource soundLinterna;

    // Start is called before the first frame update
    void Start()
    {
        //audioSource = GetComponent<AudioSource>();
        //if (audioSource == null)
        //{
        //    audioSource = gameObject.AddComponent<AudioSource>();
        //}
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("f"))
        {
            if (luzlinterna.enabled == true)
            {
                luzlinterna.enabled = false;
                //audioSource.PlayOneShot(soundLinterna);
                soundLinterna.Play();
            }
            else if (luzlinterna.enabled == false)
            {
                luzlinterna.enabled = true;
                //audioSource.PlayOneShot(soundLinterna);
                soundLinterna.Play();
            }
        }
        
    }

    public void lighted(AudioClip clip)
    {
        soundLinterna.PlayOneShot(clip);
    }
}
