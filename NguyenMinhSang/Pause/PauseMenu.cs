using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public GameObject playerUI;
    private static bool paused = false;
    private static bool stillAttackingTheBoss = false; // Static so it persists across function calls

    private BossHPUI display; // Cache the reference

    private void Start()
    {
        Time.timeScale = 1f;
        display = FindObjectOfType<BossHPUI>(); // Cache the BossHPUI reference
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
        if (display != null && display.gameObject.activeInHierarchy)
        {
            display.DisableBossHPUI();
            stillAttackingTheBoss = true;
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        playerUI.SetActive(true);
        Time.timeScale = 1f;
        paused = false;

        if (display != null && stillAttackingTheBoss)
        {
            display.EnableBossHPUI();
        }
    }

    public void MainMenuButton()
    {
        DataPersistenceManager.instance.SaveGame();
        Time.timeScale = 1f;
        SceneManager.LoadSceneAsync("MainMenu");
    }

}
