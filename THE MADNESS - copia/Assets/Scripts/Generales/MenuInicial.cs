using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Controla las acciones del menú inicial del juego, como iniciar la partida o salir de la aplicación.
/// </summary>
public class MenuInicial : MonoBehaviour
{
    /// <summary>
    /// Inicia el juego cargando la siguiente escena en el índice de construcción.
    /// </summary>
    public void Jugar()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    /// <summary>
    /// Cierra la aplicación. Si se está en el editor, muestra un mensaje en la consola.
    /// </summary>
    public void Salir()
    {
        Debug.Log("Saliste bro");
        Application.Quit();
    }
}
