using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;


public class ButtonEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public float inflateScale = 1.2f;          // Escala al pasar el ratón por encima (para efectos previos al clic)
    public float inflateDuration = 0.2f;       // Duración de la animación de inflado
    public float shrinkScale = 0.1f;           // Escala mínima cuando "explota" (como si se pinchara)
    public float shrinkDuration = 0.2f;        // Duración de la explosión (reducción del tamaño)
    private Vector3 originalScale;             // Guarda el tamaño original del botón
    private CanvasGroup canvasGroup;           // CanvasGroup para manejar visibilidad e interactividad

    void Start()
    {
        originalScale = transform.localScale;  // Guarda el tamaño original del botón
        canvasGroup = GetComponent<CanvasGroup>(); // Obtén el CanvasGroup para controlar la visibilidad
        if (canvasGroup == null)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>(); // Si no tiene un CanvasGroup, lo añadimos
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Inflar el botón al pasar el ratón
        StartCoroutine(ScaleButton(originalScale * inflateScale, inflateDuration));
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Volver al tamaño original cuando el ratón sale
        StartCoroutine(ScaleButton(originalScale, inflateDuration));
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // Efecto de "explosión" del botón (se hace pequeño como si fuera pinchado)
        StartCoroutine(ShrinkButton());
    }

    private IEnumerator ScaleButton(Vector3 targetScale, float duration)
    {
        Vector3 startScale = transform.localScale;
        float elapsedTime = 0f;

        // Animación de escalado (inflar o desinflar)
        while (elapsedTime < duration)
        {
            transform.localScale = Vector3.Lerp(startScale, targetScale, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.localScale = targetScale;
    }

    private IEnumerator ShrinkButton()
    {
        // Escala el botón hacia el tamaño mínimo, como si estuviera pinchado
        yield return ScaleButton(originalScale * shrinkScale, shrinkDuration);

        // Al llegar al tamaño mínimo, hacer el botón invisible (simulando que ha explotado)
        SetButtonVisibility(false);

        // Espera un pequeño tiempo para dar el efecto de que el botón "explotó"
        yield return new WaitForSeconds(0.1f);

        // El botón permanece desactivado (simula que ha explotado)
        // Si quieres que vuelva a aparecer después, puedes descomentar esta línea
        // SetButtonVisibility(true); 
    }

    private void SetButtonVisibility(bool isVisible)
    {
        if (canvasGroup != null)
        {
            // Cambia la visibilidad y la interactividad
            canvasGroup.alpha = isVisible ? 1f : 0f;
            canvasGroup.interactable = isVisible;
            canvasGroup.blocksRaycasts = isVisible;
        }
    }
}
