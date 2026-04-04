using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public Slider healthSlider;
    public PlayerStats playerStats;

    private int currentHealth;
    private int bonusMaxHealth = 0;

    private void Awake()
    {
        // Initialize health slider value to 100% at the start
        if (healthSlider != null)
        {
            healthSlider.value = playerStats.baseMaxHealth;
        }
        currentHealth = playerStats.baseMaxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, playerStats.baseMaxHealth + bonusMaxHealth); // Ensure health doesn't go below 0
        healthSlider.value = currentHealth;
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, playerStats.baseMaxHealth + bonusMaxHealth); // Ensure health doesn't exceed max
        healthSlider.value = currentHealth;
    }

    public void IncreaseMaxHealth(int amount)
    {
        bonusMaxHealth += amount;
        healthSlider.maxValue = playerStats.baseMaxHealth + bonusMaxHealth;
    }
}
