using System.Collections;
using UnityEngine;

public class LaserBean : MonoBehaviour
{
    private Rigidbody2D rb;
    private PlayerControl playerControl;

    public float speed = 20f;
    public float damage = 1f;
    public float critDamage = 3f;
    public float critChance = 10f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerControl = GameObject.FindWithTag("Player").GetComponent<PlayerControl>();

        rb.AddForce(transform.up * speed, ForceMode2D.Impulse);
        Invoke(nameof(DestroyBean), 5f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (playerControl.IsInstaKillActive())
        {
            damage = 1000f;
        }

        if (other.CompareTag("Asteroid"))
        {
            float rand = Random.Range(0f, 100f);
            if (rand <= critChance)
            {
                other.GetComponent<Asteroids>().TakeDamage(critDamage);
            }
            else
            {
                other.GetComponent<Asteroids>().TakeDamage(damage);
            }
            DestroyBean();
        }

        if (other.CompareTag("PowerUp"))
        {
            other.GetComponent<PowerUpClass>().ApplyPowerUp();
            other.GetComponent<SpriteRenderer>().enabled = false;
            DestroyBean();
        }
    }

    private void DestroyBean()
    {
        Destroy(gameObject);
    }
}
