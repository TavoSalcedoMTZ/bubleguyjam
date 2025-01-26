using System.Collections;
using UnityEngine;

public class GeneradorPrefabsJefe1 : MonoBehaviour
{
    public GameObject CurrentPrefab { get; private set; }

    private GameObject prefabToSpawn;
    private CircleCollider2D spawnArea;
    private float tiempoGenerarPrefab;
    private float probabilidadGenerarPrefab;

    public void Inicializar(GameObject prefabToSpawn, CircleCollider2D spawnArea, float tiempoGenerarPrefab, float probabilidadGenerarPrefab)
    {
        this.prefabToSpawn = prefabToSpawn;
        this.spawnArea = spawnArea;
        this.tiempoGenerarPrefab = tiempoGenerarPrefab;
        this.probabilidadGenerarPrefab = probabilidadGenerarPrefab;
        StartCoroutine(GenerarPrefabConProbabilidad());
    }

    private IEnumerator GenerarPrefabConProbabilidad()
    {
        while (true)
        {
            yield return new WaitForSeconds(tiempoGenerarPrefab);
            if (Random.value < probabilidadGenerarPrefab)
            {
                GenerarPrefab();
            }
        }
    }

    private void GenerarPrefab()
    {
        if (CurrentPrefab == null)
        {
            Vector2 spawnPosition = GetRandomPositionInArea();
            CurrentPrefab = Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);
        }
    }

    private Vector2 GetRandomPositionInArea()
    {
        Vector2 areaCenter = spawnArea.bounds.center;
        float radius = spawnArea.radius;

        float randomAngle = Random.Range(0f, 2f * Mathf.PI);
        float randomDistance = Random.Range(0f, radius);

        float randomX = areaCenter.x + Mathf.Cos(randomAngle) * randomDistance;
        float randomY = areaCenter.y + Mathf.Sin(randomAngle) * randomDistance;

        return new Vector2(randomX, randomY);
    }
}
