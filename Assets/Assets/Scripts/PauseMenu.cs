using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [Header("UI References")]
    public GameObject pauseMenuUI;  

    private bool isPaused = false;

    private void Start()
    {
        if (pauseMenuUI == null)
            pauseMenuUI = GameObject.Find("PauseMenu");

        if (pauseMenuUI != null)
            pauseMenuUI.SetActive(false);

        // Ensure normal speed when starting the scene
        Time.timeScale = 1f;
    }

    private void Update()
    {
        // Detect ESC (PC) and Back Button (Android)
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    public void PauseGame()
    {
        if (pauseMenuUI == null || isPaused)
            return;

        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
        Debug.Log("Game Paused");
    }

    public void ResumeGame()
    {
        if (pauseMenuUI == null || !isPaused)
            return;

        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        Debug.Log("Game Resumed");
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
