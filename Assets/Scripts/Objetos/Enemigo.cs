using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo : MonoBehaviour
{
    public EnemyManager enemymanager; // El EnemyManager asociado al enemigo
    public int Vida = 10;

    void Start()
    {
        // Intentar obtener la referencia al EnemyManager en la escena
        if (enemymanager == null)
        {
            enemymanager = FindObjectOfType<EnemyManager>(); // Busca el primer EnemyManager en la escena
        }
    }

    void Update()
    {
        if (Vida <= 0)
        {
            Destroy(gameObject); // Destruir el enemigo
            if (enemymanager != null)
            {
                enemymanager.EnemyDefeated(); // Llamar al método cuando el enemigo es derrotado
            }
        }
    }

    public void DicreseVida()
    {
        Vida -= 1; // Disminuir la vida del enemigo
    }
}
