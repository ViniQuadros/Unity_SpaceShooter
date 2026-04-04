using System.Collections;
using UnityEngine;

public class Asteroids : MonoBehaviour
{
    [Header("Speed Config")]
    public float minSpeed = 1.0f;
    public float maxSpeed = 4.0f;

    [Header("Scale Config")]
    public float minScale = 0.6f;
    public float maxScale = 1.5f;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Collider2D boxCollider;
    private Color damageColor;
    private float health = 10f;
    private bool isDead = false;

    private int scoreValue = 10; //For classic mode

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        //Apply random speed and scale to the asteroid
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

    public void TakeDamage(float damage, bool isCriticalDamage)
    {
        if (isDead)
            return;

        health -= damage;

        if (isCriticalDamage)
            damageColor = Color.darkViolet;
        else
            damageColor = Color.red;

        StartCoroutine(FlashRed());

        if (health <= 0f)
        {
            isDead = true;

            if (RoguelikeGameManager.Instance.GetCurrentScene() == "Classic Mode")
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
