using UnityEngine;

public class BossDefeat : MonoBehaviour
{

    private Enemigo enemigo; 
    private passWorld passWorldScript; 
    void Start()
    {
        passWorldScript= FindAnyObjectByType<passWorld>();
        enemigo = GetComponent<Enemigo>();
    }
    void Update()
    {
        if (enemigo.Vida==0)
        {
            passWorldScript.PasandoAlSiguienteNivel = true; 
        }
    }
}
