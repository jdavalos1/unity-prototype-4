using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Score value
    public int numberOfPoints = 0;

    public bool isGameActive = false;
    private bool isPaused = false;
    
    // UIs
    [SerializeField] GameObject introMenu;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject inGameUI;
    [SerializeField] GameObject gameOverMenu;

    // UI text
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI roundText;
    [SerializeField] TextMeshProUGUI gameoverScoreText;
    
    /// <summary>
    /// Start the game and remove the intro section
    /// </summary>
    public void StartGame()
    {
        isGameActive = true;
        introMenu.SetActive(false);
        inGameUI.SetActive(true);
    }

    /// <summary>
    /// End the game and delete all the current enemies
    /// </summary>
    public void EndGame()
    {
        isGameActive = false;
        
        foreach(var enemy in FindObjectsOfType<Enemy>())
        {
            Destroy(enemy.gameObject);
        }

        pauseMenu.SetActive(false);
        inGameUI.SetActive(false);
        gameOverMenu.SetActive(true);
        gameoverScoreText.text = scoreText.text;
    }

    public void HandlePause()
    {
        if(isPaused)
        {
            isGameActive = true;
            isPaused = false;
            Time.timeScale = 1.0f;
            pauseMenu.SetActive(false);
        }
        else
        {
            isGameActive = false;
            isPaused = true;
            Time.timeScale = 0.0f;
            pauseMenu.SetActive(true);
        }
    }

    public void UpdatePoints(int pointsToAdd)
    {
        numberOfPoints += pointsToAdd;
        scoreText.text = $"Score: {numberOfPoints}";
    }

    public void UpdateRound(int round)
    {
        roundText.text = $"Round {round}";
    }

    public void RestartGame()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
