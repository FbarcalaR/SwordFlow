using TMPro;
using UnityEngine;

public class Highscore : MonoBehaviour
{
    private const string HighScoreKey = "highScore";
    
    void Start()
    {
        var text = GetComponent<TextMeshProUGUI>();
        int highScore = PlayerPrefs.GetInt(HighScoreKey, 0);
        text.text = highScore.ToString();
    }
}
