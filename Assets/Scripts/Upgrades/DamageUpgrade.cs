using UnityEngine;

[CreateAssetMenu(menuName = "Upgrades/Damage Upgrade")]
public class DamageUpgrade : UpgradesSO
{
    public override void ApplyEffect(PlayerStats playerStats)
    {
        playerStats.bonusDamage += modifierValue;
        ResumeGame();
    }

    public override string GetDescription()
    {
        return $"Increase laser damage by {modifierValue}";
    }
}