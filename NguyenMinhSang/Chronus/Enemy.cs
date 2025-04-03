using UnityEngine;

public class Enemy : MonoBehaviour
{
    private EnemySpawner spawner;

    public void SetSpawner(EnemySpawner spawner)
    {
        this.spawner = spawner;
    }

    public void Die()
    {
        if (spawner != null)
        {
            spawner.RemoveEnemy(gameObject);
        }

        Destroy(gameObject); // Destroy the enemy object
    }
}
