using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public int totalEnemiesAlive; 

    void Start()
    {
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
}
