using System.Collections;
using UnityEngine;

public class AtaqueJefe1 : MonoBehaviour
{
    [Header("Referencias")]
    public Enemigo enemigoScript;
    private Coroutine rutinaAtaque;

    [Header("Variables")]
    [SerializeField, Range(0, 5)] private float tiempoAtaque;
    [Range(0, 10)]public int Dano;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Target"))
        {
            IniciarAtaque();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Target"))
        {
            DetenerAtaque();
        }
    }
    public void IniciarAtaque()
    {
        if (enemigoScript != null && rutinaAtaque == null)
        {
            rutinaAtaque = StartCoroutine(IntervaloAtaque());
        }
    }

    public void DetenerAtaque()
    {
        if (rutinaAtaque != null)
        {
            StopCoroutine(rutinaAtaque);
            rutinaAtaque = null;
        }
    }

    private IEnumerator IntervaloAtaque()
    {
        while (true)
        {
            Debug.Log("Atacando al enemigo");
            //Esta funcion necesita una variable que lo condicione porque si no el daño lo esta aplicado aunque no este en el cuarto del enemigo
            enemigoScript.HacerDano(Dano);
            yield return new WaitForSeconds(tiempoAtaque);
        }
    }
}
