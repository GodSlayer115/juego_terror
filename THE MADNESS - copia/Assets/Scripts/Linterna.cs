using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Linterna : MonoBehaviour
{
    public Light luzlinterna;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("f"))
        {
            if (luzlinterna.enabled == true)
            {
                luzlinterna.enabled = false;
            }
            else if (luzlinterna.enabled == false)
            {
                luzlinterna.enabled = true;
            }
        }
        
    }
}
