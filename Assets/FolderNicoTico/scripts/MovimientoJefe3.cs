using UnityEngine;
using Pathfinding;

public class MovimientoJefe3 : MonoBehaviour
{
    private AIPath path;
    [SerializeField] private float moveSpeedInicial;
    public float stopDistanceThresholdCac; // Hacerlo público o serializado
    public float stopDistanceThresholdDis; // Hacerlo público o serializado
    public float moveSpeed = 2f;
    public Transform target;

    private Vector2 currentDirection;

    void Update()
    {
        if (target != null)
        {
            MoverHaciaTarget();
        }
    }

    private void MoverHaciaTarget()
    {
        Vector2 direction = (target.position - transform.position).normalized;

        // Limitar el movimiento a los puntos cardinales
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            currentDirection = new Vector2(Mathf.Sign(direction.x), 0);
        }
        else
        {
            currentDirection = new Vector2(0, Mathf.Sign(direction.y));
        }

        // Movimiento sin rotación
        Vector3 movement = new Vector3(currentDirection.x, currentDirection.y, 0) * moveSpeed * Time.deltaTime;
        transform.position += movement;
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }
    public void DetenerMovimiento()
    {
        moveSpeed = 0f;
    }

    public void ReanudarMovimiento()
    {
        moveSpeed = moveSpeedInicial;
    }

}
