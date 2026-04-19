using UnityEngine;

public class EnemyShipBehavior : MonoBehaviour
{
    public enum State 
    { 
        Idle, 
        Chase, 
        Shoot, 
        Dead 
    }
    public State currentState = State.Idle;
    public SpriteRenderer boostRenderer;

    [Header("Detection")]
    public float detectionRadius = 10f;
    public float shootRadius = 8f;
    public float losePlayerRadius = 12f;

    [Header("Movement")]
    public float moveSpeed = 3f;
    public float rotationSpeed = 200f;

    [Header("Combat")]
    public float fireRate = 1.5f;
    public GameObject bulletPrefab;
    public Transform firePoint;

    private Transform player;
    private float fireCooldown;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (currentState == State.Dead) {
            boostRenderer.enabled = false;
            return;
        }

        float distToPlayer = Vector2.Distance(transform.position, player.position);

        switch (currentState)
        {
            case State.Idle:
                if (distToPlayer <= detectionRadius)
                    currentState = State.Chase;
                break;

            case State.Chase:
                if (distToPlayer > losePlayerRadius)
                    currentState = State.Idle;
                else if (distToPlayer <= shootRadius)
                    currentState = State.Shoot;
                break;

            case State.Shoot:
                if (distToPlayer > shootRadius)
                    currentState = State.Chase;
                break;
        }

        switch (currentState)
        {
            case State.Idle: HandleIdle(); break;
            case State.Chase: HandleChase(); break;
            case State.Shoot: HandleShoot(); break;
        }

        fireCooldown -= Time.deltaTime;
    }

    void HandleIdle()
    {
        // Need to add patrol behavior here
    }

    void HandleChase()
    {
        RotateToward(player.position);
        transform.Translate(Vector2.up * moveSpeed * Time.deltaTime);
    }

    void HandleShoot()
    {
        RotateToward(player.position);

        if (Vector2.Distance(transform.position, player.position) < shootRadius * 0.6f)
            transform.Translate(Vector2.down * moveSpeed * 0.5f * Time.deltaTime);

        if (fireCooldown <= 0f)
        {
            Shoot();
            fireCooldown = fireRate;
        }
    }

    void RotateToward(Vector3 target)
    {
        Vector2 dir = (target - transform.position).normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90f;
        float rot = Mathf.MoveTowardsAngle(
            transform.eulerAngles.z, angle,
            rotationSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(0, 0, rot);
    }

    void Shoot()
    {
        if (bulletPrefab && firePoint)
        {
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            AudioManager.audioManagerInstance.PlayLaserShoot();
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, shootRadius);
        Gizmos.color = Color.gray;
        Gizmos.DrawWireSphere(transform.position, losePlayerRadius);
    }
}
