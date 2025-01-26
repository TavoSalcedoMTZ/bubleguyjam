using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class Jefe3 : MonoBehaviour
{
    private AIPath path;
    [SerializeField] private float moveSpeedInicial;
    [SerializeField] private float moveSpeedMax;
    [SerializeField] private Transform target;
    [SerializeField] private float tiempoAtaque;
    [SerializeField] private float velocidadBala;
    [SerializeField] private float stopDistanceThresholdCac;
    [SerializeField] private float stopDistanceThresholdDis;
    private float distanceToTarget;
    public GameObject prefabBala;
    public Transform puntoDeDisparo;
    [SerializeField] private JabonManage jugador;
    [SerializeField] private Proyectil proyectil;

    [SerializeField] private GameObject prefabToSpawn;
    private GameObject currentPrefab;

    public Enemigo enemigoScript;

    [SerializeField] private CircleCollider2D spawnArea;

    private Coroutine rutinaAtaque;
    private Coroutine rutinaGenerarPrefab;

    [SerializeField] private float tiempoGenerarPrefab;
    [SerializeField] private float probabilidadGenerarPrefab;

    [SerializeField] private float tiempoparaprobabilidadDeCambiarTipoAtaque;
    [SerializeField] private float probabilidadDeCambiarTipoAtaque;

    private float temporizador;

    private void Start()
    {
        path = GetComponent<AIPath>();
        rutinaGenerarPrefab = StartCoroutine(GenerarPrefabConProbabilidad());
    }

    private void Update()
    {
        if (currentPrefab == null)
        {
            Seguimiento1();
        }
        else
        {
            SeguimientoPrefab();
        }

        if (target != null)
        {
            // Calcular la dirección hacia el objetivo
            Vector2 direccion = (target.position - transform.position).normalized;

            // Rotar el objeto que dispara para apuntar al objetivo
            float angulo = Mathf.Atan2(direccion.y, direccion.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angulo);

            // Controlar el tiempo de ataque
            temporizador += Time.deltaTime;
            if (temporizador >= tiempoAtaque)
            {
                ElegirAtaque(direccion); // Elegir ataque aleatoriamente
                temporizador = 0f;
            }
        }
    }

    private IEnumerator IntervaloAtaque()
    {
        Debug.Log("Atacando al enemigo con cuerpo a cuerpo");
        while (true)
        {
            enemigoScript.HacerDano(2);
            yield return new WaitForSeconds(tiempoAtaque);
        }
    }

    private void ElegirAtaque(Vector2 direccion)
    {
        // Calcular la distancia al objetivo
        distanceToTarget = Vector2.Distance(transform.position, target.position);

        int tipoAtaque = Random.Range(0, 2); // 0 para cuerpo a cuerpo, 1 para ataque a distancia
        if (tipoAtaque == 0 && distanceToTarget < stopDistanceThresholdCac)
        {
            // Ataque cuerpo a cuerpo
            Debug.Log("Seleccionado ataque cuerpo a cuerpo.");

            // Detener cualquier ataque a distancia activo
            if (rutinaAtaque != null)
            {
                StopCoroutine(rutinaAtaque);
                rutinaAtaque = null;
            }

            // Iniciar ataque cuerpo a cuerpo si no está activo
            if (rutinaAtaque == null)
            {
                rutinaAtaque = StartCoroutine(IntervaloAtaque());
            }
        }
        else if (tipoAtaque == 1 && distanceToTarget < stopDistanceThresholdDis)
        {
            // Ataque a distancia
            Debug.Log("Seleccionado ataque a distancia.");

            // Detener cualquier ataque cuerpo a cuerpo activo
            if (rutinaAtaque != null)
            {
                StopCoroutine(rutinaAtaque);
                rutinaAtaque = null;
            }

            // Ejecutar ataque a distancia
            Disparar(direccion);
        }
        else
        {
            Debug.Log("Ningún ataque seleccionado. Fuera de rango.");
        }
    }


    public void Seguimiento1()
    {
        path.maxSpeed = moveSpeedInicial;

        distanceToTarget = Vector2.Distance(transform.position, target.position);
        if (distanceToTarget < stopDistanceThresholdCac)
        {
            path.destination = transform.position;
        }
        else
        {
            path.destination = target.position;

            // Detener cualquier rutina activa si el objetivo está fuera del rango cuerpo a cuerpo
            if (rutinaAtaque != null)
            {
                StopCoroutine(rutinaAtaque);
                rutinaAtaque = null;
            }
        }

    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    public interface IAsignacionB
    {
        void SetEnemigo(Enemigo enemigoScript);
    }

    public void SeguimientoPrefab()
    {
        if (currentPrefab != null)
        {
            path.destination = currentPrefab.transform.position;
            path.maxSpeed = moveSpeedMax;

            if (Vector2.Distance(transform.position, currentPrefab.transform.position) < stopDistanceThresholdCac)
            {
                Destroy(currentPrefab);
                currentPrefab = null;
            }
        }
    }

    private IEnumerator GenerarPrefabConProbabilidad()
    {
        while (true)
        {
            yield return new WaitForSeconds(tiempoGenerarPrefab);
            if (Random.value < probabilidadGenerarPrefab)
            {
                GenerarPrefab();
            }
        }
    }

    public void GenerarPrefab()
    {
        if (currentPrefab == null)
        {
            Vector2 spawnPosition = GetRandomPositionInArea();
            currentPrefab = Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);
        }
    }

    private Vector2 GetRandomPositionInArea()
    {
        // Obtenemos el centro y radio del CircleCollider2D
        Vector2 areaCenter = spawnArea.bounds.center;
        float radius = spawnArea.radius;

        // Generamos una posición aleatoria dentro del círculo
        float randomAngle = Random.Range(0f, 2f * Mathf.PI); // Ángulo aleatorio entre 0 y 2pi
        float randomDistance = Random.Range(0f, radius);  // Distancia aleatoria dentro del radio

        // Convertimos de coordenadas polares a cartesianas
        float randomX = areaCenter.x + Mathf.Cos(randomAngle) * randomDistance;
        float randomY = areaCenter.y + Mathf.Sin(randomAngle) * randomDistance;

        // Devolvemos la posición aleatoria dentro del círculo
        return new Vector2(randomX, randomY);
    }


    private void Disparar(Vector2 direccion)
    {
        Debug.Log("Atacando al enemigo con ataque a distancia");
        if (prefabBala != null && puntoDeDisparo != null)
        {
            // Instanciar el proyectil
            GameObject proyectil = Instantiate(prefabBala, puntoDeDisparo.position, puntoDeDisparo.rotation);

            // Configurar la dirección del proyectil
            Rigidbody2D rb = proyectil.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = direccion * proyectil.GetComponent<Proyectil>().velocidad;
            }
        }
    }
}
