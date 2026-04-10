using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "UpgradesSO", menuName = "Scriptable Objects/UpgradesSO")]
public abstract class UpgradesSO : ScriptableObject
{
    public float modifierValue;

    public abstract void ApplyEffect();
    public abstract string GetDescription();

    public void ResumeGame()
    {
        UpgradesManager upgradesManager = GameObject.FindGameObjectWithTag("UpgradesManager").GetComponent<UpgradesManager>();
        upgradesManager.ResumeGame();
    }
}
