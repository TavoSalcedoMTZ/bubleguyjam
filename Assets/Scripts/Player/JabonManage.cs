using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // Necesario para trabajar con UI

public class JabonManage : MonoBehaviour
{
    public int Jabon = 15; // Cantidad inicial de jabón
    private DisparoPlayer player; // Referencia al script del jugador
    public Transform playerTransf; // Transform del jugador para escalar
    private Vector3 initialScale; // Escala inicial del jugador
    public int maxJabon = 15; // Valor máximo de jabón

    // Prefabs originales de referencia (no se escalan)
    public List<Transform> burbujasOriginales;

    // Instancias escalables en la escena
    public List<Transform> burbujasEscalables;

    // Referencia a la imagen de tipo Filled (filler)
    public Image jabonFiller;
    public PanelDaño panelDaño; // Referencia al panel de daño

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

        // Asegúrate de que el Filler (Image) esté configurado correctamente
        if (jabonFiller != null)
        {
            jabonFiller.fillAmount = Mathf.InverseLerp(0, maxJabon, Jabon); // Inicializa el llenado de la imagen
        }
    }

    void Update()
    {
        if (Jabon <= 0)
        {
            Debug.Log("Te has quedado sin jabón");
            player.canShoot = false; // Desactiva la capacidad de disparar
            string scene_name = "Game Over";
            SceneManager.LoadScene(scene_name);
        }

        // Actualiza el Filler con el valor del jabón
        if (jabonFiller != null)
        {
            jabonFiller.fillAmount = Mathf.InverseLerp(0, maxJabon, Jabon); // Cambia la cantidad de llenado
        }
    }

    public void JabonDicrese(int _cantidadjabon, bool? isPlayerAttack = null)
    {
        Jabon -= _cantidadjabon;
        Jabon = Mathf.Max(Jabon, 0);

        UpdateScales();

       
        if (isPlayerAttack != true)
        {
            panelDaño.MostrarDaño();
        }
    }
    public void DañoObstaculos(int daño)
    {
        JabonDicrese(daño, false);   
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
