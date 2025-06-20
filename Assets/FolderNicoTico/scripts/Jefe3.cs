using UnityEngine;

public class Jefe3 : MonoBehaviour
{
    [SerializeField] private Transform target;

    [Header("Referencias de componentes")]
    [SerializeField] private MovimientoJefe3 movimiento;
    [SerializeField] private AtaqueJefe3 ataque;

    private float distanciaAlTarget;

    private void Start()
    {
        if (movimiento != null)
        {
            movimiento.SetTarget(target);
        }

        // Establece dirección de disparo por defecto si quieres que dispare hacia el jugador
        if (ataque != null)
        {
            ataque.direccionDisparo = (target.position - transform.position).normalized;
        }
    }

    private void Update()
    {
        if (target == null || movimiento == null || ataque == null)
            return;

        distanciaAlTarget = Vector2.Distance(transform.position, target.position);

        // Cambia comportamiento según modo de ataque activo
        if (ataque.EstaEnModoCuerpoACuerpo())
        {
            if (distanciaAlTarget <= movimiento.stopDistanceThresholdCac)
            {
                movimiento.DetenerMovimiento();
                ataque.IniciarAtaqueCuerpoACuerpo();
            }
            else
            {
                movimiento.ReanudarMovimiento();
                ataque.DetenerAtaque();
            }
        }
        else // Ataque a distancia
        {
            if (distanciaAlTarget <= movimiento.stopDistanceThresholdDis)
            {
                movimiento.DetenerMovimiento();
                ataque.IniciarAtaqueDistancia(target.position - transform.position);
            }
            else
            {
                movimiento.ReanudarMovimiento();
                ataque.DetenerAtaque();
            }
        }
    }
}
