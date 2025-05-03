using UnityEngine;
using Pathfinding;
using System.Collections;
using static Spawners;

public class AiAgent : MonoBehaviour, IEnemigo
{
    private AIPath path;
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private Transform target;
    [SerializeField] private EnemyManager enemyManager;
    [SerializeField] private float stopDistanceThreshold = 1f;
    [SerializeField] private JabonManage jabonManage;

    private float distanceToTarget;
    private bool siguiendo = true;
    private Coroutine ataqueCoroutine;
    private Enemigo enemigoScript;

    private void Start()
    {
        path = GetComponent<AIPath>();
        enemigoScript = GetComponent<Enemigo>();

        if (path != null)
        {
            path.maxSpeed = moveSpeed;
        }
    }

    private void Update()
    {
        if (target == null || path == null) return;

        distanceToTarget = Vector2.Distance(transform.position, target.position);

        if (siguiendo && distanceToTarget < stopDistanceThreshold)
        {
            DetenerSeguimiento();
        }
        else if (!siguiendo && distanceToTarget >= stopDistanceThreshold)
        {
            ReiniciarSeguimiento();
        }

        ActualizarDestino();
    }

    private void ActualizarDestino()
    {
        if (path == null) return;

        path.maxSpeed = siguiendo ? moveSpeed : 0f;
        path.destination = siguiendo ? target.position : transform.position;
    }

    private void IniciarAtaque()
    {
        if (ataqueCoroutine == null)
        {
            ataqueCoroutine = StartCoroutine(AtaqueCoroutine());
        }
    }

    private IEnumerator AtaqueCoroutine()
    {
        while (!siguiendo)
        {
            enemigoScript.HacerDano(1);
            yield return new WaitForSeconds(2f);
        }
    }

    private void DetenerSeguimiento()
    {
        siguiendo = false;
        path.maxSpeed = 0f;
        IniciarAtaque();
    }

    private void ReiniciarSeguimiento()
    {
        siguiendo = true;
        path.maxSpeed = moveSpeed;

        if (ataqueCoroutine != null)
        {
            StopCoroutine(ataqueCoroutine);
            ataqueCoroutine = null;
        }
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    public void SetEnemyManager(EnemyManager manager)
    {
        enemyManager = manager;
    }

    public void SetJabonManage(JabonManage jabon)
    {
        jabonManage = jabon;
    }
}
