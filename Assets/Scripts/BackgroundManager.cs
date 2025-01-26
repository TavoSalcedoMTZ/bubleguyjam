using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
    public GameObject[] backgrounds;           // Array de fondos
    public Transform[] spawnPoints;            // Puntos de spawn para el jugador
    public Spawners spawner;

    private GameObject currentBackground;      // Fondo actual en la escena
    private Camera mainCamera;                 // Referencia a la c�mara principal
    private List<int> usedBackgrounds = new List<int>(); // Fondos ya usados
    private int maxBackgroundsToShow = 7;      // M�ximo n�mero de fondos que se pueden mostrar
    private int backgroundCount = 0;           // Contador para los fondos mostrados
    private bool isTransitioning = false;      // Bandera para verificar si una transici�n est� en curso

    [SerializeField] public MaskTransition maskTransition; // Referencia al script de transici�n

    // Enemigo predefinido para cambiar su transform
    public GameObject predefinedEnemy;         // El enemigo que se transformar� en el s�ptimo fondo
    public Transform newTransformPosition;     // Nueva posici�n o transformaci�n para el enemigo

    private void Start()
    {
        // Validar fondos y puntos de spawn
        if (backgrounds == null || backgrounds.Length == 0 || spawnPoints == null || spawnPoints.Length != backgrounds.Length)
        {
            Debug.LogError("Faltan fondos o puntos de spawn. Aseg�rate de asignar todos los fondos y puntos de spawn en el Inspector.");
            return;
        }

        mainCamera = Camera.main;
        if (mainCamera == null)
        {
            Debug.LogError("No se encontr� ninguna c�mara principal etiquetada como 'MainCamera'.");
            return;
        }

        StartCoroutine(ChangeBackgroundWithTransition());
    }

    public IEnumerator ChangeBackgroundWithTransition()
    {
        // Ejecuta la transici�n antes de cambiar el fondo
        yield return maskTransition.PerformTransition(ChangeBackground);
    }

    public void ChangeBackground()
    {
        // Evitar la creaci�n de m�s fondos si ya se han mostrado los 7 fondos permitidos
        if (usedBackgrounds.Count >= maxBackgroundsToShow)
        {
            Debug.Log("Ya se han mostrado los 7 fondos permitidos.");

            // Cambiar el transform del enemigo predefinido en lugar de spawnear enemigos
            ChangeEnemyTransform();
            return;  // Termina la ejecuci�n para evitar seguir creando fondos o enemigos
        }

        // Destruir el fondo actual si existe
        if (currentBackground != null)
        {
            Destroy(currentBackground);
        }

        // Elegir un fondo aleatorio que no haya sido usado
        int randomIndex;
        do
        {
            randomIndex = Random.Range(0, backgrounds.Length);
        } while (usedBackgrounds.Contains(randomIndex));

        usedBackgrounds.Add(randomIndex);

        // Seleccionar el fondo y verificar que no sea nulo
        GameObject selectedBackground = backgrounds[randomIndex];

        if (selectedBackground == null)
        {
            Debug.LogError($"El fondo en el �ndice {randomIndex} es nulo. Verifica tu array de backgrounds.");
            return;
        }

        // Instanciar el fondo en la posici�n correcta
        Vector3 backgroundPosition = mainCamera.transform.position;
        backgroundPosition.z = 100f; // Posicionar el fondo detr�s de la c�mara
        currentBackground = Instantiate(selectedBackground, backgroundPosition, Quaternion.identity);
        currentBackground.transform.parent = mainCamera.transform;

        // Solo spawn de enemigos hasta el sexto fondo
        if (backgroundCount < 6)
        {
            spawner.SpawearEnemigos();
        }

        Debug.Log($"Fondo cambiado al fondo {randomIndex + 1}");

        // Spawnear el jugador en la nueva posici�n
        SpawnPlayer(randomIndex);

        backgroundCount++; // Incrementamos el contador de fondos mostrados
    }

    private void SpawnPlayer(int backgroundIndex)
    {
        if (spawnPoints == null || spawnPoints.Length == 0 || spawnPoints[backgroundIndex] == null)
        {
            Debug.LogError("No se encontr� el punto de spawn para este fondo.");
            return;
        }

        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            Vector3 spawnPosition = spawnPoints[backgroundIndex].position;
            spawnPosition.z = 0f;
            player.transform.position = spawnPosition;
        }
        else
        {
            Debug.LogError("No se encontr� al jugador en la escena.");
        }
    }

    // M�todo para cambiar la posici�n de un enemigo predefinido cuando se llega al s�ptimo fondo
    private void ChangeEnemyTransform()
    {
        if (predefinedEnemy != null && newTransformPosition != null)
        {
            // Cambiar la posici�n o transform del enemigo predefinido
            predefinedEnemy.transform.position = newTransformPosition.position;
            predefinedEnemy.transform.rotation = newTransformPosition.rotation;
            predefinedEnemy.transform.localScale = newTransformPosition.localScale;
        }
        else
        {
            Debug.LogError("El enemigo predefinido o la nueva posici�n no est�n asignados correctamente.");
        }
    }

    public IEnumerator ChangeBackgroundWithFade()
    {
        // Evitar que se inicie una nueva transici�n si ya hay una en curso
        if (isTransitioning) yield break;

        isTransitioning = true;

        // Llamar a PerformTransition para manejar fade in, cambiar el fondo, y luego fade out
        yield return StartCoroutine(maskTransition.PerformTransition(() => {
            // Este c�digo se ejecuta despu�s del fade in
            ChangeBackground(); // Cambiar el fondo despu�s del fade in
        }));

        isTransitioning = false;  // Transici�n completada
    }
}
