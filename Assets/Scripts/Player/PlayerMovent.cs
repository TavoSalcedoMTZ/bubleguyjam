using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 180f;
    private Rigidbody2D rb;
    private Vector2 movement;
    private Animator playerAnimator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        // Tomar los valores de entrada para el movimiento
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        // Normalizar el vector de movimiento para evitar que el jugador se mueva más rápido en diagonal
        movement = new Vector2(moveX, moveY).normalized;

        // Llamar a las animaciones de dirección con base en las teclas presionadas
        if (Input.GetKey(KeyCode.UpArrow))
        {
           

        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {

            
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            

        }

        // Actualizar los parámetros del Animator para las animaciones
        playerAnimator.SetFloat("Horizontal", moveX);
        playerAnimator.SetFloat("Vertical", moveY);
        playerAnimator.SetFloat("Speel", movement.sqrMagnitude);
    }

    void FixedUpdate()
    {
        // Mover el personaje de acuerdo al vector de movimiento
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }


}

