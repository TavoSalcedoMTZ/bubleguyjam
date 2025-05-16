using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class BackgroundManager : MonoBehaviour
{
    public GameObject[] backgrounds;           // Array de fondos
    public Transform[] spawnPoints;            // Puntos de spawn para el jugador
    public Spawners spawner;

    private GameObject currentBackground;      // Fondo actual en la escena
    private Camera mainCamera;                 // Referencia a la cámara principal
    private List<int> usedBackgrounds = new List<int>(); // Fondos ya usados
    private int maxBackgroundsToShow = 7;      // Máximo número de fondos que se pueden mostrar
    private int backgroundCount = 0;           // Contador para los fondos mostrados
    private bool isTransitioning = false;      // Bandera para verificar si una transición está en curso
    public GameObject BordesDelMapa;

    [SerializeField] public MaskTransition maskTransition; // Referencia al script de transición

    public GameObject predefinedEnemy;         // El enemigo que se transformará en el séptimo fondo
    public Transform newTransformPosition;     // Nueva posición o transformación para el enemigo
    public EnemyManager enemyManager;         // Referencia al EnemyManager
    public GameObject loadingScreen;


    private void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        // Reiniciar estado
        enemyManager=FindFirstObjectByType<EnemyManager>();

        usedBackgrounds.Clear();
        BordesDelMapa.SetActive(true); // Desactivar bordes del mapa
        backgroundCount = 0;
        isTransitioning = false;

        if (currentBackground != null)
        {
            Destroy(currentBackground);
            currentBackground = null;
        }

        mainCamera = Camera.main;

        if (mainCamera == null)
        {
            Debug.LogError("No se encontró ninguna cámara principal etiquetada como 'MainCamera'.");
            return;
        }

        // Iniciar la lógica como si fuera Start()
        StartCoroutine(ChangeBackgroundWithTransition());
    }

   public void Limpiar()
    {
        if (currentBackground != null)
        {
            Destroy(currentBackground);
            currentBackground = null;
        }
        usedBackgrounds.Clear();
        backgroundCount = 0;
        isTransitioning = false;
            BordesDelMapa.SetActive(false);
        
    }
    public IEnumerator ChangeBackgroundWithTransition()
    {
        yield return maskTransition.PerformTransition(ChangeBackground);
    }

    public void ChangeBackground()
    {
        if (usedBackgrounds.Count >= maxBackgroundsToShow)
        {
            Debug.Log("Ya se han mostrado los 7 fondos permitidos.");
            ChangeEnemyTransform();
            return;
        }

        if (currentBackground != null)
        {
            Destroy(currentBackground);
        }

        int randomIndex;
        do
        {
            randomIndex = Random.Range(0, backgrounds.Length);
        } while (usedBackgrounds.Contains(randomIndex));

        usedBackgrounds.Add(randomIndex);

        GameObject selectedBackground = backgrounds[randomIndex];

        if (selectedBackground == null)
        {
            Debug.LogError($"El fondo en el índice {randomIndex} es nulo.");
            return;
        }

        Vector3 backgroundPosition = mainCamera.transform.position;
        backgroundPosition.z = 100f;
        currentBackground = Instantiate(selectedBackground, backgroundPosition, Quaternion.identity);
        currentBackground.transform.parent = mainCamera.transform;

        if (backgroundCount <= 6)
        {
            spawner.SpawearEnemigos();
        }

        Debug.Log($"Fondo cambiado al fondo {randomIndex + 1}");
        SpawnPlayer(randomIndex);
        StartCoroutine(UpdatePathFinder());

        backgroundCount++;
    }

    private IEnumerator UpdatePathFinder()
    {
   
        loadingScreen.SetActive(true);

     
        yield return null;

        AstarPath.active.Scan();

        yield return null;

    
        loadingScreen.SetActive(false);
    }

    private void SpawnPlayer(int backgroundIndex)
    {
        if (spawnPoints == null || spawnPoints.Length == 0 || spawnPoints[backgroundIndex] == null)
        {
            Debug.LogError("No se encontró el punto de spawn para este fondo.");
            return;
        }

        GameObject player = GameObject.FindWithTag("Target");
        if (player != null)
        {
            Vector3 spawnPosition = spawnPoints[backgroundIndex].position;
            spawnPosition.z = 0f;
            player.transform.position = spawnPosition;
        }
        else
        {
            Debug.LogError("No se encontró al jugador en la escena.");
        }
    }

    private void ChangeEnemyTransform()
    {
        if (predefinedEnemy != null && newTransformPosition != null)
        {
            predefinedEnemy.transform.position = newTransformPosition.position;

            enemyManager.totalEnemiesAlive++;

        }
        else
        {
            Debug.LogError("El enemigo predefinido o la nueva posición no están asignados correctamente.");
        }
    }

    public IEnumerator ChangeBackgroundWithFade()
    {
        if (isTransitioning) yield break;

        isTransitioning = true;

        yield return StartCoroutine(maskTransition.PerformTransition(() => {
            ChangeBackground();
        }));

        isTransitioning = false;
    }
}
