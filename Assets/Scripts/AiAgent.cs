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
    [SerializeField] private JabonManage jabonManage;

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

        // El enemigo siempre sigue al jugador
        path.destination = target.position;
    }

    private void IniciarAtaque()
    {
        if (ataqueCoroutine == null)
        {
            ataqueCoroutine = StartCoroutine(AtaqueCoroutine());
        }
    }

    private void DetenerAtaque()
    {
        if (ataqueCoroutine != null)
        {
            StopCoroutine(ataqueCoroutine);
            ataqueCoroutine = null;
        }
    }

    private IEnumerator AtaqueCoroutine()
    {
        while (true)
        {
            enemigoScript.HacerDano(1);
            yield return new WaitForSeconds(2f);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform == target)
        {
            path.maxSpeed = 0f;
            IniciarAtaque();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.transform == target)
        {
            path.maxSpeed = moveSpeed;
            DetenerAtaque();
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
