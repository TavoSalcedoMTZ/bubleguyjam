using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    public MaskTransition maskTransition; // Referencia al script de la transici�n de m�scara
    public BackgroundManager backgroundManager; // Referencia al BackgroundManager
    private bool portalActivated = false; // Flag para evitar activaci�n m�ltiple
    public EnemyManager enemyManager; // Referencia al EnemyManager
    public passWorld passWorld; // Cambi� el nombre para reflejar una clase de tipo PassWorld (aseg�rate de que esta clase exista y est� configurada correctamente)
    public int index1 = 1;  // Comienza en la etapa 1
    public SpeedTimer timer;

    private void Start()
    {
        // Asignamos autom�ticamente el EnemyManager si no est� asignado en el Inspector
        if (enemyManager == null)
        {
            enemyManager = FindObjectOfType<EnemyManager>(); // Buscar� el EnemyManager en la escena
            if (enemyManager == null)
            {
                Debug.LogError("No se encontr� un EnemyManager en la escena.");
            }
        }
        // Asignamos autom�ticamente el EnemyManager si no est� asignado en el Inspector
        if (timer == null)
        {
            timer = FindObjectOfType<SpeedTimer>(); // Buscar� el EnemyManager en la escena
            if (timer == null)
            {
                Debug.LogError("No se encontr� un EnemyManager en la escena.");
            }
        }

        // Asignamos autom�ticamente PassWorld si no est� asignado en el Inspector
        if (passWorld == null)
        {
            passWorld = FindObjectOfType<passWorld>(); // Buscar� el PassWorld en la escena
            if (passWorld == null)
            {
                Debug.LogError("No se encontr� un PassWorld en la escena.");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Target") && portalActivated) // Verifica si es el jugador y que el portal no haya sido activado ya
        {
            if (passWorld.transportlevel)  // Si transportlevel est� activado
            {
                timer.RestartTimer();
                index1++;  // Aumentamos el �ndice para cargar la siguiente escena
                LoadScene();  // Cargamos la nueva escena seg�n el �ndice
            }
            else
            {
                Debug.Log("�Portal activado!");

                // Inicia la transici�n con la m�scara
                if (maskTransition != null)
                {
                    // Llama a la transici�n, pasando el cambio de fondo como callback
                    StartCoroutine(maskTransition.PerformTransition(() => backgroundManager.ChangeBackground()));
                }
                else
                {
                    Debug.LogError("MaskTransition no est� asignado en el portal.");
                }
            }

            enemyManager.stageFree = false;
        }
    }

    // Si quieres permitir que el portal se pueda reutilizar, reseteamos el flag al salir del �rea
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Target"))
        {
            portalActivated = false; // Reseteamos la bandera cuando el jugador sale del portal
        }
    }

    private void Update()
    {
        if (enemyManager.stageFree)
        {
            portalActivated = true; // Solo se activa el portal si stageFree es true
        }
    }

    public void LoadScene()
    {
        // Verificamos cu�l es el valor del �ndice y cargamos la escena correspondiente
        string sceneName = "";

        if (index1 == 2)
        {
            sceneName = "Etapa2";  // Nombre de la siguiente escena
        }
        else if (index1 == 3)
        {
            sceneName = "Etapa3";  // Nombre de la siguiente escena
        }
        else
        {
            Debug.Log("No hay m�s etapas definidas.");
            return;
        }

        // Verificar que la escena est� incluida en Build Settings antes de intentar cargarla
        if (SceneUtility.GetBuildIndexByScenePath("Assets/Scenes/" + sceneName + ".unity") == -1)
        {
            Debug.LogError("La escena " + sceneName + " no est� en Build Settings. Aseg�rate de agregarla.");
            return;
        }

        // Cargar la escena
        Debug.Log("Cargando la escena: " + sceneName);
        SceneManager.LoadScene(sceneName);
    }
}
