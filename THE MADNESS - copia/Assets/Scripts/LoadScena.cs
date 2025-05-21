using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScena : MonoBehaviour
{
    // Start is called before the first frame update
    public void CambiarEscena(string nombre)
    {
        SceneManager.LoadScene(nombre);
       
    }
    private void OnTriggerEnter(Collider other)
    {
        string nombre = other.tag;
        CambiarEscena(nombre);
    }

}
