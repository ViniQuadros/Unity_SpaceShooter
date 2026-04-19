using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

[CreateAssetMenu(menuName = "Upgrades/Exp Multiplier Upgrade")]
public class ExpMultiplierUpgrade : UpgradesSO
{
    public override void ApplyEffect(PlayerStats playerStats)
    {
        playerStats.bonusExpMultiplier += modifierValue;
        ResumeGame();
    }

    public override string GetDescription()
    {
        return "Increase Experience Multiplier by " + modifierValue + "%";
    }
}
