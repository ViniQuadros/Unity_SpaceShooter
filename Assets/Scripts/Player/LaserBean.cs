using System.Collections;
using UnityEngine;

public class LaserBean : MonoBehaviour
{
    private Rigidbody2D rb;
    private PlayerControl playerControl;
    private PlayerStats playerStats;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerControl = GameObject.FindWithTag("Player").GetComponent<PlayerControl>();
        playerStats = GameObject.FindWithTag("Player").GetComponent<PlayerStats>();

        rb.AddForce(transform.up * playerStats.TotalProjectileSpeed, ForceMode2D.Impulse);
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

        if (other.CompareTag("Asteroid") || other.CompareTag("Enemy"))
        {
            float rand = Random.Range(0f, 100f);
            if (rand <= playerStats.TotalCritChance)
            {
                float totalCritDamage = playerStats.TotalCritDamage;
                other.GetComponent<EnemyClass>().TakeDamage(totalCritDamage, true);
            }
            else
            {
                float totalDamage = playerStats.TotalDamage;
                other.GetComponent<EnemyClass>().TakeDamage(totalDamage, false);
            }
            DestroyBean();
        }
    }

    private void DestroyBean()
    {
        Destroy(gameObject);
    }
}
