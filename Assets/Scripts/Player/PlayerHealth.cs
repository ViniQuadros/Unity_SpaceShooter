using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public Slider healthSlider;
    public PlayerStats playerStats;

    private float currentHealth;
    private float bonusMaxHealth = 0;

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
        // Ensure health doesn't exceed max
        currentHealth = Mathf.Clamp(currentHealth, 0, playerStats.baseMaxHealth + bonusMaxHealth); 
        healthSlider.value = currentHealth;
    }

    public void IncreaseMaxHealth(float modifier)
    {
        float totalMaxHealth = playerStats.baseMaxHealth + bonusMaxHealth;
        bonusMaxHealth += (totalMaxHealth * modifier);
        healthSlider.maxValue = playerStats.baseMaxHealth + bonusMaxHealth;
        healthSlider.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, totalMaxHealth); // Adjust the width of the health bar based on the new max health
    }
}
