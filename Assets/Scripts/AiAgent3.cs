using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;
using static Spawners;

public class AiAgent3 : MonoBehaviour, IEnemigo
{
    private AIPath path;
    [SerializeField] private float moveSpeed;
    [SerializeField] private Transform target;
    [SerializeField] private Transform posicion1;
    [SerializeField] private Transform posicion2;

    [SerializeField] private float stopDistanceThreshold;
    private float distanceToTarget;

    private bool isFollowing = false;
    private bool movingBetweenPositions = true;

    private EnemyManager enemyManager;

    private void Start()
    {
        path = GetComponent<AIPath>();
        path.destination = posicion1.position;  // Inicia en la primera posición
        path.canMove = true;
    }

    private void Update()
    {
        if (isFollowing)
        {
            Seguimiento1(); // Sigue al objetivo
        }
        else if (movingBetweenPositions)
        {
            MovimientoEntrePosiciones(); // Mueve entre las posiciones predeterminadas
        }
    }

    private void MovimientoEntrePosiciones()
    {
        path.maxSpeed = moveSpeed;

        distanceToTarget = Vector2.Distance(transform.position, path.destination);
        if (distanceToTarget < stopDistanceThreshold)
        {
            // Cambiar la posición objetivo entre posicion1 y posicion2
            path.destination = (path.destination == posicion1.position) ? posicion2.position : posicion1.position;
        }
    }

    public void Seguimiento1()
    {
        path.maxSpeed = moveSpeed;

        distanceToTarget = Vector2.Distance(transform.position, target.position);
        if (distanceToTarget < stopDistanceThreshold)
        {
            path.destination = transform.position; // Detenerse si está lo suficientemente cerca
        }
        else
        {
            path.destination = target.position; // Seguir al objetivo
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Target"))
        {
            path.canMove = true; // Activar movimiento
            isFollowing = true; // Comienza a seguir al objetivo
            movingBetweenPositions = false; // Deja de moverse entre posiciones
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Target"))
        {
            isFollowing = false; // Deja de seguir al objetivo
            movingBetweenPositions = true; // Vuelve a moverse entre posiciones
            path.canMove = true; // Activar movimiento
            path.destination = posicion1.position; // Regresar a la primera posición
        }
    }

    // Implementación de la interfaz IEnemigo
    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    // Implementación de SetEnemyManager
    public void SetEnemyManager(EnemyManager manager)
    {
        enemyManager = manager; // Asignar el EnemyManager
    }
}
