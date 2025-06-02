using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class Jefe2 : MonoBehaviour
{
    private AIPath path;
    [SerializeField] private float moveSpeedInicial;
    [SerializeField] private float moveSpeedMax;
    [SerializeField] private Transform target;
    [SerializeField] private float stopDistanceThreshold;
    private float distanceToTarget;
    [SerializeField] private float tiempoGenerarPrefab;
    [SerializeField] private float probabilidadGenerarPrefab;
    [SerializeField] private CircleCollider2D spawnArea;

    [SerializeField] private GameObject prefabToSpawn;  // Esta es la línea que faltaba, asigna el prefab que quieres instanciar
    private GameObject currentPrefab;
    private Coroutine rutinaGenerarPrefab;

    public Rotacion rotacionScript;
    public Ataque ataqueScript;

    private void Start()
    {
        path = GetComponent<AIPath>();

        rutinaGenerarPrefab = StartCoroutine(GenerarPrefabConProbabilidad());
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

        if (target != null)
        {
            // Llamamos al método para rotar hacia el objetivo
            Vector2 direccion = (target.position - transform.position).normalized;
            rotacionScript.RotarHaciaObjetivo(direccion);

            // Controlar el ataque
            ataqueScript.TemporizadorAtaque(direccion);
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


    private IEnumerator GenerarPrefabConProbabilidad()
    {
        while (true)
        {
            yield return new WaitForSeconds(tiempoGenerarPrefab);

            if (currentPrefab == null && Random.value < probabilidadGenerarPrefab)
            {
                GenerarPrefab();
            }
        }
    }


    public void GenerarPrefab()
    {
        if (currentPrefab == null && prefabToSpawn != null) // Verifica que el prefabToSpawn no sea nulo
        {
            Vector2 spawnPosition = GetRandomPositionInArea();
            currentPrefab = Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);
        }
    }

    private Vector2 GetRandomPositionInArea()
    {
        Vector2 areaCenter = spawnArea.bounds.center;
        float radius = spawnArea.radius;
        float minDistanceFromBoss = stopDistanceThreshold + 0.5f;

        Vector2 spawnPosition;

        int intentosMaximos = 10;
        int intentos = 0;

        do
        {
            float randomAngle = Random.Range(0f, 2f * Mathf.PI);
            float randomDistance = Random.Range(minDistanceFromBoss, radius); 

            float randomX = areaCenter.x + Mathf.Cos(randomAngle) * randomDistance;
            float randomY = areaCenter.y + Mathf.Sin(randomAngle) * randomDistance;

            spawnPosition = new Vector2(randomX, randomY);
            intentos++;
        }
        while (Vector2.Distance(transform.position, spawnPosition) < minDistanceFromBoss && intentos < intentosMaximos);

        return spawnPosition;
    }

}
