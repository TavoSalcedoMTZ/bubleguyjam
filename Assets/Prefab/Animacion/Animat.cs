using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding; // Importamos para acceder a AIPath

public class Animat : MonoBehaviour
{
    public Animator animator;  // Referencia al Animator
    private AIPath aiPath;  // Referencia al AIPath del agente

    private void Start()
    {
        aiPath = GetComponent<AIPath>();  // Obtenemos la referencia al componente AIPath
    }

    private void Update()
    {
        if (aiPath != null)
        {
            // Usamos la velocidad de movimiento del AIPath para actualizar el parámetro de la animación
            animator.SetFloat("movement", aiPath.velocity.magnitude);  // La magnitud de la velocidad es el valor de movimiento

            // Control de la dirección (volteo del sprite)
            if (aiPath.velocity.x < 0)  // Si la velocidad en el eje X es negativa (movimiento hacia la izquierda)
            {
                transform.localScale = new Vector3(-1, 1, 1);  // Volteamos el sprite hacia la izquierda
            }
            else if (aiPath.velocity.x > 0)  // Si la velocidad en el eje X es positiva (movimiento hacia la derecha)
            {
                transform.localScale = new Vector3(1, 1, 1);  // Volteamos el sprite hacia la derecha
            }
        }
    }
}
