using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Health playerHealth;
    [SerializeField] private Image totalhealthbar;
    [SerializeField] private Image currenthealthbar;

    private void Start()
    {
        if (playerHealth == null)
        {
            playerHealth = FindObjectOfType<Health>();
        }

        totalhealthbar.fillAmount = playerHealth.startingHealth / 10;
    } 

    private void Update()
    {
        currenthealthbar.fillAmount = playerHealth.currentHealth / 10;
    }
}
