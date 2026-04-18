using UnityEngine;

[CreateAssetMenu(menuName = "Upgrades/Helper Ship Upgrade")]
public class HelperShipUpgrade : UpgradesSO
{
    public GameObject helperShipPrefab;

    public override void ApplyEffect(PlayerStats playerStats)
    {
        Instantiate(helperShipPrefab);
        ResumeGame();
    }

    public override string GetDescription()
    {
        return "Summon a helper ship that orbits around you and shoots at enemies.";
    }
}
