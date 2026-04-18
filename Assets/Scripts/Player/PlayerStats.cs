using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header("Speed Stats")]
    public float baseMovementSpeed = 5f;

    [Header("Health Stats")]
    public float baseMaxHealth = 100f;

    [Header("Combat Stats")]
    public float baseFireRate = 0.5f;
    public float baseProjectileSpeed = 20f;
    public float baseDamage = 5f;
    public float baseCritDamage = 20f;
    public float baseCritChance = 0.1f;

    [Header("Bonus Stats")]
    public float bonusMaxHealth = 0f;
    public float bonusMovementSpeed = 0f;
    public float bonusFireRate = 0f;
    public float bonusProjectileSpeed = 0f;
    public float bonusDamage = 0f;
    public float bonusCritDamage = 0f;
    public float bonusCritChance = 0f;
    public float bonusExpMultiplier = 1f;

    public enum PlayerMode
    {
        Classic,
        Roguelike
    }

    public enum PlayerState
    {
        Idle,
        Moving,
        Shooting,
        Dead
    }

    public PlayerMode currentMode = PlayerMode.Classic;
    public PlayerState state = PlayerState.Idle;

    public float TotalMaxHealth => baseMaxHealth + bonusMaxHealth;
    public float TotalMovementSpeed => baseMovementSpeed + bonusMovementSpeed;
    public float TotalFireRate => baseFireRate + bonusFireRate;
    public float TotalProjectileSpeed => baseProjectileSpeed + bonusProjectileSpeed;
    public float TotalDamage => baseDamage + bonusDamage;
    public float TotalCritDamage => baseCritDamage + bonusCritDamage;
    public float TotalCritChance => baseCritChance + bonusCritChance;
}
