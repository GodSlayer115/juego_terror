using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Zones : MonoBehaviour
{
    /// <summary>
    /// Define el tipo de zona para ejecutar diferentes comportamientos: "activate", "return" o "scene".
    /// </summary>
    public string type;

    /// <summary>
    /// Referencia al script de salud del jugador para modificar su estado si es necesario.
    /// </summary>
    public Health playerHealth;

    /// <summary>
    /// Cantidad de daño que puede causar la zona (no usada en el código actual).
    /// </summary>
    public int damage;

    /// <summary>
    /// Punto de reaparición para el jugador en zonas de tipo "return".
    /// </summary>
    public Transform spawn;

    /// <summary>
    /// Prefab del enemigo que será instanciado en la zona.
    /// </summary>
    public GameObject prefabenemy;

    /// <summary>
    /// Lista de puntos donde se instanciarán los enemigos.
    /// </summary>
    public List<Transform> pointEnemy;

    /// <summary>
    /// Nombre de la escena para cargar cuando la zona sea de tipo "scene".
    /// </summary>
    public string scene;

    /// <summary>
    /// Indica si se debe activar la música al entrar en la zona.
    /// </summary>
    public bool activateMusic = false;

    /// <summary>
    /// AudioSource de la música que se activará en esta zona.
    /// </summary>
    public AudioSource music;

    /// <summary>
    /// AudioSource de la música que se está reproduciendo actualmente y debe detenerse.
    /// </summary>
    public AudioSource currMusic;

    /// <summary>
    /// Referencia al script encargado de cargar escenas con transición.
    /// </summary>
    public LoadScena LoadScena;

    /// <summary>
    /// Referencia interna al GameObject del jugador.
    /// </summary>
    private GameObject player;

    /// <summary>
    /// Inicializa las referencias necesarias al comenzar la escena.
    /// Busca al jugador por etiqueta y obtiene su script de salud.
    /// </summary>
    void Start()
    {
        player = GameObject.FindWithTag("camilo");
        playerHealth = player.GetComponent<Health>();
    }

    /// <summary>
    /// Evento que se dispara al entrar en un trigger collider.
    /// Dependiendo del tipo de zona, ejecuta diferentes acciones:
    /// - "activate": Instancia enemigos en los puntos especificados y/o cambia música.
    /// - "return": Teletransporta al jugador al punto de spawn.
    /// - "scene": Cambia a la escena indicada.
    /// </summary>
    /// <param name="other">Collider que entra en la zona</param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("camilo"))
        {
            switch (type)
            {
                case "activate":
                    // Instancia enemigos en cada punto definido
                    for (int i = 0; i < pointEnemy.Count; i++)
                    {
                        Instantiate(prefabenemy, pointEnemy[i].position, prefabenemy.transform.rotation);
                    }
                    // Cambia la música si está activado
                    if (activateMusic)
                    {
                        currMusic.Stop();
                        music.Play();
                    }
                    break;

                case "return":
                    // Teletransporta al jugador al punto de spawn
                    player.transform.position = spawn.position;
                    break;

                case "scene":
                    // Cambia la escena usando el método del LoadScena
                    LoadScena.CambiarEscena(scene);
                    break;
            }
        }
    }
}
