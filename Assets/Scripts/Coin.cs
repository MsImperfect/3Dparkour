using UnityEngine;
using TMPro;

public class Coin : MonoBehaviour
{
    public int coinValue = 1;

    private static int score = 0;
    private static int highScore = 0;

    public TMP_Text scoreText;
    public TMP_Text highScoreText;

    private void Start()
    {
        LoadHighScore();
        UpdateScore();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            score += coinValue;

            if (score > highScore)
            {
                highScore = score;
                SaveHighScore();
            }

            UpdateScore();
            Destroy(gameObject); // Remove the coin
        }
    }


    void UpdateScore()
    {
        if (scoreText != null) scoreText.text = "Score: " + score.ToString();
        if (highScoreText != null) highScoreText.text = "Highscore: " + highScore.ToString();
    }

    void SaveHighScore()
    {
        PlayerPrefs.SetInt("HighScore", highScore);
        PlayerPrefs.Save();
    }

    void LoadHighScore()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);
    }

    public void ResetScore()
    {
        score = 0;
        UpdateScore();
    }
}
