using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject deathPanel;
    public TextMeshProUGUI finalScore;

    public void ShowResults()
    {
        deathPanel.SetActive(true);
        finalScore.text = "Your Score: " + ScoreManager.scoreManagerInstance.GetScore().ToString();
        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainScene");
    }
}
