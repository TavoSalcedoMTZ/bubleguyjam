using System.Collections;
using UnityEngine;

public class AtaqueJefe3CaC : MonoBehaviour
{
    [SerializeField] private float tiempoAtaque;
    [SerializeField] private Enemigo enemigoScript;

    private Coroutine rutinaAtaque;

    public void IniciarAtaque()
    {
        if (rutinaAtaque != null) return;

        rutinaAtaque = StartCoroutine(IntervaloAtaque());
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

    public void DetenerAtaque()
    {
        if (rutinaAtaque != null)
        {
            StopCoroutine(rutinaAtaque);
            rutinaAtaque = null;
        }
    }
}
