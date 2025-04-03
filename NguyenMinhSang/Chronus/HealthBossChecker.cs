using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBossChecker : MonoBehaviour
{
    public int Health = 10;
    private int previousHealth;
    private bool isDamaged = false;
    private Animator animator;
    private BossHPUI hpUI;

    void Start()
    {
        animator = GetComponent<Animator>();
        previousHealth = Health;

        animator.SetInteger("Health", Health);
        hpUI = FindObjectOfType<BossHPUI>();
    }

    void Update()
    {
        if (Health < previousHealth)
        {
            isDamaged = true;
            animator.SetBool("isDamaged", true); // Set Animator parameter
            OnHealthChanged();
        }

        // Cập nhật giá trị health vào Animator
        animator.SetInteger("Health", Health);

        previousHealth = Health;
    }

    void OnHealthChanged()
    {
        if (isDamaged)
        {
            if (Health <= 0)
            {
                animator.SetTrigger("Death"); // Trigger death animation
            }
            else
            {
                animator.SetTrigger("Hit"); // Play hit animation
                Invoke("ResetDamageState", 0.5f);
            }
        }
    }

    public void ResetDamageState()
    {
        isDamaged = false;
        animator.SetBool("isDamaged", false);
    }

    public void ReduceHealth(int damage)
    {
        if (Health > 0)
        {
            Health -= damage;
            Health = Mathf.Clamp(Health, 0, 10); // Đảm bảo health không vượt quá giới hạn

            // Cập nhật giá trị health vào animator mỗi lần thay đổi
            animator.SetInteger("Health", Health);

            if (Health <= 0)
            {
                animator.SetTrigger("Death");
                Destroy(gameObject, 2f);
            }
            else
            {
                animator.SetTrigger("Hit");
            }
            if (hpUI != null)
            {
                hpUI.TakeDamage(damage); // Update HP in BossHPUI
            }

        }
    }
}
