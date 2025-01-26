using UnityEngine;
using TMPro; // Para usar TextMeshPro

public class SpeedTimer : MonoBehaviour
{

    public TextMeshProUGUI timerText; // Referencia al texto del cronómetro
    public TextMeshProUGUI speedText; // Referencia al texto de la velocidad

    public float timerDuration = 10f; // Duración del cronómetro
    private float currentTime;
    private bool timerRunning = false;
    public EnemyManager enemyManager;

    private PlayerMovement originalScript; // Referencia al script original


    void Start()
    {
        currentTime = timerDuration;
        originalScript = FindObjectOfType<PlayerMovement>();

        timerRunning = true;

    }

    void Update()
    {
        if (timerRunning)
        {
            if (currentTime > 0)
            {
                currentTime -= Time.deltaTime;
                UpdateUI();
            }
            else
            {
                currentTime = 0;
                TimerEnd();
            }

            if (enemyManager.totalEnemiesAlive == 0)
            {
                StopTimerEarly();
            }

            
        }
    }



    void TimerEnd()
    {
        timerRunning = false;

        // Disminuir la velocidad si el tiempo termina
        if (originalScript != null)
        {
            originalScript.moveSpeed -= 1f;
        }

        UpdateUI();
        Debug.Log("Tiempo terminado. Nueva velocidad: " + originalScript.moveSpeed);
    }

    public void StopTimerEarly()
    {
        if (timerRunning)
        {
            timerRunning = false;

            // Aumentar la velocidad si todos los enemigos son derrotados
            if (originalScript != null)
            {
                originalScript.moveSpeed += 2f;
            }

            UpdateUI();
            Debug.Log("Todos los enemigos derrotados. Nueva velocidad: " + originalScript.moveSpeed);
        }
    }

    void UpdateUI()
    {
        // Actualizar el texto del cronómetro
        timerText.text = "Tiempo: " + currentTime.ToString("F2");

        // Actualizar el texto de la velocidad
        if (originalScript != null)
        {
            speedText.text = "Velocidad: " + originalScript.moveSpeed;
        }
    }
}
