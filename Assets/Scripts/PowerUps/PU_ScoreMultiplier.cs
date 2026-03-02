using System.Collections;
using UnityEngine;

public class PU_ScoreMultiplier : PowerUpClass
{
    public override void ApplyPowerUp()
    {
        powerUpName = "Score Multiplier";
        base.ApplyPowerUp();
        StartCoroutine(ResetScoreMultiplierAfterTime(duration));
    }

    public IEnumerator ResetScoreMultiplierAfterTime(float duration)
    {
        ScoreManager.scoreManagerInstance.SetIsDoubled(true);
        yield return new WaitForSeconds(duration);
        ScoreManager.scoreManagerInstance.SetIsDoubled(false);
        Destroy(gameObject);
    }
}
