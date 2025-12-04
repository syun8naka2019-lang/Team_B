using UnityEngine;
using UnityEngine.UI;

public class GameplayScoreDisplay : MonoBehaviour
{
    public Text scoreText;
    void Update()
    {
        if (ScoreBoard.Instance != null && scoreText != null)
        {
            scoreText.text = "Score: " + ScoreBoard.Instance.Score.ToString();
        }
    }
}