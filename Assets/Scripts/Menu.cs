using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;
using System.Collections;
public class Menu : MonoBehaviour
{
    public string seleccion;  // Escena seleccionada por defecto
    public GameObject pausePanel;  // Panel de pausa
    private bool isPaused = false;  // Estado de pausa
    public Slider slider1, slider2, slider3;  // Sliders
    public VideoPlayer videoPlayer;  // Reproductor de video
    public GameObject transitionPanel;  // Panel de transición (con el video)
    public GameObject currentScenePanel;  // Panel de la escena actual (Menú)
    public GameObject whitePanel;  // Panel blanco

    public void Play(string sceneName)
    {
        SaveSliderValues();
        StartCoroutine(LoadSceneWhileTransition(sceneName));  // Iniciar la transición sin desactivar el menú
    }

    public void menu(string sceneName)
    {
        StartCoroutine(LoadSceneWhileTransition(sceneName));  // Iniciar la transición sin desactivar el menú
    }

    private IEnumerator LoadSceneWhileTransition(string sceneName)
    {
        // Inicia la carga asíncrona de la nueva escena
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        asyncLoad.allowSceneActivation = false;

        // Muestra el panel de transición (con el video)
        transitionPanel.SetActive(true);  // Asegúrate de que el panel de transición sea visible
        videoPlayer.Play();  // Reproduce el video de transición

        // Mientras se carga la nueva escena, mostramos la animación de transición
        while (!asyncLoad.isDone)
        {
            if (asyncLoad.progress >= 0.9f)
            {
                // Aquí verificamos si el video ha llegado al final
                if (videoPlayer.frame >= (long)videoPlayer.frameCount - 1)
                {
                    asyncLoad.allowSceneActivation = true;
                }
            }
            yield return null;
        }

        // Al terminar de cargar la escena, mostramos el panel blanco
        // Mueve el activado del whitePanel aquí, solo después de que el video termine
        whitePanel.SetActive(true);

        // Esperamos un breve momento para asegurarnos que la escena se activó
        yield return new WaitForSecondsRealtime(0.1f);

        // Reproducimos la animación de transición sobre el panel blanco
        StartCoroutine(PlayTransitionAnimation());
    }

    private IEnumerator PlayTransitionAnimation()
    {
        // Reproducimos el video de transición
        videoPlayer.Play();

        // Esperamos a que el video termine de reproducirse
        while (videoPlayer.isPlaying)
        {
            yield return null;
        }

        // Esperamos un pequeño tiempo adicional antes de desactivar el panel blanco
        yield return new WaitForSecondsRealtime(0.5f);
        whitePanel.SetActive(false);  // Desactivamos el panel blanco después de la transición
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Exit()
    {
        Debug.Log("Saliste");
        Application.Quit();
    }

    public void Pause()
    {
        TogglePause();
    }

    private void TogglePause()
    {
        if (isPaused)
        {
            Time.timeScale = 1f;
            pausePanel.SetActive(false);
        }
        else
        {
            Time.timeScale = 0f;
            pausePanel.SetActive(true);
        }
        isPaused = !isPaused;
    }

    private void SaveSliderValues()
    {
        PlayerPrefs.SetFloat("Slider1Value", slider1.value);
        PlayerPrefs.SetFloat("Slider2Value", slider2.value);
        PlayerPrefs.SetFloat("Slider3Value", slider3.value);
        PlayerPrefs.Save();
    }

    private void LoadSliderValues()
    {
        if (PlayerPrefs.HasKey("Slider1Value")) slider1.value = PlayerPrefs.GetFloat("Slider1Value");
        if (PlayerPrefs.HasKey("Slider2Value")) slider2.value = PlayerPrefs.GetFloat("Slider2Value");
        if (PlayerPrefs.HasKey("Slider3Value")) slider3.value = PlayerPrefs.GetFloat("Slider3Value");
    }

    void Start()
    {
        LoadSliderValues();
    }
}
