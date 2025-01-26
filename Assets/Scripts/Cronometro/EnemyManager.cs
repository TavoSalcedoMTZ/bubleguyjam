    using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public int totalEnemiesAlive;
    public bool stageFree;
    void Start()
    {
        stageFree = false;
    }


    public void EnemyDefeated()
    {
        // Llamado cuando un enemigo es derrotado
        totalEnemiesAlive--;
    }

    public bool AreEnemiesPresent()
    {
        // Retorna verdadero si aún quedan enemigos vivos
        return totalEnemiesAlive > 0;
    }

    private void Update()
    {
        if (totalEnemiesAlive == 0)
        {
            stageFree=true;
        }
        else if(totalEnemiesAlive != 0)
        {
            stageFree=false;
        }
    }
}
