using System.Collections;
using UnityEngine;

public class AiAgent6 : MonoBehaviour
{
    [SerializeField] private Transform target;  // El objetivo a seguir
    [SerializeField] private GameObject prefabBala;  // Prefab de la bala a disparar
    [SerializeField] private float velocidadBala;  // Velocidad de la bala
    [SerializeField] private float intervaloDisparo;  // Intervalo entre disparos
    [SerializeField] private JabonManage jugador;

    private void Start()
    {
        StartCoroutine(DispararConIntervalo());  // Iniciar la rutina de disparo
    }

    private void Update()
    {
        // Opcional: Añadir lógica para rotar hacia el objetivo o realizar otras acciones
    }

    private IEnumerator DispararConIntervalo()
    {
        while (true)
        {
            Disparar();  // Llamamos a la función para disparar
            yield return new WaitForSeconds(intervaloDisparo);  // Esperar el intervalo de disparo
        }
    }

    public void Disparar()
    {
        if (prefabBala != null && target != null)
        {
            // Instanciamos el proyectil en la posición actual del agente
            GameObject balaInstanciada = Instantiate(prefabBala, transform.position, Quaternion.identity);

            // Obtener el componente Rigidbody2D de la bala
            Rigidbody2D rbBala = balaInstanciada.GetComponent<Rigidbody2D>();

            if (rbBala != null)
            {
                // Calcular la dirección hacia el target (jugador o enemigo)
                Vector2 direccion = (target.position - transform.position).normalized;

                // Asignamos la velocidad a la bala en esa dirección
                rbBala.velocity = direccion * velocidadBala;

                // Destruimos la bala después de 5 segundos (según el tiempo de vida en el script del proyectil)
                Destroy(balaInstanciada, 5f);
            }
            else
            {
                Debug.LogWarning("La bala instanciada no tiene un Rigidbody2D.");
            }
        }
        else
        {
            Debug.LogWarning("Prefab de bala o target no asignado.");
        }
    }
}
