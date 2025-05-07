using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Zones : MonoBehaviour
{
    public string type;
    public Health playerHealth;
    public int damage;
    public Transform spawn;
    public GameObject prefabenemy;
    public List<Transform> pointEnemy;
    public string scene;
    
    private GameObject player;
    void Start()
    {
       

        player = GameObject.FindWithTag("Player");
        playerHealth = player.GetComponent<Health>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            switch(type) 
            {
                case "activate":
                    for (int i = 0; i < pointEnemy.Count; i++)
                    {
                        Instantiate(prefabenemy, pointEnemy[i].position, prefabenemy.transform.rotation);
                    }
                    break;
                case "return":
                    player.transform.position = spawn.position;
                    break;
                case "scene":
                    SceneManager.LoadScene(scene);
                    break;
            }
        }
    }
}
