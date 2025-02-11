using UnityEngine;
using System.Collections;

public class SpeedTimer : MonoBehaviour
{
    public float timerDuration = 10f;
    private float currentTime;
    private bool timerRunning = false;
    public EnemyManager enemyManager;
    private PlayerMovement originalScript;
    public GameObject[] bubbles;
    public float inflateTime = 1f;
    public float maxSize = 2f;
    private float explosionTime = 0.2f;
    public float waitTime = 3.33f;

    private void Start()
    {
       RestartTimer();
    }
    void Update()
    {
        if (timerRunning)
        {
            if (currentTime > 0)
            {
                currentTime -= Time.deltaTime;
            }
            else
            {
                currentTime = 0;
                TimerEnd();
            }

            if (enemyManager != null && enemyManager.totalEnemiesAlive == 0)
            {
                StopTimerEarly();
            }
        }
    }

    void TimerEnd()
    {
        timerRunning = false;
        if (originalScript != null)
        {
            originalScript.moveSpeed -= 1f;
        }
        Debug.Log("Tiempo terminado. Nueva velocidad: " + (originalScript != null ? originalScript.moveSpeed.ToString() : "N/A"));
    }

    public void StopTimerEarly()
    {
        if (timerRunning)
        {
            timerRunning = false;
            if (originalScript != null)
            {
                originalScript.moveSpeed += 2f;
            }
            Debug.Log("Todos los enemigos derrotados. Nueva velocidad: " + (originalScript != null ? originalScript.moveSpeed.ToString() : "N/A"));
            StartCoroutine(InflateAndExplodeBubbles());
        }
    }

    public void RestartTimer()
    {
        if (!timerRunning)
        {
            currentTime = timerDuration;
            timerRunning = true;
            if (originalScript != null)
            {
                originalScript.moveSpeed -= 2f; // Restablecer la velocidad
            }
            ResetBubbles(); // Reiniciar las burbujas
            Debug.Log("Temporizador reiniciado. Nueva velocidad: " + (originalScript != null ? originalScript.moveSpeed.ToString() : "N/A"));
        }
    }

    void ResetBubbles()
    {
        foreach (GameObject bubble in bubbles)
        {
            if (bubble != null)
            {
                bubble.SetActive(true); // Reactivar las burbujas desactivadas
                bubble.transform.localScale = Vector3.one; // Restablecer tamaño original
            }
        }
        Debug.Log("Burbujas reiniciadas.");
    }

    IEnumerator InflateAndExplodeBubbles()
    {
        for (int i = 0; i < bubbles.Length; i++)
        {
            yield return new WaitForSeconds(waitTime);
            yield return StartCoroutine(InflateBubble(bubbles[i]));
            yield return StartCoroutine(ExplodeBubble(bubbles[i]));
        }
    }

    IEnumerator InflateBubble(GameObject bubble)
    {
        if (bubble == null) yield break;
        Vector3 initialScale = Vector3.one;
        Vector3 targetScale = initialScale * maxSize;
        float elapsedTime = 0f;

        while (elapsedTime < inflateTime)
        {
            elapsedTime += Time.deltaTime;
            float scaleFactor = Mathf.Lerp(1f, maxSize, elapsedTime / inflateTime);
            bubble.transform.localScale = initialScale * scaleFactor;
            yield return null;
        }
        bubble.transform.localScale = targetScale;
    }

    IEnumerator ExplodeBubble(GameObject bubble)
    {
        if (bubble == null) yield break;
        Vector3 initialScale = bubble.transform.localScale;
        Vector3 targetScale = initialScale * 3f;
        float elapsedTime = 0f;

        while (elapsedTime < explosionTime)
        {
            elapsedTime += Time.deltaTime;
            float scaleFactor = Mathf.Lerp(1f, 3f, elapsedTime / explosionTime);
            bubble.transform.localScale = initialScale * scaleFactor;
            yield return null;
        }
        bubble.transform.localScale = targetScale;
        bubble.SetActive(false);
    }
}
