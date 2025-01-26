using UnityEngine;

public class AtaqueJefe3Distancia : MonoBehaviour
{
    public GameObject prefabBala;
    public Transform puntoDeDisparo;

    public void Disparar(Vector2 direccion)
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
