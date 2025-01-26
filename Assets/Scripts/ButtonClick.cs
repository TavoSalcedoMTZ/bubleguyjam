using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonClick : MonoBehaviour
{
    public SceneTransition sceneTransition;

    public void OnButtonClick()
    {
        sceneTransition.FadeOutAndLoadScene("Principal");
    }
}
