using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurbujasController : MonoBehaviour
{
    private Enemigo enemigos;
    public int Daño;
    void Start()
    {
        
    }


    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Verificamos si el objeto con el que colisionamos tiene la etiqueta "Bala"
        if (!collision.gameObject.CompareTag("Bala"))
        {
            // Intentamos obtener el componente Enemigo del objeto con el que colisionamos
            Enemigo enemigo = collision.gameObject.GetComponent<Enemigo>();

            // Si el objeto tiene un componente Enemigo, reducimos su vida
            if (enemigo != null)
            {
                enemigo.DicreseVida();
            }

            // Destruimos el objeto actual (el que tiene este script)
            Destroy(gameObject);
        }
    }


}
