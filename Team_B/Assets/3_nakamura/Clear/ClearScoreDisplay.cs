using UnityEngine;
using UnityEngine.UI;

public class ClearScoreDisplay : MonoBehaviour
{
    public Text scoreText;

    void Start()
    {
        int score = PlayerPrefs.GetInt("StageScore", 0);
        scoreText.text = "SCORE : " + score.ToString();
    }
}
