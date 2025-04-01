using UnityEngine;

public class ArrowTrap : MonoBehaviour
{
    public float attackCooldown;
    public Transform firePoint;
    public GameObject[] projectile;
    private float cooldownTimer;

    private void Attack()
    {
        cooldownTimer = 0;

        projectile[FindProjectile()].transform.position = firePoint.position;
        projectile[FindProjectile()].GetComponent<EnemyProjectile>().ActiveProjectile();
    }

    private int FindProjectile()
    {
        for (int i = 0; i < projectile.Length; i++)
        {
            if (!projectile[i].activeInHierarchy)
                return i;
        }
        return 0;
    }

    private void Update()
    {
        cooldownTimer += Time.deltaTime;

        if (cooldownTimer >= attackCooldown)
        {
            Attack();
        }
    }
}
