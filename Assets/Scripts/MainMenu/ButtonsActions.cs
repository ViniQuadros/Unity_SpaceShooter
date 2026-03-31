using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonsActions : MonoBehaviour
{
    public GameObject mainMenuButtons;
    public GameObject modesButtons;

    //Main Buttons functions
    public void ShowModesButtons() 
    {
        modesButtons.SetActive(true);
        mainMenuButtons.SetActive(false);
    }

    public void ShowMainMenuButtons() 
    {
        modesButtons.SetActive(false);
        mainMenuButtons.SetActive(true);
    }

    public void QuitGame() 
    {
        Application.Quit();
    }

    //Modes Buttons functions
    public void ClassicMode()
    {
        SceneManager.LoadScene("Classic Mode");
    }

    public void RoguelikeMode()
    {
        SceneManager.LoadScene("Roguelike Mode");
    }
}
