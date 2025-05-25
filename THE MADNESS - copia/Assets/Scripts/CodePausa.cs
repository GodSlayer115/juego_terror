using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CodePausa : MonoBehaviour
{
    public GameObject ObjetoMenuPausa;
    public GameObject ObjetoMenuOption;
    public bool Pausa = false;
    public GameObject objetoCamara;
    public GameObject objetoLinterna;
    public GameObject objetoCamaraAux;
    public GameObject CanvasPausa;
    public GameObject HealthBarUI;



    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;

        if (objetoCamaraAux != null)
        {
            objetoCamaraAux.SetActive(false);
        }

        if (HealthBarUI != null)
            HealthBarUI.SetActive(true);

        if (CanvasPausa != null)
        {
            CanvasPausa.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //if(Pausa == false)
            //{
            //    ObjetoMenuPausa.SetActive(true);
            //    Pausa = true;
            //    objetoCamara.SetActive(false);
            //    objetoLinterna.SetActive(false);



            //    Time.timeScale = 0;
            //    Cursor.visible = true;
            //    Cursor.lockState = CursorLockMode.None;
            //}
            //else if(Pausa == true)
            //{
            //    Resume();

            //}
            if (!Pausa)
            {
                Pausar();
            }
            else
            {
                Resume();
            }
        }
    }

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
        {
            objetoCamaraAux.SetActive(true);
        }

        Time.timeScale = 0;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        //AudioSource[] sonidos = FindObjectsOfType<AudioSource>();
        //for (int i = 0; i < sonidos.Length; i++)
        //{
        //    sonidos[i].Pause();
        //}
    }

    public void Resume()
    {
        if (CanvasPausa != null)
            CanvasPausa.SetActive(false);
        if (HealthBarUI != null)
            HealthBarUI.SetActive(true);

        //ObjetoMenuPausa.SetActive(false);
        Pausa = false;

        Time.timeScale = 1;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        objetoCamara.SetActive(true);
        objetoLinterna.SetActive(true);

        if (objetoCamaraAux != null)
        {
            objetoCamaraAux.SetActive(false);
        }

        //AudioSource[] sonidos = FindObjectsOfType<AudioSource>();
        //for (int i = 0; i < sonidos.Length; i++)
        //{
        //    sonidos[i].Play();
        //}
    }

    public void GoMenu(string NameMenu)
    {
        SceneManager.LoadScene(NameMenu);
    }

    public void GoOptions(string NameOption)
    {
        SceneManager.LoadScene(NameOption);
    }

    public void ExitGame()
    {
        Debug.Log("Saliste bro");
        Application.Quit();
    }
}
