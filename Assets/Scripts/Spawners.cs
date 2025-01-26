using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawners : MonoBehaviour
{
    // Posiciones de los puntos de spawn para los enemigos
    public Vector2[] puntoSpawn;

    // Lista de enemigos que se van a generar
    public GameObject[] enemigo;

    [SerializeField] private Transform target;
    public EnemyManager enemyManager;
    public JabonManage jabon;
    // Número de enemigos a spawnear
    public int numberOfEnemiesToSpawn = 3;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            SpawearEnemigos();
        }
    }

    public interface IEnemigo
    {
        void SetTarget(Transform target);
        void SetEnemyManager(EnemyManager manager); // Agregar un método para asignar el EnemyManager
        void SetJabonManage(JabonManage jabonmanage);
    }

    public void SpawearEnemigos()
    {
        // Creamos una lista de índices de puntos de spawn para randomizar
        List<int> spawnIndices = new List<int>();
        for (int i = 0; i < puntoSpawn.Length; i++)
        {
            spawnIndices.Add(i);
        }

        // Mezclamos los índices para obtener una selección aleatoria de puntos de spawn
        Shuffle(spawnIndices);

        // Creamos una lista de enemigos a spawn
        List<GameObject> selectedEnemies = new List<GameObject>();
        for (int i = 0; i < numberOfEnemiesToSpawn; i++)
        {
            selectedEnemies.Add(enemigo[Random.Range(0, enemigo.Length)]); // Seleccionamos un enemigo aleatorio
        }

        // Spawn de enemigos con posiciones aleatorias
        for (int i = 0; i < numberOfEnemiesToSpawn; i++)
        {
            if (i < spawnIndices.Count)  // Nos aseguramos de no sobrepasar la cantidad de puntos de spawn disponibles
            {
                // Seleccionar aleatoriamente un punto de spawn
                Vector2 spawnPosition = puntoSpawn[spawnIndices[i]];

                // Seleccionamos un enemigo aleatorio de la lista
                GameObject enemigoInstanciado = Instantiate(selectedEnemies[i], spawnPosition, Quaternion.identity);

                // Incrementar el contador de enemigos vivos
                enemyManager.totalEnemiesAlive++;

                // Verificar si el enemigo tiene el componente IEnemigo y asignar el target
                if (enemigoInstanciado.TryGetComponent(out IEnemigo targetableEnemigo))
                {
                    targetableEnemigo.SetTarget(target); // Asignar el target
                    targetableEnemigo.SetEnemyManager(enemyManager); // Asignar el EnemyManager
                    targetableEnemigo.SetJabonManage(jabon);
                }
            }
        }
    }

    // Función para mezclar los índices aleatorios de la lista
    private void Shuffle(List<int> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n + 1);
            int value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}