using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private Button newGameButton;
    [SerializeField] private Button continuteButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button quitButton;

    private void Start()
    {
        if (!DataPersistenceManager.instance.HasGameData())
        {
            continuteButton.interactable = false; // Nếu không có dữ liệu game, tắt nút Continute
        }
    }

    public void NewGame()
    {
        DisableMenuButton();
        DataPersistenceManager.instance.NewGame();
        SceneManager.LoadSceneAsync(1);
    }

    public void Continute()
    {
        DisableMenuButton();
        int lastLevel = DataPersistenceManager.instance.gameData.currentLevel;
        SceneManager.LoadSceneAsync(lastLevel);
    }

    public void Quit()
    {
        DisableMenuButton();
        Application.Quit();
    }

    private void DisableMenuButton()
    {
        newGameButton.interactable = false;
        continuteButton.interactable = false;
        settingsButton.interactable = false;
        quitButton.interactable = false;
    }
}
