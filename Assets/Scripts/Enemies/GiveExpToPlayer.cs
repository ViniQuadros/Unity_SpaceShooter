using UnityEngine;

public class GiveExpToPlayer : MonoBehaviour
{
    [Header("Exp Limits")]
    public float minExp = 90;
    public float maxExp = 100;

    public void GiveExp()
    {
        float expToGive = Random.Range(minExp, maxExp + 1);
        PlayerExperience playerExp = FindFirstObjectByType<PlayerExperience>();
        if (playerExp != null)
        {
            playerExp.AddExperience(expToGive);
        }
    }
}
