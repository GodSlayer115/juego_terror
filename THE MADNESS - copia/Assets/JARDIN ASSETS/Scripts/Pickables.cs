using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rorb : MonoBehaviour
{
    public GameObject player;
    public Health health;
    public FPSController fpsController;
    public float speedBuffTime;
    public int speedBuff;
    public float jumpBuffTime;
    public float jumpBuff;
    public string type;
    public int heal;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        health = player.GetComponent<Health>();
        fpsController = player.GetComponent<FPSController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collision con " + type);
        if (other.gameObject.CompareTag("Player"))
        {
            switch (type)
            {
                case "RedOrb":
                    health.health += heal;
                    
                    break;

                case "JumpOrb":
                    fpsController.StartCoroutine(fpsController.jumpBuff(jumpBuffTime, jumpBuff));
                    break;

                case "SpeedOrb":
                    fpsController.StartCoroutine(fpsController.speedBuff(speedBuffTime, speedBuff));
                    break;

                default:
                    Debug.LogWarning("Tipo de orbe desconocido: " + type);
                    break;
            }

            Destroy(gameObject); // El orbe desaparece al recogerlo
        }
       
    }
}
