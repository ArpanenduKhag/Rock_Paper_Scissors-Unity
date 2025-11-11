using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private Button resumeButton; 

    private bool isPaused = false;
    private bool isGameOver = false; 

    private void Start()
    {
        if (pauseMenuUI != null)
            pauseMenuUI.SetActive(false);
        else
            Debug.LogError("PauseMenuUI not assigned in Inspector!");

        if (resumeButton == null)
            Debug.LogWarning("Resume Button not assigned in PauseMenu script!");

        Time.timeScale = 1f;
    }

    private void Update()
    {
        // ESC / Android Back key
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                ResumeGame();
            else if (!isGameOver) // only allow pause if not game over
                PauseGame();
        }
    }

    public void PauseGame()
    {
        if (pauseMenuUI == null) return;

        pauseMenuUI.SetActive(true);
        if (resumeButton != null)
            resumeButton.interactable = true; 

        Time.timeScale = 0f;
        isPaused = true;
        Debug.Log("Game Paused");
    }

    public void ResumeGame()
    {
        if (pauseMenuUI == null || isGameOver) return; // 🔹 can't resume after game over

        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        Debug.Log("Game Resumed");
    }

    // Called by GameManager when final result is shown
    public void ShowGameOverMenu()
    {
        if (pauseMenuUI == null) return;

        pauseMenuUI.SetActive(true);
        if (resumeButton != null)
            resumeButton.interactable = false; 

        isGameOver = true;
        Time.timeScale = 0f; // pause everything at end
        Debug.Log("Game Over Menu Displayed");
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }
}
