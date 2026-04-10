using UnityEngine;

[CreateAssetMenu(menuName = "Upgrades/Crit Chance Upgrade")]
public class CritChanceUpgrade : UpgradesSO
{
    public GameObject laserBean;
    public override void ApplyEffect()
    {
        laserBean.GetComponent<LaserBean>().SetBonusCritChance(modifierValue);
        modifierValue += 1f;
        ResumeGame();
    }

    public override string GetDescription()
    {
        return $"Increase crit chance by {modifierValue}%";
    }
}
