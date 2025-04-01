using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public GameObject playerUI;
    private static bool paused = false;

    private void Start()
    {
        Time.timeScale = 1f;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (paused == true)
            {
                Resume();
            }
            else
            {
                Stop();
            }
        }
    }

    void Stop()
    {
        pauseMenuUI.SetActive(true);
        playerUI.SetActive(false);
        Time.timeScale = 0f;
        paused = true;
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        playerUI.SetActive(true);
        Time.timeScale = 1f;
        paused = false;
    }

    public void MainMenuButton()
    {
        DataPersistenceManager.instance.SaveGame();
        Time.timeScale = 1f;
        SceneManager.LoadSceneAsync("MainMenu");
    }

}
