using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Ai_enemigo : MonoBehaviour
{
    public Transform objetivo;
    public float velocidad;
    public NavMeshAgent IA;
    public Animation anim;
    public bool persiguiendo;
    
  
    public float rango;
    float distancia;
    public string caminar;
    public string atacar;


    private void Update()
    {
        distancia= Vector3.Distance(IA.transform.position, objetivo.position);
        if (distancia <= rango)
        {
            persiguiendo = true; // estás dentro del rango → te persigue
        }
        else
        {
            persiguiendo = false; // estás fuera del rango → se detiene
        }

        if (persiguiendo == false)
        {
            IA.speed = 0;
            anim.CrossFade(atacar);
        }
        else if (persiguiendo == true)
        {
            IA.speed = velocidad;
            anim.CrossFade(caminar);
            IA.SetDestination(objetivo.position);
        }
     
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, rango);
    }
}
