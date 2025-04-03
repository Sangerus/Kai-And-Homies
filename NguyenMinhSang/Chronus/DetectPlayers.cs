using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float detectionRange = 5f;  // Detection distance
    private bool isPlayerNearby = false;
    private Transform player;
    private Animator animator;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Check if player is within detection range
        float distance = Vector3.Distance(transform.position, player.position);
        isPlayerNearby = distance < detectionRange;

        // Play animation when player is nearby
        animator.SetBool("isPlayerNearby", isPlayerNearby);
    }
}
