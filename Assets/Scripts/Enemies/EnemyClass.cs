using System.Collections;
using UnityEngine;

public class EnemyClass : MonoBehaviour
{
    public float maxHealth = 150;

    protected float currentHealth;
    protected Rigidbody2D rb;
    protected Collider2D boxCollider;
    protected Color damageColor;

    private int scoreValue = 10; //For classic mode
    private SpriteRenderer spriteRenderer;
    private bool isDead = false;

    protected virtual void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
    }

    public virtual void TakeDamage(float damage, bool isCriticalDamage)
    {
        if (isDead)
            return;

        currentHealth -= damage;

        if (isCriticalDamage)
            damageColor = Color.darkViolet;
        else
            damageColor = Color.red;

        StartCoroutine(FlashRed());

        if (currentHealth <= 0f)
        {
            isDead = true;

            if (GameManager.Instance.GetCurrentScene() == "Classic Mode")
                ScoreManager.scoreManagerInstance.AddScore(scoreValue);

            if (GameManager.Instance.GetCurrentScene() == "Roguelike Mode")
            {
                GiveExpToPlayer exp = GetComponent<GiveExpToPlayer>();
                exp.GiveExp();
            }

            StartCoroutine(Die());
        }
    }

    public IEnumerator FlashRed()
    {
        Color originalColor = spriteRenderer.color;
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = originalColor;
    }

    public IEnumerator Die()
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
