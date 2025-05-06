using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PanelDaño : MonoBehaviour
{
    public float fadeDuration = 0.2f;
    public float maxOpacity = 0.3f;
    public Image panelDaño;
    public Camera mainCamera;
    public float shakeIntensity = 0.1f;
    public float shakeDuration = 0.2f;

    private bool estaMostrando = false;

    public void MostrarDaño()
    {
        if (!estaMostrando)
            StartCoroutine(DañoPanel());
    }

    IEnumerator DañoPanel()
    {
        estaMostrando = true;

        // Agitar la cámara
        StartCoroutine(SacudirCamara());

        float time = 0f;
        Color originalColor = panelDaño.color;
        originalColor.a = 0f;
        panelDaño.color = originalColor;
        panelDaño.gameObject.SetActive(true);

        // Subir opacidad (0 → 0.3)
        while (time < fadeDuration)
        {
            float alpha = Mathf.Lerp(0f, maxOpacity, time / fadeDuration);
            panelDaño.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            time += Time.deltaTime;
            yield return null;
        }
        panelDaño.color = new Color(originalColor.r, originalColor.g, originalColor.b, maxOpacity);

        yield return new WaitForSeconds(0.2f);

        // Bajar opacidad (0.3 → 0)
        time = 0f;
        while (time < fadeDuration)
        {
            float alpha = Mathf.Lerp(maxOpacity, 0f, time / fadeDuration);
            panelDaño.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            time += Time.deltaTime;
            yield return null;
        }

        panelDaño.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0f);


        estaMostrando = false;
    }

    IEnumerator SacudirCamara()
    {
        Vector3 originalPos = mainCamera.transform.position;
        float elapsed = 0f;

        while (elapsed < shakeDuration)
        {
            float offsetX = Random.Range(-shakeIntensity, shakeIntensity);
            float offsetY = Random.Range(-shakeIntensity, shakeIntensity);
            mainCamera.transform.position = originalPos + new Vector3(offsetX, offsetY, 0f);

            elapsed += Time.deltaTime;
            yield return null;
        }

        mainCamera.transform.position = originalPos;
    }
}
