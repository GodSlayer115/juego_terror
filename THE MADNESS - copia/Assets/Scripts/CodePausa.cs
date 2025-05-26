using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Controla el sistema de pausa del juego: pausa, reanuda, cambia de escena y muestra menús.
/// </summary>
public class CodePausa : MonoBehaviour
{
    /// <summary>Menú principal de pausa.</summary>
    public GameObject ObjetoMenuPausa;

    /// <summary>Menú de opciones dentro del menú de pausa.</summary>
    public GameObject ObjetoMenuOption;

    /// <summary>Indica si el juego está en pausa.</summary>
    public bool Pausa = false;

    /// <summary>Cámara activa en juego normal.</summary>
    public GameObject objetoCamara;

    /// <summary>Objeto de la linterna del jugador.</summary>
    public GameObject objetoLinterna;

    /// <summary>Cámara alternativa para la pausa (por ejemplo, una cámara fija del menú).</summary>
    public GameObject objetoCamaraAux;

    /// <summary>UI del canvas cuando el juego está pausado.</summary>
    public GameObject CanvasPausa;

    /// <summary>UI de la barra de salud del jugador.</summary>
    public GameObject HealthBarUI;

    /// <summary>Inicializa el estado de los objetos de UI y las cámaras.</summary>
    void Start()
    {
        Time.timeScale = 1f;

        if (objetoCamaraAux != null)
            objetoCamaraAux.SetActive(false);

        if (HealthBarUI != null)
            HealthBarUI.SetActive(true);

        if (CanvasPausa != null)
            CanvasPausa.SetActive(false);
    }

    /// <summary>Escucha la tecla ESC para activar o desactivar la pausa.</summary>
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!Pausa)
                Pausar();
            else
                Resume();
        }
    }

    /// <summary>Activa el estado de pausa y muestra el menú correspondiente.</summary>
    public void Pausar()
    {
        ObjetoMenuPausa.SetActive(true);
        ObjetoMenuOption.SetActive(false);
        Pausa = true;

        if (CanvasPausa != null)
            CanvasPausa.SetActive(true);
        if (HealthBarUI != null)
            HealthBarUI.SetActive(false);

        objetoCamara.SetActive(false);
        objetoLinterna.SetActive(false);

        if (objetoCamaraAux != null)
            objetoCamaraAux.SetActive(true);

        Time.timeScale = 0;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    /// <summary>Reanuda el juego desde el estado de pausa.</summary>
    public void Resume()
    {
        if (CanvasPausa != null)
            CanvasPausa.SetActive(false);
        if (HealthBarUI != null)
            HealthBarUI.SetActive(true);

        ObjetoMenuPausa.SetActive(false);
        Pausa = false;

        Time.timeScale = 1;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        objetoCamara.SetActive(true);
        objetoLinterna.SetActive(true);

        if (objetoCamaraAux != null)
            objetoCamaraAux.SetActive(false);
    }

    /// <summary>Cambia a otra escena con nombre específico (como el menú principal).</summary>
    /// <param name="NameMenu">Nombre de la escena a cargar.</param>
    public void GoMenu(string NameMenu)
    {
        SceneManager.LoadScene(NameMenu);
    }

    /// <summary>Cambia a la escena de opciones (no usada directamente desde pausa en este script).</summary>
    /// <param name="NameOption">Nombre de la escena de opciones.</param>
    public void GoOptions(string NameOption)
    {
        SceneManager.LoadScene(NameOption);
    }

    /// <summary>Cierra el juego y muestra mensaje en consola si está en editor.</summary>
    public void ExitGame()
    {
        Debug.Log("Saliste bro");
        Application.Quit();
    }
}
