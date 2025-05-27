using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Cambia el color del punto de mira (crosshair) cuando el jugador apunta a un objeto recogible.
/// </summary>
public class puntoIndicador : MonoBehaviour
{
    /// <summary>
    /// Imagen del punto de mira en la UI.
    /// </summary>
    public Image crosshairImage;

    /// <summary>
    /// Color por defecto del punto de mira.
    /// </summary>
    public Color defaultColor = Color.white;

    /// <summary>
    /// Color del punto de mira cuando se apunta a un objeto recogible.
    /// </summary>
    public Color highlightColor = Color.green;

    /// <summary>
    /// Distancia máxima del rayo que detecta objetos recogibles.
    /// </summary>
    public float rayDistance = 3f;

    /// <summary>
    /// Máscara de capas para filtrar objetos recogibles.
    /// </summary>
    public LayerMask pickupLayer;

    /// <summary>
    /// Cámara del jugador usada para lanzar el rayo hacia adelante.
    /// </summary>
    public Camera playerCamera;

    /// <summary>
    /// Verifica que las referencias necesarias estén asignadas al iniciar el script.
    /// </summary>
    void Start()
    {
        if (playerCamera == null)
            Debug.LogError("¡No se ha asignado la cámara del jugador!");

        if (crosshairImage == null)
            Debug.LogError("¡No se ha asignado la imagen del punto!");
    }

    /// <summary>
    /// Lanza un rayo desde la cámara del jugador y cambia el color del punto
    /// si se detecta un objeto recogible dentro del rango.
    /// </summary>
    void Update()
    {
        if (playerCamera == null) return;

        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, rayDistance, pickupLayer))
        {
            if (hit.collider.CompareTag("Pickup"))
            {
                crosshairImage.color = highlightColor;
                return;
            }
        }

        crosshairImage.color = defaultColor;
    }
}
