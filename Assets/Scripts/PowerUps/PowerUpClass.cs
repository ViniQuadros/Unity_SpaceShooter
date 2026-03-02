using Unity.VisualScripting;
using UnityEngine;

public class PowerUpClass : MonoBehaviour
{
    public float speed = 4f;
    public float frequency = 5f;
    public float magnitude = 0.5f;

    private Rigidbody2D rb;
    private float time;
    private int direction = 1;
    private bool isUsed = false;

    protected string powerUpName;
    protected float duration = 10f;
    protected PowerUpsManager powerUpsManager;

    public void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        powerUpsManager = GameObject.FindGameObjectWithTag("PowerUpManager").GetComponent<PowerUpsManager>();
    }

    public void Start()
    {
        direction = transform.position.x >= 0 ? -1 : 1;
    }

    private void Update()
    {
        if (transform.position.x < 12 * direction && !isUsed)
        {
            Destroy(gameObject);
        }
    }

    void FixedUpdate()
    {
        time += Time.fixedDeltaTime;

        Vector2 forward = transform.right * speed * direction;
        Vector2 side = transform.up * Mathf.Sin(time * frequency) * magnitude;

        rb.linearVelocity = forward + side;
    }

    public virtual void ApplyPowerUp()
    {
        if (!isUsed)
        {
            powerUpsManager.SetIsPowerUpActive(true, powerUpName);
            Debug.Log("Power-Up Applied!");
            isUsed = true;
        }
    }

    public void DestoyAfterTime()
    {
        Destroy(gameObject);
    }
}
