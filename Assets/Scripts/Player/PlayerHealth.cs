using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using FirstGearGames.SmoothCameraShaker;

public class PlayerHealth : MonoBehaviour
{
    public Slider healthSlider;
    public PlayerStats playerStats;

    private float currentHealth;
    private float bonusMaxHealth = 0;

    private SpriteRenderer spriteRenderer;

    public ShakeData damageShake;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Initialize health slider value to 100% at the start
        if (healthSlider != null)
        {
            healthSlider.value = playerStats.baseMaxHealth;
        }

        currentHealth = playerStats.baseMaxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, playerStats.baseMaxHealth + bonusMaxHealth); // Ensure health doesn't go below 0
        healthSlider.value = currentHealth;

        if (currentHealth <= 0)
        {
            PlayerControl playerControl = GetComponent<PlayerControl>();
            playerControl.Die();
            return;
        }

        StartCoroutine(HitImpact());
        CameraShakerHandler.Shake(damageShake);
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, playerStats.baseMaxHealth + bonusMaxHealth); // Ensure health doesn't exceed max
        healthSlider.value = currentHealth;
    }

    public void IncreaseMaxHealth(float modifier)
    {
        float totalMaxHealth = playerStats.baseMaxHealth + bonusMaxHealth;
        bonusMaxHealth += (totalMaxHealth * modifier);
        healthSlider.maxValue = playerStats.baseMaxHealth + bonusMaxHealth;
        healthSlider.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, totalMaxHealth); // Adjust the width of the health bar based on the new max health
    }

    private IEnumerator HitImpact()
    {
        Color originalColor = spriteRenderer.color;
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.color = originalColor;
    }
}
