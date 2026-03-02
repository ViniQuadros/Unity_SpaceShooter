using System.Collections;
using UnityEngine;

public class PU_InstaKill : PowerUpClass
{
    public override void ApplyPowerUp()
    {
        powerUpName = "Strength Increased";

        StartCoroutine(PowerUpDuration());

        base.ApplyPowerUp();
    }

    private IEnumerator PowerUpDuration()
    {
        PlayerControl player = GameObject.FindWithTag("Player").GetComponent<PlayerControl>();
        player.ActivateInstaKill(true);
        yield return new WaitForSeconds(duration);
        player.ActivateInstaKill(false);
        Destroy(gameObject);
    }
}
