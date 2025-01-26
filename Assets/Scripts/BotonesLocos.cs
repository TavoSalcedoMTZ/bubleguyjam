using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;


public class ButtonEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public float inflateScale = 1.2f;          // Escala al pasar el rat�n por encima (para efectos previos al clic)
    public float inflateDuration = 0.2f;       // Duraci�n de la animaci�n de inflado
    public float shrinkScale = 0.1f;           // Escala m�nima cuando "explota" (como si se pinchara)
    public float shrinkDuration = 0.2f;        // Duraci�n de la explosi�n (reducci�n del tama�o)
    private Vector3 originalScale;             // Guarda el tama�o original del bot�n
    private CanvasGroup canvasGroup;           // CanvasGroup para manejar visibilidad e interactividad

    void Start()
    {
        originalScale = transform.localScale;  // Guarda el tama�o original del bot�n
        canvasGroup = GetComponent<CanvasGroup>(); // Obt�n el CanvasGroup para controlar la visibilidad
        if (canvasGroup == null)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>(); // Si no tiene un CanvasGroup, lo a�adimos
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Inflar el bot�n al pasar el rat�n
        StartCoroutine(ScaleButton(originalScale * inflateScale, inflateDuration));
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Volver al tama�o original cuando el rat�n sale
        StartCoroutine(ScaleButton(originalScale, inflateDuration));
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // Efecto de "explosi�n" del bot�n (se hace peque�o como si fuera pinchado)
        StartCoroutine(ShrinkButton());
    }

    private IEnumerator ScaleButton(Vector3 targetScale, float duration)
    {
        Vector3 startScale = transform.localScale;
        float elapsedTime = 0f;

        // Animaci�n de escalado (inflar o desinflar)
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
        // Escala el bot�n hacia el tama�o m�nimo, como si estuviera pinchado
        yield return ScaleButton(originalScale * shrinkScale, shrinkDuration);

        // Al llegar al tama�o m�nimo, hacer el bot�n invisible (simulando que ha explotado)
        SetButtonVisibility(false);

        // Espera un peque�o tiempo para dar el efecto de que el bot�n "explot�"
        yield return new WaitForSeconds(0.1f);

        // El bot�n permanece desactivado (simula que ha explotado)
        // Si quieres que vuelva a aparecer despu�s, puedes descomentar esta l�nea
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
