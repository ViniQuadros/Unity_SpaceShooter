using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStats", menuName = "Scriptable Objects/PlayerStats")]
public class PlayerStats : ScriptableObject
{
    [Header("Speed Stats")]
    public float baseMovementSpeed;

    [Header("Health Stats")]
    public int baseMaxHealth;

    [Header("Combat Stats")]
    public float baseFireRate;

    [Header("Laser Stats")]
    public float baseProjectileSpeed;
    public float baseDamage;
    public float baseCritDamage;
    public float baseCritChance;
}
