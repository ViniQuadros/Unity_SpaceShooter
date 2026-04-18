using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UpgradesManager : MonoBehaviour
{
    [SerializeField] private List<UpgradesSO> possibleUpgrades;

    [Header("UI Parts")]
    public GameObject upgradeCanvas;
    public Button[] upgradeButtons;
    public TextMeshProUGUI[] upgradeTexts;

    private PlayerStats playerStats;

    void Start()
    {
        playerStats = GameObject.FindWithTag("Player").GetComponent<PlayerStats>();
        upgradeCanvas.SetActive(false);
    }

    void Update()
    {
        // For testing purposes, show upgrades when player presses U
        if (Input.GetKeyDown(KeyCode.U))
        {
            ShowPossibleUpgrades();
        }
    }

    public void ShowPossibleUpgrades()
    {
        List<UpgradesSO> selected = GetRandomUpgrades(upgradeButtons.Length);

        for (int i = 0; i < selected.Count; i++)
        {
            int local = i; // Get a local copy of the index for the lambda
            upgradeTexts[i].text = selected[i].GetDescription();

            upgradeButtons[i].onClick.RemoveAllListeners(); // Avoid stacking listeners
            upgradeButtons[i].onClick.AddListener(() => selected[local].ApplyEffect(playerStats));
        }

        upgradeCanvas.SetActive(true);
        Time.timeScale = 0f; // Pause the game
    }

    // Avoid getting the same upgrade multiple times in one selection
    private List<UpgradesSO> GetRandomUpgrades(int count)
    {
        List<UpgradesSO> shuffled = new List<UpgradesSO>(possibleUpgrades);

        for (int i = shuffled.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            (shuffled[i], shuffled[j]) = (shuffled[j], shuffled[i]);
        }

        return shuffled.GetRange(0, Mathf.Min(count, shuffled.Count));
    }

    public void ResumeGame()
    {
        upgradeCanvas.SetActive(false);
        Time.timeScale = 1f; // Resume the game
    }
}
