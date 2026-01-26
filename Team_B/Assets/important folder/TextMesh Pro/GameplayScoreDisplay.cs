//using UnityEngine;
//using TMPro;

//public class GameplayScoreDisplay : MonoBehaviour
//{
//    public TextMeshProUGUI scoreText;

//    void Start()
//    {
//        if (scoreText != null)
//        {
//            scoreText.text = "Score: " + ScoreBoard.Instance.Score;
//        }
//        else
//        {
//            Debug.LogError("Score: scoreText Ç™ê›íËÇ≥ÇÍÇƒÇ¢Ç‹ÇπÇÒÅI");
//        }
//    }
//}

using UnityEngine;
using UnityEngine.UI;

public class GameplayScoreDisplay : MonoBehaviour
{
    public Text scoreText;

    void Update()
    {
        if (ScoreBoard.Instance == null || scoreText == null)
            return;

        // Åö 8åÖå≈íËÅi99999999Ç≈é~Ç‹ÇÈÅj
        scoreText.text = ScoreBoard.Instance.Score.ToString("D8");
    }
}
