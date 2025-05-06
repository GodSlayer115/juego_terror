using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    public int health = 100;
    public AudioSource deathScream;
    

    private bool isDead = false;

    private void Start()
    {
        
    }

    void Update()
    {
        if (health <= 0 && !isDead)
        {
            isDead = true; // evita múltiples ejecuciones
            
            deathScream.Play();
            StartCoroutine(DeathSequence());
        }
    }

    IEnumerator DeathSequence()
    {
        yield return new WaitForSeconds(deathScream.clip.length);
        SceneManager.LoadScene("GAMEOVER");
    }

}
