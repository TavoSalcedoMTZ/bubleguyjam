using System.Collections;
using UnityEngine;

public class AtaqueJefe1 : MonoBehaviour
{
    private Enemigo enemigoScript;
    private float tiempoAtaque;
    private Coroutine rutinaAtaque;

    public void Inicializar(Enemigo enemigoScript, float tiempoAtaque)
    {
        this.enemigoScript = enemigoScript;
        this.tiempoAtaque = tiempoAtaque;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        DetenerAtaque();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        IniciarAtaque();
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
            enemigoScript.HacerDano(2);
            yield return new WaitForSeconds(tiempoAtaque);
        }
    }
}
