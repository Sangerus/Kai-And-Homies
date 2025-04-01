using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour, IDataPersistence
{
    private int nextLevel;

    public void ShowPortal()
    {
        gameObject.SetActive(true);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            nextLevel = SceneManager.GetActiveScene().buildIndex + 1;
            // Cập nhật `currentLevel` trong GameData
            DataPersistenceManager.instance.gameData.currentLevel = nextLevel;
            DataPersistenceManager.instance.SaveGame();

            // Chuyển Scene
            SceneManager.LoadSceneAsync(nextLevel);
        }
    }

    public void LoadData(GameData data)
    {
        this.nextLevel = data.currentLevel;
    }

    public void SaveData(GameData data)
    {
        data.currentLevel = this.nextLevel;
    }
}
