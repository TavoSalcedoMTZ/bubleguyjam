using UnityEngine;
using UnityEngine.SceneManagement;

public class EleccionPersonaje : MonoBehaviour
{
    // Nombre de la escena que quieres cargar
    public string nombreEscena;

    // Funci�n para cuando se elige a Carlos
    public void ElegirCarlos()
    {
        Debug.Log("Elegiste a Carlos");
        
    }

    // Funci�n para cuando se elige a Pepe
    public void ElegirPepe()
    {
        Debug.Log("Elegiste a Pepe");
       
    }

    // Funci�n para cuando se elige a Javier
    public void ElegirJavier()
    {
        Debug.Log("Elegiste a Javier");
       
    }

  
}
