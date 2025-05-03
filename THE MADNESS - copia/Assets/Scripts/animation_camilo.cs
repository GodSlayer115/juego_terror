using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animation_camilo : MonoBehaviour
{
    public float runSpeed;
    public float rotationSpeed;
    public Animator animator;
    public float y, x;

    // Update is called once per frame
    void Update()
    {
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");
        
        transform.Rotate(0,x*Time.deltaTime*rotationSpeed,0);
        transform.Translate(0,0,y*Time.deltaTime*runSpeed);
        animator.SetFloat("vex", x);
        animator.SetFloat("vey",y);
        
    }
}
