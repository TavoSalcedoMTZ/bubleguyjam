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
    [SerializeField] private Collider2D collider2D;
    [SerializeField] private JabonManage jabonManage;

    private bool isFollowing = false;

    // Variable para el EnemyManager
    private EnemyManager enemyManager;

    // Referencia a un script para manejar el da�o (esto puede ser cualquier script que maneje la salud o da�o)
    [SerializeField] private JabonManage jabon;

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

    public void SetJabonManage(JabonManage jabon)
    {
        jabonManage = jabon;
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

    private void OnCollisionEnter2D(Collision2D other)
    {
        AplicarDanio(1); 
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Target"))
        {
            path.canMove = false; // El agente deja de moverse cuando sale del �rea de activaci�n
            isFollowing = false; // Deja de seguir al objetivo
        }
    }

    // Funci�n para aplicar da�o al enemigo
    private void AplicarDanio(int cantidadDanio)
    {
        if (jabon != null)
        {
            jabon.JabonDicrese(cantidadDanio); // Llamamos a la funci�n que recibe el da�o en el script de salud
        }
        else
        {
            Debug.LogError("El objeto enemigo no tiene un script de salud asignado.");
        }
    }
}
