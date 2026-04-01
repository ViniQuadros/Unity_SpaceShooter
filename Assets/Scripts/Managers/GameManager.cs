using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public string GetCurrentScene()
    {
        return SceneManager.GetActiveScene().name;
    }

    public virtual void ShowResults()
    {
        Debug.Log("Game Over!");
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
