using UnityEngine;

[CreateAssetMenu(menuName = "Upgrades/Crit Chance Upgrade")]
public class CritChanceUpgrade : UpgradesSO
{
    public override void ApplyEffect(PlayerStats playerStats)
    {
        playerStats.bonusCritChance += modifierValue;
        ResumeGame();
    }

    public override string GetDescription()
    {
        return $"Increase crit chance by {modifierValue}%";
    }
}
