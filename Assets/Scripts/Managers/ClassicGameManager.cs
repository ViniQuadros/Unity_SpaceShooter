using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClassicGameManager : GameManager
{
    public GameObject deathPanel;
    public TextMeshProUGUI finalScore;

    public override void ShowResults()
    {
        deathPanel.SetActive(true);
        finalScore.text = "Your Score: " + ScoreManager.scoreManagerInstance.GetScore().ToString();
        Time.timeScale = 0f;
    }
}
