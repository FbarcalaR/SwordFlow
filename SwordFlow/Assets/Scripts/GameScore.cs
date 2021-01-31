using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameScore : MonoBehaviour
{
    public Text scoreText;
    public Text highScoreText;

    public int score;
    private int highScore;
    private const string HighScoreKey = "highScore";
    private static GameScore _instance;

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
            _instance.score = 0;
            highScore = PlayerPrefs.GetInt(HighScoreKey, 0);
        }
        else
        {
            Destroy(this);
        }
    }

    private void Update()
    {
        _instance.highScoreText.text = "High Score: " + _instance.highScore;
        _instance.scoreText.text = "Score: " + _instance.score;
    }

    public void AddScore(int scoreToAdd)
    {
        _instance.score += scoreToAdd;

        _instance.scoreText.text = "Score: " + _instance.score;
        if(_instance.score > _instance.highScore)
        {
            _instance.highScore = _instance.score;
            PlayerPrefs.SetInt(HighScoreKey, _instance.score);
            _instance.highScoreText.text = "High Score: " + _instance.highScore;
        }
    }
}
