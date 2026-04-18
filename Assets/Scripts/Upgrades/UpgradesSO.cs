using UnityEngine;

[CreateAssetMenu(fileName = "UpgradesSO", menuName = "Scriptable Objects/UpgradesSO")]
public abstract class UpgradesSO : ScriptableObject
{
    public float modifierValue;
    protected PlayerStats playerStats;

    public abstract void ApplyEffect(PlayerStats playerStats);
    public abstract string GetDescription();

    public void ResumeGame()
    {
        UpgradesManager upgradesManager = GameObject.FindGameObjectWithTag("UpgradesManager").GetComponent<UpgradesManager>();
        upgradesManager.ResumeGame();
    }
}
