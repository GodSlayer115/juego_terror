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

    private float tiempoInicio;
    private float tiempoFinal;
    private bool juegoFinalizado = false;

    /// <summary>
    /// Contador de orbes rojos recogidos por el jugador.
    /// </summary>
    public int orbesRojos;

    /// <summary>
    /// Establece la instancia única del GameManager y asegura que no se destruya al cambiar de escena.
    /// </summary>
    void Awake()
    {
        // Garantiza que solo exista una instancia del GameManager
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Mantiene el GameObject al cambiar de escena
        }
        else
        {
            Destroy(gameObject); // Destruye duplicados
        }
    }

    /// <summary>
    /// Registra el tiempo de inicio del juego.
    /// </summary>
    void Start()
    {
        tiempoInicio = Time.time;
    }

    /// <summary>
    /// Llama a este método cuando se quiera finalizar el juego.
    /// Calcula y muestra el tiempo total jugado.
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
    /// Devuelve el tiempo total jugado desde el inicio hasta la finalización del juego.
    /// </summary>
    /// <returns>Tiempo total jugado en segundos.</returns>
    public float ObtenerTiempoTotal()
    {
        return tiempoFinal - tiempoInicio;
    }

    /// <summary>
    /// Incrementa el contador de objetos recogidos según su tipo.
    /// Ejemplo: GameManager.Instance.Contador("orbesRojos");
    /// </summary>
    /// <param name="objeto">Nombre del objeto recogido (clave para el switch).</param>
    public void Contador(string objeto)
    {
        switch (objeto)
        {
            case "orbesRojos":
                {
                    orbesRojos += 1;
                    break;
                }

                // Aquí puedes agregar otros tipos de coleccionables.
                // Ejemplo:
                // case "llaves":
                //     llaves += 1;
                //     break;
        }
    }
}
