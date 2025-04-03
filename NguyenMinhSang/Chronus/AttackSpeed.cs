using UnityEngine;

public class AnimationSpeedParameter : MonoBehaviour
{
    public Animator animator; // Assign the Animator in the Inspector
    public string speedParameter = "Speed"; // Make sure this matches the parameter in Animator

    private void Start()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>(); // Auto-assign if missing
        }
    }

    // Call this function from Animation Event
    public void IncreaseAnimationSpeed(float speedIncrease)
    {
        if (animator != null)
        {
            float currentSpeed = animator.GetFloat(speedParameter);
            animator.SetFloat(speedParameter, currentSpeed + speedIncrease); // Add to current speed
        }
    }
}
