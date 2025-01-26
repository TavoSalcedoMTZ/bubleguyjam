using UnityEngine;
using Pathfinding;
using static Spawners;

public class AiAgent : MonoBehaviour, IEnemigo
{
    private AIPath path;
    [SerializeField] private float moveSpeed;
    [SerializeField] private Transform target;
    [SerializeField] private EnemyManager enemyManager;
    [SerializeField] private float stopDistanceThreshold;
    private float distanceToTarget;

    private void Start()
    {
        path = GetComponent<AIPath>();
    }

    private void Update()
    {
        Seguimiento1();
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    // Implementación de SetEnemyManager que es parte de la interfaz IEnemigo
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
            path.destination = transform.position;
        }
        else
        {
            path.destination = target.position;
        }
    }
}
