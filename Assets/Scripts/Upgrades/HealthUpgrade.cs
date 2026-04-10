using UnityEngine;

[CreateAssetMenu(menuName = "Upgrades/Health Upgrade")]
public class HealthUpgrade : UpgradesSO
{
    public override void ApplyEffect()
    {
        PlayerHealth playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        playerHealth.IncreaseMaxHealth(modifierValue);
        modifierValue += 0.1f;
        ResumeGame();
    }

    public override string GetDescription()
    {
        return $"Increase max health by {modifierValue * 100}%";
    }
}
