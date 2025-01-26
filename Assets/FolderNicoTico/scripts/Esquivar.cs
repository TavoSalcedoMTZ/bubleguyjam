using System.Collections;
using UnityEngine;
using Pathfinding;

public class Esquivar : MonoBehaviour
{
    private AIPath path;
    [SerializeField] private GameObject prefabToSpawn; // Prefab a generar al recibir daño
    private GameObject currentPrefab;

    [SerializeField] private float moveSpeedMax; // Velocidad de esquive al seguir el prefab
    [SerializeField] private float moveSpeedInicial; // Velocidad inicial del enemigo

    [SerializeField] private BoxCollider2D spawnArea; // Área donde el prefab puede ser generado
    [SerializeField] private float stopDistanceThreshold = 1f; // Distancia para considerar que llegó al prefab

    private Transform target; // El objetivo al que el enemigo seguirá

    private void Start()
    {
        path = GetComponent<AIPath>();
        target = null; // Inicialmente no hay target
    }

    private void Update()
    {
        if (currentPrefab == null)
        {
            // Si no hay prefab, seguimos al target normal (si existe)
            if (target != null)
            {
                SeguimientoNormal();
            }
        }
        else
        {
            // Si hay un prefab, seguimos ese prefab
            SeguimientoPrefab();
        }
    }

    private void SeguimientoNormal()
    {
        path.maxSpeed = moveSpeedInicial;

        if (target != null)
        {
            path.destination = target.position;
        }
    }

    private void SeguimientoPrefab()
    {
        if (currentPrefab != null)
        {
            path.maxSpeed = moveSpeedMax; // Aumenta la velocidad para esquivar

            path.destination = currentPrefab.transform.position;

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

    public void SetTarget(Transform newTarget)
    {
        target = newTarget; // Permite asignar un target normal si lo deseas
    }

    private Vector2 GetRandomPositionInArea()
    {
        Vector2 areaSize = spawnArea.size;
        Vector2 areaCenter = spawnArea.bounds.center;

        float randomX = Random.Range(areaCenter.x - areaSize.x / 2, areaCenter.x + areaSize.x / 2);
        float randomY = Random.Range(areaCenter.y - areaSize.y / 2, areaCenter.y + areaSize.y / 2);

        return new Vector2(randomX, randomY);
    }
}
