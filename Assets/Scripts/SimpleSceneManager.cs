using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SimpleSceneManager : MonoBehaviour
{
    public GameObject pauseMenuUI;

    private bool isPaused = false;

    // Load scene by build index
    public void LoadSceneByIndex(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    // Pause or unpause the game
    public void TogglePause()
    {
        isPaused = !isPaused;
        if (isPaused)
        {
            Time.timeScale = 0f; // Pause the game
            if (pauseMenuUI != null)
            {
                pauseMenuUI.SetActive(true); // Show pause menu UI
            }
        }
        else
        {
            Time.timeScale = 1f; // Resume the game
            if (pauseMenuUI != null)
            {
                pauseMenuUI.SetActive(false); // Hide pause menu UI
            }
        }
    }

    // Quit the application
    public void QuitGame()
    {
        Debug.Log("Quitting the application...");
        Application.Quit();
    }
}
