using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;
using static Spawners;

public class AiAgent2 : MonoBehaviour, IEnemigo
{
    private AIPath path;
    [SerializeField] private float moveSpeed;
    [SerializeField] private Transform target;
    [SerializeField] private float stopDistanceThreshold;
    private float distanceToTarget;
    [SerializeField] private Collider2D collider2D;
    [SerializeField] private JabonManage jabonManage;
    private Coroutine ataqueCoroutine;
    private Enemigo enemigoScript;

    private bool isFollowing = false;

    // Variable para el EnemyManager
    private EnemyManager enemyManager;

    private void Start()
    {
        path = GetComponent<AIPath>();
        if (jabonManage == null)
        {
            jabonManage = FindFirstObjectByType<JabonManage>();
        }
        path.canMove = false; // Al inicio el agente no debería moverse hasta que entre en el área de activación
        enemigoScript = GetComponent<Enemigo>();
    }

    private void Update()
    {
        if (isFollowing)
        {
            Seguimiento1();
        }
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    public void SetJabonManage(JabonManage jabon)
    {
        jabonManage = jabon;
    }

    // Implementación de SetEnemyManager
    public void SetEnemyManager(EnemyManager manager)
    {
        enemyManager = manager; // Asigna el EnemyManager
    }

    private IEnumerator AtaqueCoroutine()
    {
        while (true)
        {
            if (Vector2.Distance(transform.position, target.position) < stopDistanceThreshold)
            {
                enemigoScript.HacerDano(1);
            }

            yield return new WaitForSeconds(2f); // Ataca cada segundo
        }
    }


    public void Seguimiento1()
    {
        path.maxSpeed = moveSpeed;

        distanceToTarget = Vector2.Distance(transform.position, target.position);
        if (distanceToTarget < stopDistanceThreshold)
        {
            path.destination = transform.position; // Detenerse si está lo suficientemente cerca
        }
        else
        {
            path.destination = target.position; // Seguir el objetivo
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Target"))
        {
            path.canMove = true;
            isFollowing = true;

            if (ataqueCoroutine == null)
            {
                ataqueCoroutine = StartCoroutine(AtaqueCoroutine());
            }
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Target"))
        {
            path.canMove = false;
            isFollowing = false;

            if (ataqueCoroutine != null)
            {
                StopCoroutine(ataqueCoroutine);
                ataqueCoroutine = null;
            }
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
