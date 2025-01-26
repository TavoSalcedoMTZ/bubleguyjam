using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlackFadeTransition : MonoBehaviour
{
    public Image fadeImage; // Imagen negra que cubre la pantalla
    public float fadeDuration = 1.0f; // Duraci�n del fundido
    private bool isFading = false;

    private void Awake()
    {
        // Aseg�rate de que la imagen est� completamente transparente al principio
        if (fadeImage != null)
        {
            fadeImage.color = new Color(0f, 0f, 0f, 0f);
            fadeImage.gameObject.SetActive(false); // Desactivamos la imagen desde el inicio
        }
    }

    public void StartFadeIn(System.Action onFadeComplete)
    {
        if (fadeImage != null && !isFading)
        {
            fadeImage.gameObject.SetActive(true); // Activamos la imagen cuando comienza la transici�n
            StartCoroutine(FadeInCoroutine(onFadeComplete));
        }
    }

    private IEnumerator FadeInCoroutine(System.Action onFadeComplete)
    {
        isFading = true;
        float elapsedTime = 0f;

        // Fundir hacia el negro
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);
            fadeImage.color = new Color(0f, 0f, 0f, alpha);
            yield return null;
        }

        // Aseg�rate de que el color final sea completamente negro
        fadeImage.color = new Color(0f, 0f, 0f, 1f);

        // Callback despu�s del fundido
        onFadeComplete?.Invoke();

        // Esperar un momento antes de desvanecer
        yield return new WaitForSeconds(0.5f);
        StartFadeOut();
    }

    private void StartFadeOut()
    {
        if (fadeImage != null && !isFading)
        {
            StartCoroutine(FadeOutCoroutine());
        }
    }

    private IEnumerator FadeOutCoroutine()
    {
        float elapsedTime = 0f;
        // Desvanecer de vuelta a transparente
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            fadeImage.color = new Color(0f, 0f, 0f, alpha);
            yield return null;
        }

        // Aseg�rate de que el color final sea completamente transparente
        fadeImage.color = new Color(0f, 0f, 0f, 0f);
        isFading = false;
        fadeImage.gameObject.SetActive(false); // Desactivamos la imagen despu�s de completar el fundido
    }
}
