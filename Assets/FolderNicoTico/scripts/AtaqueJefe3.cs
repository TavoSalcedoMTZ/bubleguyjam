using System.Collections;
using UnityEngine;

public class AtaqueJefe3 : MonoBehaviour
{
    [Header("General")]
    [SerializeField] private Enemigo enemigoScript;
    [SerializeField] private float tiempoCambioModo = 5f;
    [Range(0f, 1f)]
    [SerializeField] private float probabilidadDeCambiarModoAtaque = 0.5f;

    [Header("Cuerpo a Cuerpo")]
    [SerializeField] private float tiempoAtaqueCaC = 1.5f;
    [SerializeField] private int dano;

    [Header("A Distancia")]
    [SerializeField] private GameObject prefabBala;
    public Transform puntoDeDisparo;
    [SerializeField] private float intervaloDisparo = 2f;
    public Vector2 direccionDisparo = Vector2.right;

    private Coroutine rutinaPrincipal;
    private Coroutine rutinaAtaque;

    [SerializeField] private Transform objetivo;
    [SerializeField] private float rangoAtaqueCaC = 1.5f;


    private enum ModoAtaque { CuerpoACuerpo, Distancia }
    private ModoAtaque modoActual = ModoAtaque.CuerpoACuerpo;

    private void Start()
    {
        rutinaPrincipal = StartCoroutine(CambiarModoYAtacar());
    }

    private IEnumerator CambiarModoYAtacar()
    {
        IniciarRutinaDeAtaque();

        while (true)
        {
            yield return new WaitForSeconds(tiempoCambioModo);

            float random = Random.value;
            if (random <= probabilidadDeCambiarModoAtaque)
            {
                modoActual = (modoActual == ModoAtaque.CuerpoACuerpo) ? ModoAtaque.Distancia : ModoAtaque.CuerpoACuerpo;
                Debug.Log("Modo de ataque cambiado a: " + modoActual);

                IniciarRutinaDeAtaque();
            }
            else
            {
                Debug.Log("Se mantiene el modo de ataque actual: " + modoActual);
            }
        }
    }

    private void IniciarRutinaDeAtaque()
    {
        DetenerAtaque();

        if (modoActual == ModoAtaque.CuerpoACuerpo)
            rutinaAtaque = StartCoroutine(RutinaAtaqueCuerpoACuerpo());
        else
            rutinaAtaque = StartCoroutine(RutinaAtaqueDistancia());
    }

    private IEnumerator RutinaAtaqueCuerpoACuerpo()
    {
        while (true)
        {
            if (objetivo != null)
            {
                float distancia = Vector2.Distance(transform.position, objetivo.position);
                if (distancia <= rangoAtaqueCaC)
                {
                    Debug.Log("Atacando al enemigo con cuerpo a cuerpo");
                    enemigoScript.HacerDano(dano);
                }
                else
                {
                    Debug.Log("Jugador fuera de rango cuerpo a cuerpo");
                }
            }

            yield return new WaitForSeconds(tiempoAtaqueCaC);
        }
    }


    private IEnumerator RutinaAtaqueDistancia()
    {
        while (true)
        {
            Debug.Log("Atacando al enemigo con ataque a distancia");
            if (prefabBala != null && puntoDeDisparo != null)
            {
                GameObject proyectil = Instantiate(prefabBala, puntoDeDisparo.position, Quaternion.identity);
                Proyectil p = proyectil.GetComponent<Proyectil>();
                if (p != null)
                {
                    p.Inicializar(direccionDisparo.normalized);
                }
            }

            yield return new WaitForSeconds(intervaloDisparo);
        }
    }

    public void DetenerTodosLosAtaques()
    {
        DetenerAtaque();

        if (rutinaPrincipal != null)
        {
            StopCoroutine(rutinaPrincipal);
            rutinaPrincipal = null;
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

    public bool EstaEnModoCuerpoACuerpo()
    {
        return modoActual == ModoAtaque.CuerpoACuerpo;
    }

    public void IniciarAtaqueCuerpoACuerpo()
    {
        if (modoActual == ModoAtaque.CuerpoACuerpo && rutinaAtaque == null)
            rutinaAtaque = StartCoroutine(RutinaAtaqueCuerpoACuerpo());
    }

    public void IniciarAtaqueDistancia(Vector2 direccion)
    {
        if (modoActual == ModoAtaque.Distancia && rutinaAtaque == null)
        {
            direccionDisparo = direccion.normalized;
            rutinaAtaque = StartCoroutine(RutinaAtaqueDistancia());
        }
    }
}
