using UnityEngine;
using Pathfinding;

[RequireComponent(typeof(Seeker))]
[RequireComponent(typeof(Rigidbody2D))]
public class Jefe1 : MonoBehaviour
{
    public Transform target;
    public float speed = 200f;
    public float nextWaypointDistance = 0.5f;

    private Seeker seeker;
    private Rigidbody2D rb;

    private Path path;
    private int currentWaypoint = 0;
    private bool reachedEndOfPath = false;

    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        // Recalcular ruta cada 0.5 segundos
        InvokeRepeating(nameof(UpdatePath), 0f, 0.5f);
    }

    void UpdatePath()
    {
        if (target == null) return;

        // Solo pide una nueva ruta si no hay otra calculándose
        if (seeker.IsDone())
        {
            seeker.StartPath(rb.position, target.position, OnPathComplete);
        }
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    void FixedUpdate()
    {
        if (path == null) return;

        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }

        // Dirección hacia el siguiente waypoint
        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.fixedDeltaTime;

        // Movimiento
        rb.MovePosition(rb.position + force);

        // Distancia al siguiente waypoint
        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        // Avanzar al siguiente waypoint si ya llegamos al actual
        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }
}
