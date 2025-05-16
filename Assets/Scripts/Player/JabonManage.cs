using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // Necesario para trabajar con UI

public class JabonManage : MonoBehaviour
{
    public int Jabon = 15; // Cantidad inicial de jab�n
    private DisparoPlayer player; // Referencia al script del jugador
    public Transform playerTransf; // Transform del jugador para escalar
    private Vector3 initialScale; // Escala inicial del jugador
    public int maxJabon = 15; // Valor m�ximo de jab�n

    // Prefabs originales de referencia (no se escalan)
    public List<Transform> burbujasOriginales;

    // Instancias escalables en la escena
    public List<Transform> burbujasEscalables;

    // Referencia a la imagen de tipo Filled (filler)
    public Image jabonFiller;
    public PanelDa�o panelDa�o; // Referencia al panel de da�o
    public GameObject playerRender;

    void Start()
    {
        // Obt�n el componente del jugador
        player = GetComponent<DisparoPlayer>();

        // Guarda la escala inicial del jugador
        initialScale = playerTransf.localScale;

        // Aseg�rate de que hay la misma cantidad de originales y escalables
        if (burbujasOriginales.Count != burbujasEscalables.Count)
        {
            Debug.LogError("La cantidad de BurbujasOriginales y BurbujasEscalables debe coincidir.");
            return;
        }

        // Establece la escala inicial de las burbujas escalables seg�n las originales
        for (int i = 0; i < burbujasEscalables.Count; i++)
        {
            if (burbujasEscalables[i] != null && i < burbujasOriginales.Count && burbujasOriginales[i] != null)
            {
                burbujasEscalables[i].localScale = burbujasOriginales[i].localScale;
            }
        }

        // Aseg�rate de que el Filler (Image) est� configurado correctamente
        if (jabonFiller != null)
        {
            jabonFiller.fillAmount = Mathf.InverseLerp(0, maxJabon, Jabon); // Inicializa el llenado de la imagen
        }
    }

    void Update()
    {
        if (Jabon <= 0)
        {
            Debug.Log("Te has quedado sin jab�n");
            player.canShoot = false; // Desactiva la capacidad de disparar
            string scene_name = "Game Over";
            SceneManager.LoadSceneAsync(scene_name);
        }

        // Actualiza el Filler con el valor del jab�n
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
            panelDa�o.MostrarDa�o();
        }
    }
    public void Da�oObstaculos(int da�o)
    {
        JabonDicrese(da�o, false);   
    }

    public void JabonIncremense(int _cantidadjabon)
    {
        // Aumenta la cantidad de jab�n
        Jabon += _cantidadjabon;
        Jabon = Mathf.Min(Jabon, (int)maxJabon); // Asegura que no supere el m�ximo

        UpdateScales(); // Actualiza las escalas
    }

    private void UpdateScales()
    {
        float scaleFactor = Mathf.InverseLerp(0, maxJabon, Jabon);

        // Escala m�nima del jugador
        Vector3 minScale = initialScale * 0.456f;
        playerRender.transform.localScale = Vector3.Lerp(minScale, initialScale, scaleFactor);

        // Escalar burbujas
        for (int i = 0; i < burbujasEscalables.Count; i++)
        {
            if (burbujasEscalables[i] != null && i < burbujasOriginales.Count && burbujasOriginales[i] != null)
            {
                Vector3 originalScale = burbujasOriginales[i].localScale;
                Vector3 minBubbleScale = originalScale * 0.456f;
                burbujasEscalables[i].localScale = Vector3.Lerp(minBubbleScale, originalScale, scaleFactor);
            }
        }
    }
}
