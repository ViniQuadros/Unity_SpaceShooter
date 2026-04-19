using System.Collections;
using UnityEngine;

public class Asteroids : EnemyClass
{
    [Header("Speed Config")]
    public float minSpeed = 1.0f;
    public float maxSpeed = 4.0f;

    [Header("Scale Config")]
    public float minScale = 0.5f;
    public float maxScale = 2.5f;

    private float damage;

    protected override void Start()
    {
        base.Start();

        //Apply random speed and scale to the asteroid
        float speed = Random.Range(minSpeed, maxSpeed + 1);
        rb.AddForce(transform.up * speed, ForceMode2D.Impulse);

        float randomScale = Random.Range(minScale, maxScale);
        transform.localScale = new Vector3(randomScale, randomScale, randomScale);

        //Set health and damage based on scale
        maxHealth = randomScale * 10f;
        currentHealth = maxHealth;
        damage = randomScale * 5f;

        if (GameManager.Instance.GetCurrentScene() == "Classic Mode")
            damage = 9999f; //Instant death in classic mode
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerHealth>().TakeDamage(damage);
            StartCoroutine(Die());
        }
    }
}
