using UnityEngine;

[CreateAssetMenu(menuName = "Upgrades/Crit Damage Upgrade")]
public class CritDamageUpgrade : UpgradesSO
{
    public override void ApplyEffect(PlayerStats playerStats)
    {
        playerStats.bonusCritDamage += modifierValue;
        ResumeGame();
    }

    public override string GetDescription()
    {
        return $"Increase crit damage by {modifierValue}";
    }
}
