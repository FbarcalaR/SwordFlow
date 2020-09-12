using UnityEngine;
using UnityEngine.UI;

public class GameScore : MonoBehaviour
{
    public int score;
    public Text scoreText;
    public Text highScoreText;

    private int highScore;
    private const string HighScoreKey = "highScore";

    // Start is called before the first frame update
    void Start()
    {
        highScore = PlayerPrefs.GetInt(HighScoreKey, 0);
        highScoreText.text = "High Score: " + highScore;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddScore(int scoreToAdd)
    {
        score += scoreToAdd;

        scoreText.text = "Score: " + score;
        if(score > highScore)
        {
            PlayerPrefs.SetInt(HighScoreKey, score);
            highScoreText.text = "High Score: " + score;
        }
    }
}
