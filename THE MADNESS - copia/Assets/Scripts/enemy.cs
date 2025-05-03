//using System.Collections;
//using System.Collections.Generic;
//using Unity.Mathematics;
//using UnityEngine;
//using Random = Unity.Mathematics.Random;


//public class enemy : MonoBehaviour
//{
//    public int rutina;
//    public float cronometro;
//    public Animator ani;
//    public quaternion angulo;
//    public float grado;
//    private void Start()
//    {
//        ani=GetComponent<Animator>();
//    }
//    public void comportamiento_enemigo()
//    {
//        cronometro += 1 * Time.deltaTime;
//        if (cronometro >= 4)
//        {
//            rutina = Random.Range(0, 2);
//            cronometro = 0;


//        }
//        switch(rutina)
//        {
//            case 0:
//                ani.SetBool("walk",false);
//                break;
//            case 1:
//                grado = Random.Range(0, 360);
//                angulo = quaternion.Euler(0, grado, 0);
//                break;
//            case 2:
//                transform.rotation = quaternion.rotateTowards(transform.rotation, angulo, 0.05f);
//                transform.Translate(Vector3.forward*1* Time.deltaTime);
//                ani.SetBool("walk",true);
//                break;

//        }

//    }




//}
