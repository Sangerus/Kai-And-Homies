using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blast : MonoBehaviour
{
    public float detectionRange = 5f; // Range to detect player
    public int damage = 10; // Damage value
    public Collider2D AttackPoint; // Assign the hitbox in Inspector
    private Transform player;
    private Animator animator;
    private bool isPlayerNearby = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();

        // Ensure laser hitbox is disabled at the start
        if (AttackPoint)
            AttackPoint.enabled = false;
    }

    void Update()
    {
        // Check if player is within detection range
        float distance = Vector3.Distance(transform.position, player.position);
        isPlayerNearby = distance < detectionRange;

        // Trigger laser animation when player is close
        animator.SetBool("isFiring", isPlayerNearby);
    }

    // Called from Animation Event when firing starts
    public void EnableAttackPoint()
    {
        if (AttackPoint)
            AttackPoint.enabled = true;
    }

    // Called from Animation Event when firing ends
    public void DisableAttackPoint()
    {
        if (AttackPoint)
            AttackPoint.enabled = false;
    }

}
