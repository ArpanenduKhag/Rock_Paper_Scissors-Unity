using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("UI References")]
    public TMP_Text playerScoreText;
    public TMP_Text botScoreText;

    [Header("Result Texts")]
    public GameObject youWinText;
    public GameObject youLoseText;
    public GameObject tieText;

    [Header("Choice Images")]
    public Image playerChoiceImage;
    public Image botChoiceImage;

    [Header("Choice Sprites")]
    public Sprite rockSprite;
    public Sprite paperSprite;
    public Sprite scissorsSprite;

    private int playerScore = 0;
    private int botScore = 0;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        // Always reset time scale when scene starts (after pause)
        Time.timeScale = 1f;

        HideAllResults();
        ClearChoiceImages();
    }

    private void HideAllResults()
    {
        youWinText.SetActive(false);
        youLoseText.SetActive(false);
        tieText.SetActive(false);
    }

    private void ClearChoiceImages()
    {
        playerChoiceImage.sprite = null;
        botChoiceImage.sprite = null;
        playerChoiceImage.enabled = false;
        botChoiceImage.enabled = false;
    }

    public void PlayerChoice(string playerChoice)
    {
        string[] choices = { "Rock", "Paper", "Scissors" };
        string botChoice = choices[Random.Range(0, choices.Length)];

        // Set images for both
        SetChoiceImage(playerChoiceImage, playerChoice);
        SetChoiceImage(botChoiceImage, botChoice);

        string result = DetermineWinner(playerChoice, botChoice);
        ShowResult(result);
    }

    private void SetChoiceImage(Image img, string choice)
    {
        switch (choice)
        {
            case "Rock":
                img.sprite = rockSprite;
                break;
            case "Paper":
                img.sprite = paperSprite;
                break;
            case "Scissors":
                img.sprite = scissorsSprite;
                break;
        }

        img.enabled = true;
        img.color = Color.white; // ensure it's visible

        Debug.Log($"Set {choice} sprite on {img.name}");
    }

    private string DetermineWinner(string player, string bot)
    {
        if (player == bot)
            return "Tie";

        if ((player == "Rock" && bot == "Scissors") ||
            (player == "Paper" && bot == "Rock") ||
            (player == "Scissors" && bot == "Paper"))
        {
            playerScore++;
            return "Win";
        }

        botScore++;
        return "Lose";
    }

    private void ShowResult(string result)
    {
        HideAllResults();

        switch (result)
        {
            case "Win":
                youWinText.SetActive(true);
                break;
            case "Lose":
                youLoseText.SetActive(true);
                break;
            case "Tie":
                tieText.SetActive(true);
                break;
        }

        playerScoreText.text = "Player: " + playerScore;
        botScoreText.text = "Bot: " + botScore;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // --- NEW ---
    // Optional: Call this from a Pause Button if you add one
    public void PauseGame()
    {
        PauseMenu pauseMenu = FindObjectOfType<PauseMenu>();
        if (pauseMenu != null)
        {
            pauseMenu.PauseGame();
        }
    }
}
