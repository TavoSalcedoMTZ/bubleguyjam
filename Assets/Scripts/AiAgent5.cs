using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;
using static Spawners;

public class AiAgent5 : MonoBehaviour, IEnemigo
{
    private AIPath path;
    [SerializeField] private float moveSpeedMax;
    [SerializeField] private float moveSpeedInicial;
    [SerializeField] private Transform target;

    [SerializeField] private float stopDistanceThreshold;
    private float distanceToTarget;

    [SerializeField] private GameObject prefabToSpawn;
    private GameObject currentPrefab;

    private Enemigo enemigoScript;

    [SerializeField] private BoxCollider2D spawnArea;

    private EnemyManager enemyManager;  // Agregamos la referencia al EnemyManager

    private void Start()
    {
        path = GetComponent<AIPath>();
        enemigoScript = GetComponent<Enemigo>();
    }

    private void Update()
    {
        if (currentPrefab == null)
        {
            Seguimiento1();
        }
        else
        {
            SeguimientoPrefab();
        }
    }

    public void Seguimiento1()
    {
        path.maxSpeed = moveSpeedInicial;

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

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    public void SeguimientoPrefab()
    {
        if (currentPrefab != null)
        {
            path.destination = currentPrefab.transform.position;
            path.maxSpeed = moveSpeedMax;

            if (Vector2.Distance(transform.position, currentPrefab.transform.position) < stopDistanceThreshold)
            {
                Destroy(currentPrefab);
                currentPrefab = null;
            }
        }
    }

    public void GenerarPrefab()
    {
        if (currentPrefab == null)
        {
            Vector2 spawnPosition = GetRandomPositionInArea();
            currentPrefab = Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);
        }
    }

    private Vector2 GetRandomPositionInArea()
    {
        Vector2 areaSize = spawnArea.size;
        Vector2 areaCenter = spawnArea.bounds.center;

        float randomX = Random.Range(areaCenter.x - areaSize.x / 2, areaCenter.x + areaSize.x / 2);
        float randomY = Random.Range(areaCenter.y - areaSize.y / 2, areaCenter.y + areaSize.y / 2);

        return new Vector2(randomX, randomY);
    }

    // Implementación de la interfaz IEnemigo
    public void SetEnemyManager(EnemyManager manager)
    {
        enemyManager = manager; // Asignar el EnemyManager
    }
}
