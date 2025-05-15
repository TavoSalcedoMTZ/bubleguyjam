using UnityEngine;

public class GameManagerG : MonoBehaviour
{
    public static GameManagerG Instance { get; private set; }

    [SerializeField]
    private int World;

    public BackgroundManager[] backgroundManagers;

    private void Awake()
    {
        // Implementación del Singleton
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Elimina duplicados
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); // Opcional: si quieres que sobreviva entre escenas

        // Desactiva todos menos el primero
        for (int i = 0; i < backgroundManagers.Length; i++)
        {
            if (i == World)
            {
                backgroundManagers[i].gameObject.SetActive(true);
            }
            else
            {
                backgroundManagers[i].gameObject.SetActive(false);
            }
        }
    }

    public void ActivateBackgroundManager(int index)
    {
        if (index < 0 || index >= backgroundManagers.Length) return;

        // Desactiva todos
        foreach (var manager in backgroundManagers)
        {
            manager.gameObject.SetActive(false);
        }

        // Activa el nuevo
        backgroundManagers[index].gameObject.SetActive(true);

        // Reinicia el manager activado
        backgroundManagers[index].Initialize(); // Asegúrate de tener esta función en tu clase
    }
}
