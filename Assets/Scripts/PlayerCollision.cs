using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public MaskTransition maskTransition; // Referencia al script de la transici�n de m�scara
    public BackgroundManager backgroundManager; // Referencia al BackgroundManager
    public PlayerMovement playerMovement;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Roca"))
        {
            // Detener el movimiento del jugador
            if (playerMovement != null)
            {
                //
            }
            else
            {
                Debug.LogError("PlayerMovement no est� asignado en PlayerCollision.");
            }
        }
    }
}
