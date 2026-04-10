using UnityEngine;

[CreateAssetMenu(menuName = "Upgrades/Damage Upgrade")]
public class DamageUpgrade : UpgradesSO
{
    public GameObject laserBean;

    public override void ApplyEffect()
    {
        laserBean.GetComponent<LaserBean>().SetBonusDamage(modifierValue);
        modifierValue += 2f;
        ResumeGame();
    }

    public override string GetDescription()
    {
        return $"Increase laser damage by {modifierValue}";
    }
}