using UnityEngine;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy Settings")]
    public List<GameObject> enemies = new List<GameObject>();
    public BossHealth boss; // Required reference to boss for shield parenting
    public GameObject enemyPrefab;
    public Transform enemySpawnPoint;
    public int spawnAmount = 3;

    [Header("Shield Settings")]
    public GameObject shieldPrefab;
    public Transform shieldSpawnPoint;
    private GameObject activeShield;
    public bool shieldDependsOnEnemies = true;

    public bool isAllMonsterDead = true;

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Activate()
    {
        SpawnEnemies();
        if (!shieldDependsOnEnemies)
        {
            SpawnShield();
        }
    }

    public void SpawnEnemies()
    {
        if (enemyPrefab == null || enemySpawnPoint == null)
        {
            Debug.LogWarning("Enemy prefab or spawn point not assigned!");
            return;
        }

        ClearExistingEnemies();

        isAllMonsterDead = false;

        for (int i = 0; i < spawnAmount; i++)
        {
            GameObject enemy = Instantiate(enemyPrefab, enemySpawnPoint.position, enemySpawnPoint.rotation);
            enemies.Add(enemy);

            Guardian guardian = enemy.GetComponent<Guardian>();
            if (guardian != null)
            {
                guardian.spawner = this;
            }
            else
            {
                Debug.LogWarning("Spawned enemy doesn't have a Guardian component!");
            }
        }

        if (shieldDependsOnEnemies)
        {
            SpawnShield();
        }
    }

    private void SpawnShield()
    {
        if (shieldPrefab == null || shieldSpawnPoint == null)
        {
            Debug.LogWarning("Shield prefab or spawn point not assigned!");
            return;
        }

        if (activeShield == null)
        {
            activeShield = Instantiate(shieldPrefab, shieldSpawnPoint.position, shieldSpawnPoint.rotation);

            // Always parent the shield to the boss if boss exists
            if (boss != null)
            {
                activeShield.transform.SetParent(boss.transform);
                activeShield.transform.localPosition = Vector3.zero;
                activeShield.transform.localRotation = Quaternion.identity;
            }
            else
            {
                Debug.LogWarning("No boss assigned - shield won't be parented!");
            }
        }
    }

    public void RemoveEnemy(GameObject enemy)
    {
        if (enemies.Contains(enemy))
        {
            enemies.Remove(enemy);
            CheckEnemyCount();
        }
    }

    private void CheckEnemyCount()
    {
        if (shieldDependsOnEnemies && enemies.Count == 0)
        {
            DestroyShield();
        }

        if (enemies.Count == 0)
        {
            isAllMonsterDead = true;
            animator.SetBool("isAllMonsterDead", true);
        }
        else
        {
            isAllMonsterDead = false;
            animator.SetBool("isAllMonsterDead", false);
        }
    }

    private void DestroyShield()
    {
        if (activeShield != null)
        {
            Destroy(activeShield);
            activeShield = null;
        }
    }

    private void ClearExistingEnemies()
    {
        foreach (var enemy in enemies)
        {
            if (enemy != null) Destroy(enemy);
        }
        enemies.Clear();

        if (shieldDependsOnEnemies)
        {
            DestroyShield();
        }
    }
}
