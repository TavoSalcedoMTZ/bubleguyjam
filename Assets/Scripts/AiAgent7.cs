using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Spawners;

public class AiAgent7 : MonoBehaviour, IEnemigo
{
    [SerializeField] private Transform target; // El objetivo
    [SerializeField] private GameObject prefabBala; // Prefab de la bala
    [SerializeField] private float velocidadBala; // Velocidad de la bala
    [SerializeField] private float intervaloDisparo; // Intervalo entre disparos
    [SerializeField] private PolygonCollider2D zonaLuzCollider; // Collider que define la zona de luz
    [SerializeField] private LayerMask capaObstaculos; // Capa de obst�culos para raycast
    [SerializeField] private JabonManage jabonManage;

    private bool objetivoVisible = false;

    private EnemyManager enemyManager;  // Referencia al EnemyManager

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    // Implementaci�n de SetEnemyManager para cumplir con la interfaz IEnemigo
    public void SetEnemyManager(EnemyManager manager)
    {
        enemyManager = manager;  // Asignamos el EnemyManager al agente
    }

    public void SetJabonManage(JabonManage jabon)
    {
        jabonManage = jabon;
    }

    private void Start()
    {
        StartCoroutine(DispararConIntervalo());
    }

    private void Update()
    {
        DetectarObjetivo();
    }

    private void DetectarObjetivo()
    {
        if (target != null)
        {
            // Verificar si el objetivo est� dentro de la zona de luz
            if (zonaLuzCollider.OverlapPoint(target.position))
            {
                // Verificar si el objetivo est� en l�nea de visi�n (sin obst�culos)
                RaycastHit2D hit = Physics2D.Raycast(transform.position, target.position - transform.position, zonaLuzCollider.bounds.extents.magnitude, capaObstaculos);
                if (hit.collider == null) // No hay obst�culos entre el enemigo y el target
                {
                    objetivoVisible = true;
                }
                else
                {
                    objetivoVisible = false;
                }
            }
            else
            {
                objetivoVisible = false;
            }
        }
    }

    private IEnumerator DispararConIntervalo()
    {
        while (true)
        {
            if (objetivoVisible) // Solo disparar si el objetivo est� visible
            {
                Disparar();
            }
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
                rbBala.linearVelocity = direccion * velocidadBala;

                Destroy(balaInstanciada, 4f);  // Destruir la bala despu�s de 4 segundos
            }
        }
        else
        {
            Debug.LogWarning("Prefab de bala o target no asignado.");
        }
    }

    private void FixedUpdate()
    {
        // Si el objetivo no est� visible, el enemigo gira hacia �l
        if (!objetivoVisible && target != null)
        {
            Vector2 direccion = (target.position - transform.position).normalized;
            float angle = Mathf.Atan2(direccion.y, direccion.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
        else if (objetivoVisible)
        {
            // Si el objetivo est� visible, seguirlo (no rotar)
            Vector2 direccion = (target.position - transform.position).normalized;
            float angle = Mathf.Atan2(direccion.y, direccion.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
    }
}
