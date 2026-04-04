using UnityEngine;

public class AsteroidsManager : MonoBehaviour
{
    [SerializeField] private GameObject[] asteroidPrefab;
    [SerializeField] private Transform[] spawnPoints;
    public PlayerControl playerControl;
    private float maxSpawnTime = 3f;

    void Start()
    {
        Invoke(nameof(SpawnAsteroids), 2f);
    }

    void SpawnAsteroids()
    {
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        //Chose a random angle towards the player
        Vector2 direction = (playerControl.transform.position - spawnPoint.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        float marginAngle = Random.Range(-20f, 20f) + 90f;
        Quaternion rotation = Quaternion.Euler(0f, 0f, angle - marginAngle);

        Instantiate(
            asteroidPrefab[Random.Range(0, asteroidPrefab.Length)],
            spawnPoint.position,
            rotation
        );

        //Decrease spawn time every 50 points to increase difficulty
        if (ScoreManager.scoreManagerInstance != null)
        {
            if (ScoreManager.scoreManagerInstance.GetScore() % 50 == 0 && maxSpawnTime > 1f)
            {
                maxSpawnTime -= 0.2f;
            }
        }

        Invoke(nameof(SpawnAsteroids), Random.Range(1f, maxSpawnTime));
    }
}
