using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadScena : MonoBehaviour
{
    public GameObject panelCarga;
    public Slider barraCarga;

    // Start is called before the first frame update
    public void CambiarEscena(string nombre)
    {
        Debug.Log("Cargando: " + nombre);
        StartCoroutine(CargarEscena(nombre));       
    }
   
    private IEnumerator CargarEscena(string nombre)
    {
        if (panelCarga != null)
        {
            panelCarga.SetActive(true); // Muestra la pantalla de carga
        }

        // Resetear la barra antes de empezar
        if (barraCarga != null)
        {
            barraCarga.value = 0f;
        }
        Time.timeScale = 1f;
        yield return new WaitForSeconds(0.5f); // Le da una espera para mostrar el panel
        
        AsyncOperation operacion = SceneManager.LoadSceneAsync(nombre);
        operacion.allowSceneActivation = false;

        //Espera hasta que termine la carga
        while (!operacion.isDone)
        {
            float progreso = Mathf.Clamp01(operacion.progress / 0.9f); // Normalizar a 0-1
            if (barraCarga != null)
            {
                barraCarga.value = progreso;
            }

            // Cuando llegue al 90% (Unity reserva el 10% final para el "activation")
            if (operacion.progress >= 0.9f)
            {
                
                if (barraCarga != null)
                {
                    barraCarga.value = 1f;
                }

                //espera breve antes de continuar
                Debug.Log("esperando2");
                yield return new WaitForSeconds(0.2f);
                
                operacion.allowSceneActivation = true;
                break;
            }

            yield return null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        string nombre = other.tag;
        CambiarEscena(nombre);
    }

}
