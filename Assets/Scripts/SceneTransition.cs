using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTransition : MonoBehaviour
{
    public Image fadeImage; // La imagen negra que se usar� para el fade
    public float fadeSpeed = 1.0f; // Velocidad de la transici�n
    private bool isFading = false;

    // Inicia la transici�n de fade out y carga la nueva escena
    public void FadeOutAndLoadScene(string sceneName)
    {
        StartCoroutine(FadeOutAndLoad(sceneName));
    }

    // Fade out (pantalla oscurece) y carga la nueva escena
    private IEnumerator FadeOutAndLoad(string sceneName)
    {
        isFading = true;

        // Aseguramos que la imagen de transici�n reciba raycast durante el fade
        fadeImage.raycastTarget = true;

        // Fade out: pantalla se oscurece
        float elapsedTime = 0f;
        Color color = fadeImage.color;
        while (elapsedTime < fadeSpeed)
        {
            color.a = Mathf.Lerp(0, 1, elapsedTime / fadeSpeed);
            fadeImage.color = color;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        color.a = 1;
        fadeImage.color = color;

        // Ahora cargamos la nueva escena de forma asincr�nica
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        // No permitir la activaci�n de la escena hasta despu�s del fade in
        asyncLoad.allowSceneActivation = false;

        // Comienza la carga de la nueva escena, pero no activarla a�n
        asyncLoad.allowSceneActivation = true;

        // Realizamos el fade in inmediatamente despu�s de la carga de la escena
        StartCoroutine(FadeIn());
    }

    // Fade in (pantalla se aclara)
    private IEnumerator FadeIn()
    {
        float elapsedTime = 0f;
        Color color = fadeImage.color;

        // Fade in: pantalla se aclara
        while (elapsedTime < fadeSpeed)
        {
            color.a = Mathf.Lerp(1, 0, elapsedTime / fadeSpeed);
            fadeImage.color = color;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        color.a = 0;
        fadeImage.color = color;

        isFading = false;
    }

    // Llamar a esta funci�n al inicio de la escena para hacer un fade in
    public void StartFadeIn()
    {
        StartCoroutine(FadeIn());
    }
}