using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadScena : MonoBehaviour
{
    [SerializeField] private Slider loadbar;
    [SerializeField] private GameObject loadPanel;

    public void IniciarJuego(string nombre)
    {
        loadPanel.SetActive(true);
        StartCoroutine(CambiarEscena(nombre));
    }


    private void OnTriggerEnter(Collider other)
    {
        string sceneName = other.tag;
        loadPanel.SetActive(true);
        StartCoroutine(CambiarEscena(sceneName));
    }

    IEnumerator CambiarEscena(string nombre)
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(nombre);

        while (!asyncOperation.isDone)
        {
            loadbar.value = asyncOperation.progress / 0.9f;
            yield return null;
        }
    }

}
