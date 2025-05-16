using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class passWorld : MonoBehaviour
{
    public bool PasandoAlSiguienteNivel = false;
    private GameManagerG gameManagerG; // Referencia al GameManagerG

    private void Start()
    {
        gameManagerG=FindFirstObjectByType<GameManagerG>();
    }
    public void NuevoNivel()
    {
        gameManagerG.NextWorld();
        PasandoAlSiguienteNivel = false; // Reinicia la variable para el siguiente uso
    }

}
    
