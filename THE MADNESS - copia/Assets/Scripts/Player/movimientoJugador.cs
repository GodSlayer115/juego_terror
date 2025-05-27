using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controla el movimiento del jugador en primera persona, incluyendo caminar, correr, saltar y mirar con el ratón.
/// También permite aplicar potenciadores temporales a la velocidad y salto.
/// </summary>
[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    /// <summary>
    /// Cámara del jugador utilizada para la rotación vertical (mirar hacia arriba/abajo).
    /// </summary>
    public Camera playerCamera;

    /// <summary>
    /// Velocidad de caminata.
    /// </summary>
    public float WalkSpeed = 6f;

    /// <summary>
    /// Velocidad de carrera.
    /// </summary>
    public float runSpeed = 12f;

    /// <summary>
    /// Valor por defecto de la velocidad de caminata.
    /// </summary>
    public float defWalkSpeed = 6f;

    /// <summary>
    /// Valor por defecto de la velocidad de carrera.
    /// </summary>
    public float defRunSpeed = 12f;

    /// <summary>
    /// Fuerza del salto.
    /// </summary>
    public float jumpPower = 7f;

    /// <summary>
    /// Valor por defecto de la fuerza del salto.
    /// </summary>
    public float defJumpPower = 7f;

    /// <summary>
    /// Valor de gravedad aplicado cuando el jugador no está en el suelo.
    /// </summary>
    public float gravity = 10f;

    /// <summary>
    /// Sensibilidad de la rotación de la cámara.
    /// </summary>
    public float lookSpeed = 2f;

    /// <summary>
    /// Límite de rotación vertical (ángulo máximo hacia arriba o abajo).
    /// </summary>
    public float lookXLimit = 45f;

    private Vector3 moveDirection = Vector3.zero;
    private float rotationX = 0f;

    /// <summary>
    /// Define si el jugador puede moverse o no.
    /// </summary>
    public bool canMove = true;

    /// <summary>
    /// Controlador de movimiento del personaje, utilizado para mover al jugador mediante físicas.
    /// </summary>
    private CharacterController characterController;

    /// <summary>
    /// Entrada horizontal del jugador (eje X).
    /// </summary>
    private float x;

    /// <summary>
    /// Entrada vertical del jugador (eje Y).
    /// </summary>
    private float y;

    /// <summary>
    /// Referencia al componente Animator para controlar las animaciones del personaje.
    /// </summary>
    public Animator animator;


    /// <summary>
    /// Inicializa el componente y bloquea el cursor.
    /// </summary>
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    /// <summary>
    /// Controla el movimiento, el salto y la rotación del jugador en cada frame.
    /// </summary>
    void Update()
    {

        #region Handles Movement
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");
        animator.SetFloat("vex", x);
        animator.SetFloat("vey", y);

        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float curSpeedX = canMove ? (isRunning ? runSpeed : WalkSpeed) * x: 0;
        float curSpeedY = canMove ? (isRunning ? runSpeed : WalkSpeed) * y : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);
        #endregion

        #region Handles Jumping
        if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
        {
            moveDirection.y = jumpPower;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }

        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }
        #endregion

        #region Handles Rotation
        characterController.Move(moveDirection * Time.deltaTime);

        if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }
        #endregion
    }

    /// <summary>
    /// Aplica un aumento temporal a la velocidad de caminata y carrera.
    /// </summary>
    /// <param name="buffTime">Duración del buff en segundos.</param>
    /// <param name="buff">Cantidad de velocidad adicional a aplicar.</param>
    /// <returns>Una corrutina.</returns>
    public IEnumerator speedBuff(float buffTime, int buff)
    {
        WalkSpeed += buff;
        runSpeed += buff;
        yield return new WaitForSeconds(buffTime);
        WalkSpeed = defWalkSpeed;
        runSpeed = defRunSpeed;
    }

    /// <summary>
    /// Aplica un aumento temporal a la fuerza del salto.
    /// </summary>
    /// <param name="buffTime">Duración del buff en segundos.</param>
    /// <param name="buff">Cantidad de fuerza adicional a aplicar.</param>
    /// <returns>Una corrutina.</returns>
    public IEnumerator jumpBuff(float buffTime, float buff)
    {
        jumpPower += buff;
        yield return new WaitForSeconds(buffTime);
        jumpPower = defJumpPower;
    }
}
