using System.Collections;
using UnityEngine;

public class HelperShip : MonoBehaviour
{
    public GameObject laserPrefab;
    public Transform laserSpawnPoint;
    public LayerMask enemiesMask;

    public float detectionRadius = 5f;
    public float shootCooldown = 5f;

    void Start()
    {
        StartCoroutine(ShootingLoop());
    }

    private IEnumerator ShootingLoop()
    {
        while (true)
        {
            GameObject target = FindClosestEnemy();

            if (target != null)
            {
                LookAt(target.transform);
                Shoot();
            }

            yield return new WaitForSeconds(shootCooldown);
        }
    }

    private GameObject FindClosestEnemy()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, detectionRadius, enemiesMask);

        GameObject closest = null;
        float closestDistance = Mathf.Infinity;

        foreach (Collider2D col in hits)
        {
            float distance = Vector2.Distance(transform.position, col.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closest = col.gameObject;
            }
        }

        return closest;
    }

    private void LookAt(Transform target)
    {
        Vector2 direction = target.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    private void Shoot()
    {
        Instantiate(laserPrefab, laserSpawnPoint.position, transform.rotation);
    }
}