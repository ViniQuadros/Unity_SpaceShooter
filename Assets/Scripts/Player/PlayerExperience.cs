using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerExperience : MonoBehaviour
{
    [Header("Exp UI")]
    public Slider expSlider;
    public TextMeshProUGUI playerLevel;
    public GameObject upgradeCanvas;

    private int currentExp = 0;
    private int expToNextLevel = 200;
    private int currentPlayerLevel = 1;

    void Start()
    {
        expSlider.value = currentExp;
        expSlider.maxValue = expToNextLevel;
        playerLevel.text = currentPlayerLevel.ToString();
    }

    public void AddExperience(int amount)
    {
        currentExp += amount;
        expSlider.value = currentExp;
        if (currentExp >= expToNextLevel)
        {
            LevelUp();
        }
    }

    public void LevelUp()
    {
        currentPlayerLevel++;
        playerLevel.text = currentPlayerLevel.ToString();

        currentExp = 0;
        expSlider.value = currentExp;

        UpgradeChoice();
    }

    private void UpgradeChoice()
    {
        Time.timeScale = 0f; // Pause the game
        upgradeCanvas.SetActive(true);
    }
}
