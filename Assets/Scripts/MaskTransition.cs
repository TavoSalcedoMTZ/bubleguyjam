using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskTransition : MonoBehaviour
{
    [SerializeField] private Transform maskTransform;           // Objeto que actúa como máscara
    [SerializeField] private SpriteRenderer maskColor;          // Controla el color y opacidad de la máscara
    [SerializeField] private float transitionSpeed = 2f;        // Velocidad de la transición
    [SerializeField] private float fadeDuration = 1f;           // Duración del fade

    private Vector3 startScale;  // Escala inicial de la máscara (invisible)
    private Vector3 endScale;    // Escala final de la máscara (visible)

    private void Awake()
    {
        // Configuración inicial de la máscara
        startScale = new Vector3(0, 0, 1); // Escala inicial invisible
        endScale = new Vector3(1, 1, 1);  // Escala máxima visible
        ResetMask();
    }

    /// <summary>
    /// Inicia la transición con fade in, ejecuta una acción y luego fade out.
    /// </summary>
    public IEnumerator PerformTransition(System.Action onComplete)
    {
        yield return StartCoroutine(FadeInMask());
        onComplete?.Invoke(); // Llama al método que cambiará el fondo
        yield return StartCoroutine(FadeOutMask());
    }

    /// <summary>
    /// Reinicia la máscara a su estado inicial.
    /// </summary>
    public void ResetMask()
    {
        maskTransform.localScale = startScale;
        maskColor.color = new Color(0, 0, 0, 0); // Completamente transparente
    }

    /// <summary>
    /// Realiza un fade in de la máscara.
    /// </summary>
    private IEnumerator FadeInMask()
    {
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            float t = elapsedTime / fadeDuration;
            maskTransform.localScale = Vector3.Lerp(startScale, endScale, t);
            maskColor.color = new Color(0, 0, 0, Mathf.Lerp(0f, 1f, t));
            elapsedTime += Time.deltaTime * transitionSpeed;
            yield return null;
        }

        maskTransform.localScale = endScale;
        maskColor.color = new Color(0, 0, 0, 1f); // Totalmente opaca
    }

    /// <summary>
    /// Realiza un fade out de la máscara.
    /// </summary>
    private IEnumerator FadeOutMask()
    {
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            float t = elapsedTime / fadeDuration;
            maskTransform.localScale = Vector3.Lerp(endScale, startScale, t);
            maskColor.color = new Color(0, 0, 0, Mathf.Lerp(1f, 0f, t));
            elapsedTime += Time.deltaTime * transitionSpeed;
            yield return null;
        }

        ResetMask(); // Regresa la máscara a su estado inicial
    }
}
