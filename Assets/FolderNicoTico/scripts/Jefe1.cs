using UnityEngine;
using Pathfinding;

[RequireComponent(typeof(AIPath))]
public class Jefe1 : MonoBehaviour
{
    public Transform target;
    public float moveSpeed = 3f;
    public float stopDistanceThreshold = 1f;


    private AIPath aiPath;
    private bool siguiendo = true;

    void Start()
    {
        aiPath = GetComponent<AIPath>();
        aiPath.maxSpeed = moveSpeed;
        aiPath.canMove = true;
    }

    void Update()
    {
        if (target == null) return;

        float distance = Vector2.Distance(transform.position, target.position);

        if (siguiendo && distance < stopDistanceThreshold)
        {
            DetenerSeguimiento();
        }
        else if (!siguiendo && distance >= stopDistanceThreshold)
        {
            ReiniciarSeguimiento();
        }

        aiPath.destination = siguiendo ? target.position : transform.position;
    }

    private void DetenerSeguimiento()
    {
        siguiendo = false;
        aiPath.canMove = false;
    }

    private void ReiniciarSeguimiento()
    {
        siguiendo = true;
        aiPath.canMove = true;
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }
}
