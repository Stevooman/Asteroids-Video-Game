using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


/// <summary>
/// This class starts and ends the game, tracks and displays the player's score, and controls all of the text
/// and buttons on the screen.
/// </summary>
public class GameManager : MonoBehaviour
{
    // Variables initialized in the Unity Editor
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI finalScoreText;
    public TextMeshProUGUI gameOverText;
    public GameObject player;
    public Button startButton;
    public Button restartButton;

    /// <summary>
    /// The player's current score.
    /// </summary>
    public int Score { get; set; }
    public GameObject TitleScreen { get; set; }
    public GameObject GameOverScreen { get; set; }

    /// <summary>
    /// Used to check if the game is over.
    /// </summary>
    public bool IsGameActive { get; set; }

    // Names of GameObject's in Unity Editor
    private const string TITLE_SCREEN = "Title Screen";
    private const string GAMEOVER_SCREEN = "Game Over Screen";

    /// <summary>
    /// Initializes class members, displays the start screen and listens for the click of the start button.
    /// </summary>
    private void Awake()
    {
        scoreText.enabled = false;
        TitleScreen = GameObject.Find(TITLE_SCREEN);

        GameOverScreen = GameObject.Find(GAMEOVER_SCREEN);
        GameOverScreen.SetActive(false);

        IsGameActive = false;
        startButton.onClick.AddListener(StartGame);
    }

    /// <summary>
    /// Starts the game and resets the player's score.
    /// </summary>
    private void StartGame()
    {
        player.SetActive(true);
        scoreText.enabled = true;
        TitleScreen.SetActive(false);
        IsGameActive = true;
        Score = 0;
    }

    /// <summary>
    /// Displays game over screen and listens for the click of the restart button.
    /// </summary>
    public void GameOver()
    {
        IsGameActive = false;
        GameOverScreen.SetActive(true);
        scoreText.enabled = false;
        finalScoreText.text = "Final Score: " + Score;
        restartButton.onClick.AddListener(RestartGame);
    }

    /// <summary>
    /// Loads the game back to the starting default screen.
    /// </summary>
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    /// <summary>
    /// Updates the player's score.
    /// </summary>
    /// <param name="pointsToAdd">The amount of points to add to the current score.</param>
    public void UpdateScore(int pointsToAdd)
    {
        Score += pointsToAdd;
        scoreText.text = "Score: " + Score;
    }
}
