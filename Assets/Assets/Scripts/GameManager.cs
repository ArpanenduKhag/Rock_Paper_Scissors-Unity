using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("UI References")]
    public TMP_Text playerScoreText;
    public TMP_Text botScoreText;
    public TMP_Text timerText; 
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

    [Header("Buttons")]
    public Button rockButton;
    public Button paperButton;
    public Button scissorsButton;

    private int playerScore = 0;
    private int botScore = 0;
    private int currentRound = 1;
    private const int totalRounds = 5;

    private bool canChoose = false;
    private string playerChoice = "";
    private string botChoice = "";

    private readonly string[] choices = { "Rock", "Paper", "Scissors" };

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        Time.timeScale = 1f;
        HideAllResults();
        ClearChoiceImages();
    }

    private void Start()
    {
        StartCoroutine(GameLoop());
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

    private IEnumerator GameLoop()
    {
        while (currentRound <= totalRounds)
        {
            Debug.Log($"--- Round {currentRound} ---");
            HideAllResults();
            ClearChoiceImages();

            playerChoice = "";
            botChoice = "";

            canChoose = true;
            EnableChoiceButtons(true);

            // Show timer for each new round
            timerText.gameObject.SetActive(true);
            float timer = 3f;

            while (timer > 0f)
            {
                timerText.text = $"Time: {timer:F0}";
                yield return new WaitForSeconds(1f);
                timer -= 1f;
            }

            // Hide timer after countdown for this round
            timerText.gameObject.SetActive(false);

            canChoose = false;
            EnableChoiceButtons(false);

            // Bot logic
            if (string.IsNullOrEmpty(playerChoice))
            {
                botChoice = choices[Random.Range(0, choices.Length)];
                botScore++;
                SetChoiceImage(botChoiceImage, botChoice);
            }
            else
            {
                botChoice = choices[Random.Range(0, choices.Length)];
                SetChoiceImage(botChoiceImage, botChoice);
                DetermineWinner();
            }

            UpdateScoreUI();
            currentRound++;

            yield return new WaitForSeconds(2f);
        }

        ShowFinalResult();
    }

    public void PlayerChoice(string choice)
    {
        if (!canChoose) return;

        playerChoice = choice;
        SetChoiceImage(playerChoiceImage, choice);

        canChoose = false;
        EnableChoiceButtons(false);
    }

    private void SetChoiceImage(Image img, string choice)
    {
        switch (choice)
        {
            case "Rock": img.sprite = rockSprite; break;
            case "Paper": img.sprite = paperSprite; break;
            case "Scissors": img.sprite = scissorsSprite; break;
        }

        img.enabled = true;
        img.color = Color.white;
    }

    private void DetermineWinner()
    {
        string result;

        if (playerChoice == botChoice)
        {
            result = "Tie";
        }
        else if (
            (playerChoice == "Rock" && botChoice == "Scissors") ||
            (playerChoice == "Paper" && botChoice == "Rock") ||
            (playerChoice == "Scissors" && botChoice == "Paper")
        )
        {
            playerScore++;
            result = "Win";
        }
        else
        {
            botScore++;
            result = "Lose";
        }

        ShowRoundResult(result);
    }

    private void ShowRoundResult(string result)
    {
        HideAllResults();

        switch (result)
        {
            case "Win": youWinText.SetActive(true); break;
            case "Lose": youLoseText.SetActive(true); break;
            case "Tie": tieText.SetActive(true); break;
        }
    }

    private void UpdateScoreUI()
    {
        playerScoreText.text = $"Player: {playerScore}";
        botScoreText.text = $"Bot: {botScore}";
    }

    private void ShowFinalResult()
    {
        HideAllResults();
        EnableChoiceButtons(false);
        ClearChoiceImages();

        timerText.gameObject.SetActive(true); // Show final result text
        timerText.fontSize = 36;

        if (playerScore > botScore)
        {
            youWinText.SetActive(true);
            timerText.text = "You Won the Game!";
        }
        else if (botScore > playerScore)
        {
            youLoseText.SetActive(true);
            timerText.text = "Bot Won the Game!";
        }
        else
        {
            tieText.SetActive(true);
            timerText.text = "It's a Draw!";
        }

        PauseMenu pauseMenu = FindObjectOfType<PauseMenu>();
        if (pauseMenu != null)
            pauseMenu.ShowGameOverMenu();
    }


    private void EnableChoiceButtons(bool enable)
    {
        rockButton.interactable = enable;
        paperButton.interactable = enable;
        scissorsButton.interactable = enable;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void PauseGame()
    {
        PauseMenu pauseMenu = FindObjectOfType<PauseMenu>();
        if (pauseMenu != null)
            pauseMenu.PauseGame();
    }
}
