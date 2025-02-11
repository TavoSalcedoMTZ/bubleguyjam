using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    public MaskTransition maskTransition; // Referencia al script de la transición de máscara
    public BackgroundManager backgroundManager; // Referencia al BackgroundManager
    private bool portalActivated = false; // Flag para evitar activación múltiple
    public EnemyManager enemyManager; // Referencia al EnemyManager
    public passWorld passWorld; // Cambié el nombre para reflejar una clase de tipo PassWorld (asegúrate de que esta clase exista y esté configurada correctamente)
    public int index1 = 1;  // Comienza en la etapa 1
    public SpeedTimer timer;

    private void Start()
    {
        // Asignamos automáticamente el EnemyManager si no está asignado en el Inspector
        if (enemyManager == null)
        {
            enemyManager = FindObjectOfType<EnemyManager>(); // Buscará el EnemyManager en la escena
            if (enemyManager == null)
            {
                Debug.LogError("No se encontró un EnemyManager en la escena.");
            }
        }
        // Asignamos automáticamente el EnemyManager si no está asignado en el Inspector
        if (timer == null)
        {
            timer = FindObjectOfType<SpeedTimer>(); // Buscará el EnemyManager en la escena
            if (timer == null)
            {
                Debug.LogError("No se encontró un EnemyManager en la escena.");
            }
        }

        // Asignamos automáticamente PassWorld si no está asignado en el Inspector
        if (passWorld == null)
        {
            passWorld = FindObjectOfType<passWorld>(); // Buscará el PassWorld en la escena
            if (passWorld == null)
            {
                Debug.LogError("No se encontró un PassWorld en la escena.");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Target") && portalActivated) // Verifica si es el jugador y que el portal no haya sido activado ya
        {
            if (passWorld.transportlevel)  // Si transportlevel está activado
            {
                timer.RestartTimer();
                index1++;  // Aumentamos el índice para cargar la siguiente escena
                LoadScene();  // Cargamos la nueva escena según el índice
            }
            else
            {
                Debug.Log("¡Portal activado!");

                // Inicia la transición con la máscara
                if (maskTransition != null)
                {
                    // Llama a la transición, pasando el cambio de fondo como callback
                    StartCoroutine(maskTransition.PerformTransition(() => backgroundManager.ChangeBackground()));
                }
                else
                {
                    Debug.LogError("MaskTransition no está asignado en el portal.");
                }
            }

            enemyManager.stageFree = false;
        }
    }

    // Si quieres permitir que el portal se pueda reutilizar, reseteamos el flag al salir del área
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
        // Verificamos cuál es el valor del índice y cargamos la escena correspondiente
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
            Debug.Log("No hay más etapas definidas.");
            return;
        }

        // Verificar que la escena está incluida en Build Settings antes de intentar cargarla
        if (SceneUtility.GetBuildIndexByScenePath("Assets/Scenes/" + sceneName + ".unity") == -1)
        {
            Debug.LogError("La escena " + sceneName + " no está en Build Settings. Asegúrate de agregarla.");
            return;
        }

        // Cargar la escena
        Debug.Log("Cargando la escena: " + sceneName);
        SceneManager.LoadScene(sceneName);
    }
}
