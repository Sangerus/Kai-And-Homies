using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndGame : MonoBehaviour
{
    public Button loadButton; // Reference to your Button

    private void Start()
    {
        // Make sure the button is assigned
        if (loadButton != null)
        {
            loadButton.onClick.AddListener(LoadLevel); // Attach the function to the button click
        }
    }

    // This function will be called when the button is clicked
    void LoadLevel()
    {
        SceneManager.LoadScene("MainMenu"); // Replace with the name of your scene
    }
}

