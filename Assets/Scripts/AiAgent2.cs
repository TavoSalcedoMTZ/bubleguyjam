using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;
using static Spawners;

public class AiAgent2 : MonoBehaviour, IEnemigo
{
    private AIPath path;
    [SerializeField] private float moveSpeed;
    [SerializeField] private Transform target;
    [SerializeField] private float stopDistanceThreshold;
    private float distanceToTarget;

    private bool isFollowing = false;

    // Variable para el EnemyManager
    private EnemyManager enemyManager;

    private void Start()
    {
        path = GetComponent<AIPath>();
        path.canMove = false; // Al inicio el agente no deber�a moverse hasta que entre en el �rea de activaci�n
    }

    private void Update()
    {
        if (isFollowing)
        {
            Seguimiento1();
        }
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    // Implementaci�n de SetEnemyManager
    public void SetEnemyManager(EnemyManager manager)
    {
        enemyManager = manager; // Asigna el EnemyManager
    }

    public void Seguimiento1()
    {
        path.maxSpeed = moveSpeed;

        distanceToTarget = Vector2.Distance(transform.position, target.position);
        if (distanceToTarget < stopDistanceThreshold)
        {
            path.destination = transform.position; // Detenerse si est� lo suficientemente cerca
        }
        else
        {
            path.destination = target.position; // Seguir el objetivo
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Target")) // Aseg�rate de que el tag "Target" est� asignado al objetivo
        {
            path.canMove = true; // El agente puede comenzar a moverse
            isFollowing = true; // El agente comienza a seguir
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Target"))
        {
            path.canMove = false; // El agente deja de moverse cuando sale del �rea de activaci�n
            isFollowing = false; // Deja de seguir al objetivo
        }
    }
}
