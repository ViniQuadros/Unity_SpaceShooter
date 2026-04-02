using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public Slider healthSlider; // Reference to the UI Slider for health

    private int maxHealth = 100;
    private int currentHealth;

    private void Awake()
    {
        // Initialize health slider value to 100% at the start
        if (healthSlider != null)
        {
            healthSlider.value = maxHealth;
        }
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // Ensure health doesn't go below 0
        healthSlider.value = currentHealth;
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // Ensure health doesn't exceed max
        healthSlider.value = currentHealth;
    }

    public void IncreaseMaxHealth(int amount)
    {
        maxHealth += amount;
        healthSlider.maxValue = maxHealth;
    }
}
