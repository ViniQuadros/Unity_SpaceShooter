using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    private int score = 0;

    private bool isDoubled = false;

    public static ScoreManager scoreManagerInstance { get; private set; }

    void Awake()
    {
        if (scoreManagerInstance != null && scoreManagerInstance != this)
        {
            Destroy(gameObject);
            return;
        }

        scoreManagerInstance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void AddScore(int points)
    {
        if (isDoubled)
        {
            points *= 2;
        }

        score += points;

        scoreText.text = "Score: " + GetScore().ToString();
    }

    public int GetScore()
    {
        return score;
    }

    public void SetIsDoubled(bool doubled)
    {
        isDoubled = doubled;
    }
}
