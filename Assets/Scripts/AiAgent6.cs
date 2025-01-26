using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Spawners;

public class AiAgent6 : MonoBehaviour, IEnemigo
{
    [SerializeField] private Transform target;
    [SerializeField] private GameObject prefabBala;
    [SerializeField] private float velocidadBala;
    [SerializeField] private float intervaloDisparo;

    private EnemyManager enemyManager;  // Referencia al EnemyManager

    private void Start()
    {
        StartCoroutine(DispararConIntervalo());
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    public void SetEnemyManager(EnemyManager manager)
    {
        enemyManager = manager;  // Asignamos el EnemyManager
    }

    private IEnumerator DispararConIntervalo()
    {
        while (true)
        {
            Disparar();
            yield return new WaitForSeconds(intervaloDisparo);
        }
    }

    public void Disparar()
    {
        if (prefabBala != null && target != null)
        {
            GameObject balaInstanciada = Instantiate(prefabBala, transform.position, Quaternion.identity);

            Rigidbody2D rbBala = balaInstanciada.GetComponent<Rigidbody2D>();

            if (rbBala != null)
            {
                Vector2 direccion = (target.position - transform.position).normalized;
                rbBala.velocity = direccion * velocidadBala;

                Destroy(balaInstanciada, 4f);  // Destruir la bala después de 4 segundos
            }
            else
            {
                Debug.LogWarning("La bala instanciada no tiene un Rigidbody2D.");
            }
        }
        else
        {
            Debug.LogWarning("Prefab de bala o target no asignado.");
        }
    }
}
