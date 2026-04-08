using System.Collections;
using UnityEngine;

public class LaserBean : MonoBehaviour
{
    private Rigidbody2D rb;
    private PlayerControl playerControl;

    public PlayerStats playerStats;

    private float bonusDamage = 0f;
    private float bonusSpeed = 0f;
    private float bonusCritChance = 0f;
    private float bonusCritDamage = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerControl = GameObject.FindWithTag("Player").GetComponent<PlayerControl>();

        rb.AddForce(transform.up * (playerStats.baseProjectileSpeed + bonusSpeed), ForceMode2D.Impulse);
        Invoke(nameof(DestroyBean), 5f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (playerControl.IsInstaKillActive())
        {
            playerStats.baseDamage = 1000f;
        }

        if (other.CompareTag("PowerUp"))
        {
            other.GetComponent<PowerUpClass>().ApplyPowerUp();
            other.GetComponent<SpriteRenderer>().enabled = false;
            DestroyBean();
        }

        if (other.CompareTag("Asteroid"))
        {
            float rand = Random.Range(0f, 100f);
            if (rand <= playerStats.baseCritChance + bonusCritChance)
            {
                float totalCritDamage = playerStats.baseCritDamage + bonusCritDamage;
                other.GetComponent<Asteroids>().TakeDamage(totalCritDamage, true);
            }
            else
            {
                float totalDamage = playerStats.baseDamage + bonusDamage;
                other.GetComponent<Asteroids>().TakeDamage(totalDamage, false);
            }
            DestroyBean();
        }
    }

    private void DestroyBean()
    {
        Destroy(gameObject);
    }

    public void SetBonusDamage(float value) => bonusDamage = Mathf.Max(0f, value);
    public void SetBonusSpeed(float value) => bonusSpeed = Mathf.Max(0f, value);
    public void SetBonusCritChance(float value) => bonusCritChance = Mathf.Clamp(value, 0f, 100f);
    public void SetBonusCritDamage(float value) => bonusCritDamage = Mathf.Max(0f, value);
}
