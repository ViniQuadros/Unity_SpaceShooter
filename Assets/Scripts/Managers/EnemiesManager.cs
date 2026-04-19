using UnityEngine;

public class EnemiesManager : MonoBehaviour
{
    [SerializeField] private GameObject[] enemiesPrefab;
    [SerializeField] private Transform[] spawnPoints;
    public PlayerControl playerControl;
    public PlayerExperience playerExperience;

    private float maxSpawnTime = 3f;
    private int arrayLength = 2;

    private bool insertNewEnemyType = false;

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

        if (playerExperience.GetCurrentPlayerLevel() == 3 && !insertNewEnemyType)
        {
            arrayLength = enemiesPrefab.Length;
            insertNewEnemyType = true;
        }

        Instantiate(
            enemiesPrefab[Random.Range(0, arrayLength)],
            spawnPoint.position,
            rotation
        );

        //Decrease spawn time every 50 points to increase difficulty if in Classic mode
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
