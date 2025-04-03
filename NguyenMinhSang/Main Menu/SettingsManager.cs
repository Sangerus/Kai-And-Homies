using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    public GameObject settingsPanel;  // Assign the Settings Panel
    public GameObject mainMenuPanel;  // Assign the Main Menu Panel
    public GameObject mainText;
    public Button closeButton;        // Assign Close Button
    public Button settingsButton;     // Assign Settings Button

    void Start()
    {
        settingsPanel.SetActive(false); // Hide Settings Panel at start

        settingsButton.onClick.AddListener(OpenSettings);
        closeButton.onClick.AddListener(CloseSettings);
    }

    void OpenSettings()
    {
        mainMenuPanel.SetActive(false); // Hide Main Menu
        mainText.SetActive(false);
        settingsPanel.SetActive(true);  // Show Settings
    }

    void CloseSettings()
    {
        settingsPanel.SetActive(false); // Hide Settings
        mainMenuPanel.SetActive(true);  // Show Main Menu
        mainText.SetActive(true);
    }
}
