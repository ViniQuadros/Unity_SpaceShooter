using UnityEngine;

public class EnemyShipHealth : EnemyClass
{
    private EnemyShipBehavior behavior;

    protected override void Start()
    {
        base.Start();
        behavior = GetComponent<EnemyShipBehavior>();
    }

    public override void TakeDamage(float damage, bool isCriticalDamage)
    {
        base.TakeDamage(damage, isCriticalDamage);
        if (currentHealth <= 0f)
        {
            behavior.currentState = EnemyShipBehavior.State.Dead;
        }
    }
}
