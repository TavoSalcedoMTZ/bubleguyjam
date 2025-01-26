using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class TransitionManager : MonoBehaviour
{
    public Material transitionMaterial; // Material para el efecto
    private bool isTransitioning = false; // Bandera para evitar interrupciones

    public float transitionDuration = 2.0f; // Duración de la transición
    public float maxSwirlAmount = 1.0f; // Cantidad máxima de remolino
    public float maxBlurAmount = 1.0f; // Cantidad máxima de desenfoque

    private void Awake()
    {
        // Si no se asigna el material desde el Inspector, cargamos el material del remolino
        if (transitionMaterial == null)
        {
            transitionMaterial = new Material(Shader.Find("Custom/SwirlBlur"));
        }
    }

    private void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        if (isTransitioning)
        {
            // Ajustar los valores del remolino y el desenfoque
            float progress = Mathf.Clamp01(transitionMaterial.GetFloat("_Progress"));
            transitionMaterial.SetFloat("_SwirlAmount", Mathf.Lerp(0.0f, maxSwirlAmount, progress));
            transitionMaterial.SetFloat("_BlurAmount", Mathf.Lerp(0.0f, maxBlurAmount, progress));

            // Aplicar el material del remolino
            Graphics.Blit(src, dest, transitionMaterial);
        }
        else
        {
            // Renderizar la imagen normal
            Graphics.Blit(src, dest);
        }
    }

    public void StartTransition(Action onTransitionComplete)
    {
        if (isTransitioning)
        {
            Debug.LogWarning("¡Ya hay una transición en curso!");
            return;
        }

        StartCoroutine(TransitionCoroutine(onTransitionComplete));
    }

    private IEnumerator TransitionCoroutine(Action onTransitionComplete)
    {
        isTransitioning = true;

        float elapsed = 0f;
        while (elapsed < transitionDuration)
        {
            elapsed += Time.deltaTime;

            // Ajustar el progreso de la transición en el shader
            float progress = Mathf.Clamp01(elapsed / transitionDuration);
            transitionMaterial.SetFloat("_Progress", progress);

            yield return null;
        }

        // Finalizar la transición
        isTransitioning = false;

        // Callback: Cambiar el fondo
        onTransitionComplete?.Invoke();

        // Iniciar el desenfoque de regreso a la normalidad
        StartCoroutine(ReverseTransition());
    }

    private IEnumerator ReverseTransition()
    {
        float elapsed = 0f;
        while (elapsed < transitionDuration)
        {
            elapsed += Time.deltaTime;

            // Ajustar el progreso de regreso en el shader
            float progress = Mathf.Clamp01(1f - (elapsed / transitionDuration));
            transitionMaterial.SetFloat("_Progress", progress);

            yield return null;
        }
    }
}
