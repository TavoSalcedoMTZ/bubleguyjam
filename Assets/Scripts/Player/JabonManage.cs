using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JabonManage : MonoBehaviour
{
    public int Jabon = 15; // Cantidad inicial de jabón
    private DisparoPlayer player; // Referencia al script del jugador
    public Transform playerTransf; // Transform del jugador para escalar
    private Vector3 initialScale; // Escala inicial del jugador
    private float maxJabon = 15f; // Valor máximo de jabón

    // Prefabs originales de referencia (no se escalan)
    public List<Transform> burbujasOriginales;

    // Instancias escalables en la escena
    public List<Transform> burbujasEscalables;

    void Start()
    {
        // Obtén el componente del jugador
        player = GetComponent<DisparoPlayer>();

        // Guarda la escala inicial del jugador
        initialScale = playerTransf.localScale;

        // Asegúrate de que hay la misma cantidad de originales y escalables
        if (burbujasOriginales.Count != burbujasEscalables.Count)
        {
            Debug.LogError("La cantidad de BurbujasOriginales y BurbujasEscalables debe coincidir.");
            return;
        }

        // Establece la escala inicial de las burbujas escalables según las originales
        for (int i = 0; i < burbujasEscalables.Count; i++)
        {
            if (burbujasEscalables[i] != null && i < burbujasOriginales.Count && burbujasOriginales[i] != null)
            {
                burbujasEscalables[i].localScale = burbujasOriginales[i].localScale;
            }
        }
    }

    void Update()
    {
        if (Jabon <= 0)
        {
            Debug.Log("Te has quedado sin jabón");
            player.canShoot = false; // Desactiva la capacidad de disparar
        }
    }

    public void JabonDicrese(int _cantidadjabon)
    {
        // Reduce la cantidad de jabón
        Jabon -= _cantidadjabon;
        Jabon = Mathf.Max(Jabon, 0); // Asegura que no sea menor a 0

        UpdateScales(); // Actualiza las escalas
    }

    public void JabonIncremense(int _cantidadjabon)
    {
        // Aumenta la cantidad de jabón
        Jabon += _cantidadjabon;
        Jabon = Mathf.Min(Jabon, (int)maxJabon); // Asegura que no supere el máximo

        UpdateScales(); // Actualiza las escalas
    }

    private void UpdateScales()
    {
        // Calcula el factor de escala basado en el valor de jabón
        float scaleFactor = Mathf.InverseLerp(0, maxJabon, Jabon);

        // Escala el jugador linealmente
        playerTransf.localScale = Vector3.Lerp(Vector3.zero, initialScale, scaleFactor);

        // Escala las burbujas escalables en función de las burbujas originales
        for (int i = 0; i < burbujasEscalables.Count; i++)
        {
            if (burbujasEscalables[i] != null && i < burbujasOriginales.Count && burbujasOriginales[i] != null)
            {
                // Escala cada burbuja escalable basándose en la escala de su burbuja original
                Vector3 originalScale = burbujasOriginales[i].localScale;
                burbujasEscalables[i].localScale = Vector3.Lerp(Vector3.zero, originalScale, scaleFactor);
            }
        }
    }
}
