using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public SceneTransition sceneTransition;  // Referencia al script SceneTransition

    // Este m�todo ser� llamado desde el bot�n para iniciar el fade
    public void OnSceneTransitionButtonClick(string sceneName)
    {
        sceneTransition.FadeOutAndLoadScene(sceneName);  // Llamamos al fade out y cargamos la nueva escena
    }
}
