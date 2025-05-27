using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EfectoShakify : MonoBehaviour
{
    public Transform camaraTransform; // Normalmente es la misma que este objeto
    public CharacterController controladorJugador; // Referencia al personaje

    [Header("Umbrales de velocidad")]
    public float umbralCaminar = 0.1f;
    public float umbralCorrer = 3.0f;

    [Header("Movimiento - Caminata")]
    public float frecuenciaCaminar = 1.5f;
    public float amplitudCaminar = 0.05f;

    [Header("Movimiento - Trote")]
    public float frecuenciaCorrer = 5.0f;
    public float amplitudCorrer = 0.12f;

    private Vector3 posicionInicial;
    private float tiempo;

    // Start is called before the first frame update
    void Start()
    {
        if (camaraTransform == null)
            camaraTransform = transform;

        posicionInicial = camaraTransform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 0) return;

        float velocidad = controladorJugador.velocity.magnitude;

        if (velocidad > umbralCaminar)
        {
            // Selecciona valores dinámicamente según la velocidad
            float frecuencia = velocidad > umbralCorrer ? frecuenciaCorrer : frecuenciaCaminar;
            float amplitud = velocidad > umbralCorrer ? amplitudCorrer : amplitudCaminar;

            tiempo += Time.deltaTime * frecuencia;

            float desplazamientoY = Mathf.Sin(tiempo * 2) * amplitud;
            float desplazamientoX = Mathf.Sin(tiempo) * amplitud * 0.5f;

            camaraTransform.localPosition = posicionInicial + new Vector3(desplazamientoX, desplazamientoY, 0);
        }
        else
        {
            camaraTransform.localPosition = Vector3.Lerp(camaraTransform.localPosition, posicionInicial, Time.deltaTime * 5f);
            tiempo = 0;
        }
    }
}
