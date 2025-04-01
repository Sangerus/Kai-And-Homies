using UnityEngine;

public class PocketWatch : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (GameManager.instance != null)
            {
                GameManager.instance.UpdatePocketWatch(GameManager.instance.GetCurrentPocketWatch() + 1);
            }
            else
            {
                Debug.LogError("GameManager instance is missing!");
            }

            Destroy(gameObject);
        }
    }
}
