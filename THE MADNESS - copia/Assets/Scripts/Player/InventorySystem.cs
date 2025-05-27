using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Sistema de inventario básico que permite almacenar objetos recogidos en una lista.
/// Implementa el patrón Singleton para acceso global.
/// </summary>
public class InventorySystem : MonoBehaviour
{
    /// <summary>
    /// Instancia única del sistema de inventario (patrón Singleton).
    /// </summary>
    public static InventorySystem instance;

    /// <summary>
    /// Lista de objetos actualmente almacenados en el inventario.
    /// </summary>
    public List<GameObject> inventory = new List<GameObject>();

    /// <summary>
    /// Asigna la instancia Singleton o destruye el objeto si ya existe una instancia.
    /// </summary>
    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    /// <summary>
    /// Agrega un objeto al inventario.
    /// </summary>
    /// <param name="item">El objeto que se desea agregar al inventario.</param>
    public void AddItem(GameObject item)
    {
        inventory.Add(item);
        Debug.Log("Objeto guardado en inventario: " + item.name);
    }
}
