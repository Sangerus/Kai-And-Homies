using UnityEngine;

public class BossDeath : MonoBehaviour
{
    public Animator animator; // Assign the Animator in the Inspector
    public string deathAnimationName = "Death"; // Name of the death animation
    public float destroyDelay = 1.5f; // Time before destroying after animation

    private bool isDying = false;

    private void Start()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
    }

    private void Update()
    {
        // Check if the boss is playing the "Dead" animation
        if (!isDying && animator.GetCurrentAnimatorStateInfo(0).IsName(deathAnimationName))
        {
            isDying = true;
            Invoke(nameof(DestroyBoss), destroyDelay); // Destroy after delay
        }
    }

    private void DestroyBoss()
    {
        Destroy(gameObject);
    }
}
