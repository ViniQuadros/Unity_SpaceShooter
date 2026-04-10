using UnityEngine;

[CreateAssetMenu(menuName = "Upgrades/Crit Damage Upgrade")]
public class CritDamageUpgrade : UpgradesSO
{
    public GameObject laserBean;
    public override void ApplyEffect()
    {
        laserBean.GetComponent<LaserBean>().SetBonusCritDamage(modifierValue);
        modifierValue += 5f;
        ResumeGame();
    }

    public override string GetDescription()
    {
        return $"Increase crit damage by {modifierValue}";
    }
}
