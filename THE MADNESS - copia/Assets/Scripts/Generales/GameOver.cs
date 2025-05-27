using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Comportamiento ejecutado al iniciar la escena de "Game Over".
/// Muestra el cursor del mouse, ya que normalmente se oculta durante el gameplay.
/// </summary>
public class GameOver : MonoBehaviour
{
    /// <summary>
    /// Método llamado automáticamente al iniciar la escena.
    /// Hace visible y desbloquea el cursor para permitir la interacción con menús o botones.
    /// </summary>
    void Start()
    {
        // Muestra el cursor en pantalla
        Cursor.visible = true;

        // Desbloquea el cursor (permite moverlo libremente por la pantalla)
        Cursor.lockState = CursorLockMode.None;
    }
}
