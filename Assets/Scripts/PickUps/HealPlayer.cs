using UnityEngine;

public class HealPlayer : MonoBehaviour
{
    private PlayerHealth playerHealth;
    private int healAmount = 20;

    void Start()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerHealth.Heal(healAmount);
            Destroy(gameObject);
        }
    }
}
