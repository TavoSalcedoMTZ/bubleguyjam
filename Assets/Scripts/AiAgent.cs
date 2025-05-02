using UnityEngine;
using Pathfinding;
using static Spawners;
using System.Collections;

public class AiAgent : MonoBehaviour, IEnemigo
{
    private AIPath path;
    [SerializeField] private float moveSpeed;
    [SerializeField] private Transform target;
    [SerializeField] private EnemyManager enemyManager;
    [SerializeField] private float stopDistanceThreshold; // Umbral de distancia para detener el seguimiento
    [SerializeField] private JabonManage jabonManage; // Para gestionar la lógica de ataque
    private float distanceToTarget;
    private bool siguiendo;
    private Coroutine ataqueCoroutine; // Referencia a la rutina de ataque
    private Enemigo enemigoScript;

    private void Start()
    {
        path = GetComponent<AIPath>();
        siguiendo = true; // Iniciamos en modo seguimiento
        enemigoScript = GetComponent<Enemigo>();
    }

    private void Update()
    {
        // Verificamos si estamos siguiendo al objetivo
        Seguimiento1();

        // Comprobamos la distancia para decidir si seguimos o atacamos
        distanceToTarget = Vector2.Distance(transform.position, target.position);

        if (siguiendo && distanceToTarget < stopDistanceThreshold)
        {
            // Si estamos en el rango de detener el seguimiento
            DetenerSeguimiento();
        }
        else if (!siguiendo && distanceToTarget >= stopDistanceThreshold)
        {
            // Si no estamos siguiendo y la distancia es mayor al umbral, reanudar el seguimiento
            ReiniciarSeguimiento();
        }
    }

    // Método para iniciar el ataque
    private void IniciarAtaque()
    {
        if (ataqueCoroutine == null)
        {
            ataqueCoroutine = StartCoroutine(AtaqueCoroutine());
        }
    }

    // Método que se ejecuta en un intervalo para simular el ataque
    private IEnumerator AtaqueCoroutine()
    {
        while (!siguiendo)
        {
            enemigoScript.HacerDano(1); 
            yield return new WaitForSeconds(2f); // Espera un segundo antes del siguiente ataque
        }
    }


    // Detener el seguimiento y empezar el ataque
    private void DetenerSeguimiento()
    {
        siguiendo = false; // Dejamos de seguir
        path.maxSpeed = 0; // Detenemos el movimiento
        IniciarAtaque(); // Comenzamos el ataque
    }

    // Reiniciar el seguimiento si la distancia vuelve a ser mayor al umbral
    private void ReiniciarSeguimiento()
    {
        siguiendo = true; // Volvemos a seguir
        path.maxSpeed = moveSpeed; // Restauramos la velocidad de movimiento
        if (ataqueCoroutine != null)
        {
            StopCoroutine(ataqueCoroutine); // Detenemos el ataque
            ataqueCoroutine = null;
        }
    }

    // Método que actualiza el destino del agente
    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    // Implementación de SetEnemyManager que es parte de la interfaz IEnemigo
    public void SetEnemyManager(EnemyManager manager)
    {
        enemyManager = manager; // Asigna el EnemyManager
    }

    public void SetJabonManage(JabonManage jabon)
    {
        jabonManage = jabon;
    }

    // Método para seguir al objetivo
    public void Seguimiento1()
    {
        path.maxSpeed = moveSpeed;
        if (siguiendo)
        {
            path.destination = target.position; // Continuamos siguiendo el objetivo
        }
        else
        {
            path.destination = transform.position; // Dejamos de movernos cuando atacamos
        }
    }
    /*
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Target"))
        {
            HacerDano(1);
        }
    }
    */
}
