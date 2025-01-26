using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;
using static Spawners;

public class AiAgent4 : MonoBehaviour, IEnemigo
{
    [SerializeField] private Transform target;        // Objetivo al que el agente debe seguir
    [SerializeField] private float moveSpeed;        // Velocidad de movimiento
    [SerializeField] private float stopDistance;     // Distancia m�nima para detenerse

    private EnemyManager enemyManager;  // Referencia al EnemyManager

    private void Update()
    {
        if (target != null)
        {
            // Calculamos la distancia al objetivo
            float distanceToTarget = Vector2.Distance(transform.position, target.position);

            if (distanceToTarget > stopDistance)
            {
                // Si el agente no est� lo suficientemente cerca, establece el destino
                Vector2 direction = (target.position - transform.position).normalized;

                // Mover el agente hacia el objetivo
                transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
            }
        }
    }

    // Implementaci�n de la interfaz IEnemigo
    public void SetTarget(Transform newTarget)
    {
        target = newTarget; // Actualizamos el objetivo
    }

    // Implementaci�n del m�todo SetEnemyManager
    public void SetEnemyManager(EnemyManager manager)
    {
        enemyManager = manager; // Asignamos el EnemyManager al agente
    }
}
