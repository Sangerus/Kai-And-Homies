using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CollectHealth : MonoBehaviour
{
    [SerializeField] private float healthValue;
    public Text FullHealthMessage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) 
        {
            Health playerHealth = collision.GetComponent<Health>();

            if (playerHealth.currentHealth < playerHealth.startingHealth)
            {
                playerHealth.AddHealth(healthValue);
                gameObject.SetActive(false);
            }
            else
            {
                ShowFullHealthMessage();
            }
        }
    }
    private void ShowFullHealthMessage()
    {
        FullHealthMessage.gameObject.SetActive(true);
        Invoke("HideFullHealthMessage", 2);
    }
    private void HideFullHealthMessage()
    {
       FullHealthMessage.gameObject.SetActive(false);
    }
}

