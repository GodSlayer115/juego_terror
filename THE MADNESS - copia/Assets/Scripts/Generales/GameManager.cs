using System.IO;
using UnityEngine;

/// <summary>
/// GameManager controla el estado global del juego, incluyendo el tiempo transcurrido,
/// colección de objetos y persistencia entre escenas.
/// </summary>
public class GameManager : MonoBehaviour
{
    /// <summary>
    /// Instancia única (singleton) del GameManager.
    /// </summary>
    public static GameManager Instance;

    /// <summary>
    /// Ruta del archivo de guardado en disco.
    /// </summary>
    private string saveFilePath;

    /// <summary>
    /// Tiempo en que el juego comenzó o se cargó.
    /// </summary>
    private float tiempoInicio;

    /// <summary>
    /// Tiempo en que el juego terminó.
    /// </summary>
    private float tiempoFinal;

    /// <summary>
    /// Indica si el juego ha sido finalizado.
    /// </summary>
    private bool juegoFinalizado = false;

    /// <summary>
    /// Contador de orbes rojos recogidos por el jugador.
    /// </summary>
    public int orbesRojos;

    /// <summary>
    /// Inicializa la instancia del GameManager y establece la ruta del archivo de guardado.
    /// Se asegura de que el objeto no se destruya al cambiar de escena.
    /// </summary>
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        saveFilePath = Path.Combine(Application.persistentDataPath, "savefile.json");
    }

    /// <summary>
    /// Finaliza el juego y calcula el tiempo total jugado.
    /// Solo puede llamarse una vez.
    /// </summary>
    public void FinalizarJuego()
    {
        if (!juegoFinalizado)
        {
            tiempoFinal = Time.time;
            juegoFinalizado = true;

            float tiempoTotal = tiempoFinal - tiempoInicio;
            Debug.Log("Tiempo total de juego: " + tiempoTotal.ToString("F2") + " segundos");
        }
    }

    /// <summary>
    /// Obtiene el tiempo total jugado desde el inicio hasta la finalización del juego.
    /// </summary>
    /// <returns>Tiempo total jugado en segundos.</returns>
    public float ObtenerTiempoTotal()
    {
        return tiempoFinal - tiempoInicio;
    }

    /// <summary>
    /// Incrementa el contador de objetos recogidos según su tipo.
    /// </summary>
    /// <param name="objeto">Nombre del objeto recogido (por ejemplo, "orbesRojos").</param>
    public void Contador(string objeto)
    {
        switch (objeto)
        {
            case "orbesRojos":
                orbesRojos += 1;
                break;

                // Puedes agregar más casos aquí.
        }
    }

    /// <summary>
    /// Guarda los datos del juego actual en un archivo JSON.
    /// Incluye el tiempo jugado, la cantidad de orbes recogidos y la salud del jugador.
    /// </summary>
    public void SaveGame()
    {
        GameObject jugador = GameObject.FindWithTag("camilo");
        if (jugador == null)
        {
            Debug.LogWarning("No se encontró el GameObject con tag 'Jugador'. No se guardará el juego.");
            return;
        }

        SaveData data = new SaveData
        {
            tiempo = Time.time - tiempoInicio,
            contOrb = this.orbesRojos,
            playerhp = jugador.GetComponent<Health>().health
        };

        string json = JsonUtility.ToJson(data, true);
        Debug.Log("Contenido del JSON: " + json);
        File.WriteAllText(saveFilePath, json);
        Debug.Log("Juego guardado en: " + saveFilePath);
    }

    /// <summary>
    /// Carga los datos del juego desde un archivo JSON si existe.
    /// Restaura el estado del juego incluyendo tiempo, orbes y salud del jugador.
    /// </summary>
    public void LoadGame()
    {
        if (File.Exists(saveFilePath))
        {
            string json = File.ReadAllText(saveFilePath);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            Debug.Log("Juego cargado desde: " + saveFilePath);

            if (data != null)
            {
                this.orbesRojos = data.contOrb;
                GameObject.FindWithTag("camilo").GetComponent<Health>().health = data.playerhp;
                tiempoInicio = Time.time - data.tiempo;
                tiempoFinal = Time.time; // Opcional, reiniciar final
            }

            Debug.Log(data.contOrb);
            Debug.Log(data.playerhp);
            Debug.Log(data.tiempo);
        }
        else
        {
            Debug.LogWarning("No se encontró el archivo de guardado.");
        }
    }

    /// <summary>
    /// Elimina el archivo de guardado del juego si existe.
    /// </summary>
    public void DeleteSave()
    {
        if (File.Exists(saveFilePath))
        {
            File.Delete(saveFilePath);
            Debug.Log("Archivo de guardado eliminado.");
        }
    }

    /// <summary>
    /// Actualiza el tiempo jugado cada frame (para depuración o interfaz si se desea).
    /// </summary>
    void Update()
    {
        float tiempoJugado = Time.time - tiempoInicio;
        // Puedes usar tiempoJugado para mostrar en UI si lo necesitas.
    }
}
