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

    /*
    Los enemigos se van a generar conforme las posiciones 
    Por ejemplo, el enemigo[1] se va a generar en el spawn[1] 
    */

    private void Start()
    {
        SpawearEnemigos();
    }

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
    }

    public void SpawearEnemigos()
    {
        for (int a = 0; a < enemigo.Length; a++)
        {
            // Instanciar el enemigo en la posición correspondiente
            GameObject enemigoInstanciado = Instantiate(enemigo[a], puntoSpawn[a], Quaternion.identity);

            // Incrementar el contador de enemigos vivos
            enemyManager.totalEnemiesAlive++;

            // Verificar si el enemigo tiene el componente IEnemigo y asignar el target
            if (enemigoInstanciado.TryGetComponent(out IEnemigo targetableEnemigo))
            {
                targetableEnemigo.SetTarget(target); // Asignar el target
                targetableEnemigo.SetEnemyManager(enemyManager); // Asignar el EnemyManager
            }
        }
    }
}
