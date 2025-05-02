using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class Enemigo : MonoBehaviour
{
    public JabonManage jabonManage;
    public EnemyManager enemymanager; // El EnemyManager asociado al enemigo
    public int Vida = 10;

    void Start()
    {
        if (jabonManage == null)
        {
            jabonManage = FindFirstObjectByType<JabonManage>();
        }
        if (enemymanager == null)
        {
            enemymanager = FindFirstObjectByType<EnemyManager>(); // Busca el primer EnemyManager en la escena
        }

    }

    public void HacerDano(int _dano)
    {
        Debug.Log("Enemigo hizo da�o al jugador");
        if (jabonManage != null)
        {
            jabonManage.JabonDicrese(_dano);
        }
        else
        {
            Debug.LogWarning("jabonManage no est� asignado");
        }
    }
    void Update()
    {
        if (Vida <= 0)
        {
            Destroy(gameObject); // Destruir el enemigo
            if (enemymanager != null)
            {
                enemymanager.EnemyDefeated(); // Llamar al m�todo cuando el enemigo es derrotado
            }
        }
        if (Input.GetKeyDown(KeyCode.P)){
            DicreseVida();
        }

    }

    public void DicreseVida()
    {
        Vida -= 1; // Disminuir la vida del enemigo
    }
}
