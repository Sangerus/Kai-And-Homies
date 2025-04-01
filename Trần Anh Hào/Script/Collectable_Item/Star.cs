using UnityEngine;

public class Star : MonoBehaviour
{
    public string id;

    [ContextMenu("Generate guid for id")]
    private void GenerateGuid()
    {
        id = System.Guid.NewGuid().ToString();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (GameManager.instance != null)
            {
                GameManager.instance.CollectStar(id);
            }
            else
            {
                Debug.LogError("GameManager instance is missing!");
            }

            gameObject.SetActive(false);
        }
    }
}

