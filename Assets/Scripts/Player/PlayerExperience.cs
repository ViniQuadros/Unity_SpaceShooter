using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerExperience : MonoBehaviour
{
    public UpgradesManager upgradesManager;

    [Header("Exp UI")]
    public Slider expSlider;
    public TextMeshProUGUI playerLevel;

    private float currentExp = 0;
    private float expToNextLevel = 200;
    private float expMultiplier = 1f;
    private int currentPlayerLevel = 1;

    void Start()
    {
        expSlider.value = currentExp;
        expSlider.maxValue = expToNextLevel;
        playerLevel.text = currentPlayerLevel.ToString();
    }

    public void AddExperience(float amount)
    {
        currentExp += (amount * expMultiplier);
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

        //upgradesManager.ShowPossibleUpgrades();
    }

    public void IncreaseExpMultiplier(float multiplier)
    {
        expMultiplier += multiplier;
    }
}
