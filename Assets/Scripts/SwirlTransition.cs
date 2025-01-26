using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwirlTransition : MonoBehaviour
{
    public Material swirlMaterial;
    public float swirlAmount = 0.5f;
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (swirlMaterial != null)
        {
            swirlMaterial.SetFloat("_SwirlAmount", swirlAmount);
            Graphics.Blit(source, destination, swirlMaterial);
        }
        else
        {
            Graphics.Blit(source, destination);
        }
    }

    public void StartTransition(float targetSwirlAmount, float duration, System.Action onComplete)
    {
        StartCoroutine(TransitionCoroutine(targetSwirlAmount, duration, onComplete));
    }

    private IEnumerator TransitionCoroutine(float targetSwirlAmount, float duration, System.Action onComplete)
    {
        float startSwirlAmount = swirlAmount;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            swirlAmount = Mathf.Lerp(startSwirlAmount, targetSwirlAmount, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        swirlAmount = targetSwirlAmount;

        // Llamar al callback después de la transición
        onComplete?.Invoke();
    }
}
