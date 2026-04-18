using UnityEngine;

[CreateAssetMenu(menuName = "Upgrades/Shield Upgrade")]
public class ShieldUpgrade : UpgradesSO
{
    public GameObject shield;

    public override void ApplyEffect(PlayerStats playerStats)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Instantiate(shield, player.transform);
        ResumeGame();
    }

    public override string GetDescription()
    {
        return "Give the player a shield that can absorb one hit. The shield will reactivate after 20 seconds.";
    }
}
