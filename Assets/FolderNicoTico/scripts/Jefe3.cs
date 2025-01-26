using System.Collections;
using UnityEngine;
using Pathfinding;

public class Jefe3 : MonoBehaviour
{
    private AIPath path;
    [SerializeField] private Transform target;
    [SerializeField] private float tiempoAtaque;

    private float temporizador;
    private Coroutine rutinaAtaque;

    [SerializeField] private AtaqueJefe3CaC ataqueCuerpoACuerpo;
    [SerializeField] private AtaqueJefe3Distancia ataqueADistancia;
    [SerializeField] private MovimientoJefe3 movimiento;

    // Umbrales de distancia para elegir el tipo de ataque
    [SerializeField] private float umbralAtaqueCac;
    [SerializeField] private float umbralAtaqueDis;

    private void Start()
    {
        path = GetComponent<AIPath>();
    }

    private void Update()
    {
        if (target != null)
        {
            temporizador += Time.deltaTime;
            if (temporizador >= tiempoAtaque)
            {
                ElegirAtaque(); // Elegir ataque según distancia
                temporizador = 0f;
            }

            // Revisar la distancia y detener el ataque cuerpo a cuerpo si nos alejamos del objetivo
            float distanceToTarget = Vector2.Distance(transform.position, target.position);
            if (distanceToTarget > umbralAtaqueCac)
            {
                // Si estamos fuera del rango de ataque cuerpo a cuerpo, detenerlo
                ataqueCuerpoACuerpo.DetenerAtaque();
            }
        }
    }

    private void ElegirAtaque()
    {
        // Calcular la distancia al objetivo
        float distanceToTarget = Vector2.Distance(transform.position, target.position);

        // Decidir el tipo de ataque según la distancia
        if (distanceToTarget <= umbralAtaqueCac) // Si está a 5 unidades o menos, ataque cuerpo a cuerpo
        {
            ataqueCuerpoACuerpo.IniciarAtaque();
        }
        else if (distanceToTarget > umbralAtaqueDis) // Si está a más de 5 unidades, ataque a distancia
        {
            ataqueADistancia.Disparar(target.position);
        }
        else
        {
            Debug.Log("Ningún ataque seleccionado. Fuera de rango.");
        }
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }
}
