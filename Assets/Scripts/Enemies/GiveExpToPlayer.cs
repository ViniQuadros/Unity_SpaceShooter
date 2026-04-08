using UnityEngine;

public class GiveExpToPlayer : MonoBehaviour
{
    [Header("Exp Limits")]
    public int minExp = 10;
    public int maxExp = 20;

    public void GiveExp()
    {
        int expToGive = Random.Range(minExp, maxExp + 1);
        PlayerExperience playerExp = FindFirstObjectByType<PlayerExperience>();
        if (playerExp != null)
        {
            playerExp.AddExperience(expToGive);
        }
    }
}
