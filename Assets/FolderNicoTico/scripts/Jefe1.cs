using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class Jefe1 : MonoBehaviour
{
    private AIPath path;
    [SerializeField] private float moveSpeedInicial;
    [SerializeField] private float moveSpeedMax;
    [SerializeField] private Transform target;
    [SerializeField] private float tiempoAtaque;

    [SerializeField] private float stopDistanceThreshold;
    private float distanceToTarget;

    [SerializeField] private GameObject prefabToSpawn;
    private GameObject currentPrefab;

    public Enemigo enemigoScript;

    [SerializeField] private CircleCollider2D spawnArea;

    private Coroutine rutinaAtaque;
    private Coroutine rutinaGenerarPrefab;

    [SerializeField] private float tiempoGenerarPrefab;
    [SerializeField] private float probabilidadGenerarPrefab;

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
    }

    private IEnumerator IntervaloAtaque()
    {
        while (true)
        {
            Debug.Log("Atacando al enemigo");
            enemigoScript.HacerDano(2);
            yield return new WaitForSeconds(tiempoAtaque);
        }
    }

    public void Seguimiento1()
    {
        path.maxSpeed = moveSpeedInicial;

        distanceToTarget = Vector2.Distance(transform.position, target.position);
        if (distanceToTarget < stopDistanceThreshold)
        {
            path.destination = transform.position;

            if (enemigoScript != null && rutinaAtaque == null)
            {
                rutinaAtaque = StartCoroutine(IntervaloAtaque());
            }
        }
        else
        {
            path.destination = target.position;

            if (rutinaAtaque != null)
            {
                StopCoroutine(rutinaAtaque);
                rutinaAtaque = null;
            }
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
            if (Random.value < probabilidadGenerarPrefab) // Genera el prefab con la probabilidad especificada
            {
                GenerarPrefab();
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
        // Obtenemos el centro y radio del CircleCollider2D
        Vector2 areaCenter = spawnArea.bounds.center;
        float radius = spawnArea.radius;

        // Generamos una posición aleatoria dentro del círculo
        float randomAngle = Random.Range(0f, 2f * Mathf.PI); // Ángulo aleatorio entre 0 y 2pi
        float randomDistance = Random.Range(0f, radius);  // Distancia aleatoria dentro del radio

        // Convertimos de coordenadas polares a cartesianas
        float randomX = areaCenter.x + Mathf.Cos(randomAngle) * randomDistance;
        float randomY = areaCenter.y + Mathf.Sin(randomAngle) * randomDistance;

        // Devolvemos la posición aleatoria dentro del círculo
        return new Vector2(randomX, randomY);
    }
}