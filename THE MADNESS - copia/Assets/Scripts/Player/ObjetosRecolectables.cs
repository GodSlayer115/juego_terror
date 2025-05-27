using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Gestiona la recolección, manipulación y almacenamiento de objetos recogibles por el jugador.
/// </summary>
public class ObjetosRecolectables : MonoBehaviour
{
    /// <summary>
    /// Cámara del jugador utilizada para lanzar rayos y detectar objetos recogibles.
    /// </summary>
    public Camera playerCamera;

    /// <summary>
    /// Distancia máxima a la que el jugador puede recoger un objeto.
    /// </summary>
    public float pickupRange = 3f;

    /// <summary>
    /// Velocidad con la que el objeto recogido se mueve hacia la posición de sujeción.
    /// </summary>
    public float moveSpeed = 5f;

    /// <summary>
    /// Posición frente al jugador donde se mantendrá el objeto recogido.
    /// </summary>
    public Transform holdPosition;

    /// <summary>
    /// Referencia al objeto actualmente recogido.
    /// </summary>
    private GameObject heldObject;

    /// <summary>
    /// Indica si el jugador está actualmente sosteniendo un objeto.
    /// </summary>
    private bool isHolding = false;

    /// <summary>
    /// Referencia al controlador de movimiento del jugador.
    /// </summary>
    private PlayerMovement fpsController;

    /// <summary>
    /// Lista de nombres de los objetos que el jugador ha recolectado.
    /// </summary>
    private List<string> collectedItems = new List<string>();

    /// <summary>
    /// Inicializa referencias necesarias al iniciar el juego.
    /// </summary>
    void Start()
    {
        fpsController = GetComponent<PlayerMovement>();
    }

    /// <summary>
    /// Verifica las entradas del jugador en cada fotograma para recoger, mover o almacenar objetos.
    /// </summary>
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !isHolding)
            TryPickup();

        if (isHolding)
        {
            MoveObjectToCamera();
            RotateHeldObject();

            if (Input.GetKeyDown(KeyCode.F))
                StoreObject();
        }
    }

    /// <summary>
    /// Intenta recoger un objeto frente al jugador si tiene la etiqueta "Pickup".
    /// </summary>
    void TryPickup()
    {
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, pickupRange))
        {
            if (hit.collider.CompareTag("Pickup"))
            {
                heldObject = hit.collider.gameObject;
                heldObject.GetComponent<Rigidbody>().isKinematic = true;
                isHolding = true;
                fpsController.canMove = false;
            }
        }
    }

    /// <summary>
    /// Mueve el objeto recogido suavemente hacia la posición de sujeción frente al jugador.
    /// </summary>
    void MoveObjectToCamera()
    {
        heldObject.transform.position = Vector3.Lerp(heldObject.transform.position, holdPosition.position, Time.deltaTime * moveSpeed);
    }

    /// <summary>
    /// Permite rotar el objeto recogido usando las teclas de movimiento del jugador.
    /// </summary>
    void RotateHeldObject()
    {
        float h = Input.GetAxis("Horizontal") * 100f * Time.deltaTime;
        float v = Input.GetAxis("Vertical") * 100f * Time.deltaTime;

        heldObject.transform.Rotate(playerCamera.transform.up, h, Space.World);
        heldObject.transform.Rotate(playerCamera.transform.right, -v, Space.World);
    }

    /// <summary>
    /// Almacena el objeto recogido, lo desactiva y permite al jugador volver a moverse.
    /// </summary>
    void StoreObject()
    {
        string itemName = heldObject.name;
        if (!collectedItems.Contains(itemName))
        {
            collectedItems.Add(itemName);
        }

        heldObject.SetActive(false);
        heldObject = null;
        isHolding = false;
        fpsController.canMove = true;
    }

    /// <summary>
    /// Verifica si el jugador ha recolectado un objeto específico.
    /// </summary>
    /// <param name="itemName">Nombre del objeto a verificar.</param>
    /// <returns>True si el objeto está en la lista de recolectados; de lo contrario, false.</returns>
    public bool HasItem(string itemName)
    {
        return collectedItems.Contains(itemName);
    }

    /// <summary>
    /// Verifica si el jugador ha recolectado todos los objetos de una lista.
    /// </summary>
    /// <param name="itemNames">Lista de nombres de objetos requeridos.</param>
    /// <returns>True si todos los objetos están en la lista de recolectados; de lo contrario, false.</returns>
    public bool HasAllItems(List<string> itemNames)
    {
        foreach (string item in itemNames)
        {
            if (!collectedItems.Contains(item)) return false;
        }
        return true;
    }
}
