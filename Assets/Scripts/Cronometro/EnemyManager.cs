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
        totalEnemiesAlive--;

        if (totalEnemiesAlive < 0)
        {
            totalEnemiesAlive = 0;
        }

        if (totalEnemiesAlive == 0)
        {
            stageFree = true;
        }
    }

    public bool AreEnemiesPresent()
    {
    
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
