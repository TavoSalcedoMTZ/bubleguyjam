using System.Collections;
using UnityEngine;

public class Ataque : MonoBehaviour
{
    [SerializeField] private GameObject prefabBala;
    [SerializeField] private Transform puntoDeDisparo;
    [SerializeField] private float tiempoAtaque;
    private float temporizador;

    public Enemigo enemigoScript;

    // M�todo para controlar el tiempo de ataque y disparar
    public void TemporizadorAtaque(Vector2 direccion)
    {
        temporizador += Time.deltaTime;
        if (temporizador >= tiempoAtaque)
        {
            Disparar(direccion);
            temporizador = 0f;
        }
    }

    private void Disparar(Vector2 direccion)
    {
        if (prefabBala != null && puntoDeDisparo != null)
        {
            // Instanciar el proyectil
            GameObject proyectil = Instantiate(prefabBala, puntoDeDisparo.position, puntoDeDisparo.rotation);

            // Configurar la direcci�n del proyectil
            Rigidbody2D rb = proyectil.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.linearVelocity = direccion * proyectil.GetComponent<Proyectil>().velocidad;
            }
        }
    }

    private IEnumerator IntervaloAtaque()
    {
        while (true)
        {
            Debug.Log("Atacando al enemigo");
            enemigoScript.HacerDano(1);
            yield return new WaitForSeconds(tiempoAtaque);
        }
    }
}
