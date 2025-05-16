using UnityEngine;

public class LevelManager : MonoBehaviour
{
    void ActivarMundo(GameObject targetObject)
    {
        targetObject.SetActive(true);
        MonoBehaviour[] scripts = targetObject.GetComponents<MonoBehaviour>();

        foreach (var script in scripts)
        {
            script.enabled = true;


            var method = script.GetType().GetMethod("Start");
            if (method != null)
            {
                method.Invoke(script, null);
            }
        }
   
    }

    void DesactivarMundo(GameObject targetObject)
    {
        MonoBehaviour[] scripts = targetObject.GetComponents<MonoBehaviour>();
        foreach (var script in scripts)
        {
            script.enabled = false;
        }
        targetObject.SetActive(false);
    }
}
