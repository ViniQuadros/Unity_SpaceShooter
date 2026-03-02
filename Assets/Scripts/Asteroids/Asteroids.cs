using System.Collections;
using UnityEngine;

public class Asteroids : MonoBehaviour
{
    public float minSpeed = 1.0f;
    public float maxSpeed = 4.0f;
    public int scoreValue = 10;
    public float minScale = 0.6f;
    public float maxScale = 1.5f;

    private float health = 10f;
    private bool isDead = false;
    private Rigidbody2D rb;

    private SpriteRenderer spriteRenderer;
    private Collider2D boxCollider;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        float speed = Random.Range(minSpeed, maxSpeed + 1);
        rb.AddForce(transform.up * speed, ForceMode2D.Impulse);

        float randomScale = Random.Range(minScale, maxScale);
        transform.localScale = new Vector3(randomScale, randomScale, randomScale);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerControl>().Die();
            Destroy(gameObject);
        }
    }

    public void TakeDamage(float damage)
    {
        if (isDead)
            return;

        health -= damage;
        StartCoroutine(FlashRed());

        if (health <= 0f)
        {
            isDead = true;
            ScoreManager.scoreManagerInstance.AddScore(scoreValue);
            StartCoroutine(Die());
        }
    }

    private IEnumerator FlashRed()
    {
        Color originalColor = spriteRenderer.color;
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = originalColor;
    }

    private IEnumerator Die()
    {
        AudioManager.audioManagerInstance.PlayAsteroidExplosion();
        spriteRenderer.enabled = false;
        boxCollider = GetComponent<Collider2D>();
        boxCollider.enabled = false;
        rb.linearVelocity = Vector2.zero;
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
