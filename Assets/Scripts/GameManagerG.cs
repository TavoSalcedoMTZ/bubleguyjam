using UnityEngine;

public class GameManagerG : MonoBehaviour
{
    public static GameManagerG Instance { get; private set; }

    [SerializeField]
    private int World;
    public bool BatallaJefe = false;

    public BackgroundManager[] backgroundManagers;

    private void Awake()
    {
      
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); 
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); 

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

     
        foreach (var manager in backgroundManagers)
        {
            manager.gameObject.SetActive(false);
        }

     
        backgroundManagers[index].gameObject.SetActive(true);

     
        backgroundManagers[index].Initialize(); 
    }
    public void NextWorld()
    {
        if (World >= 0 && World < backgroundManagers.Length)
        {
            Debug.Log("Desactivando mundo: " + World);
            backgroundManagers[World].gameObject.SetActive(false);
            backgroundManagers[World].Limpiar();
        }

        World++;

        if (World >= backgroundManagers.Length)
        {
            World = 0;
        }

        Debug.Log("Activando mundo: " + World);
        backgroundManagers[World].gameObject.SetActive(true);
        backgroundManagers[World].Initialize();
    }
}
