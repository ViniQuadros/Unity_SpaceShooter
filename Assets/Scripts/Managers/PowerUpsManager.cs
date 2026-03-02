using System.Collections;
using TMPro;
using UnityEngine;

public class PowerUpsManager : MonoBehaviour
{
    [SerializeField] private GameObject[] PU_prefabes;
    [SerializeField] private Transform[] spawnPoints;

    public float minTimeSpawn = 5f;
    public float maxTimeSpawn = 10f;
    private float spawnInterval;
    private bool isPowerUpActive = false;
    public TextMeshProUGUI activePowerUp;

    void Start()
    {
        spawnInterval = Random.Range(minTimeSpawn, maxTimeSpawn);
        Invoke(nameof(SpawnPowerUps), spawnInterval);
    }

    private void SpawnPowerUps()
    {
        if (!isPowerUpActive)
        {
            Instantiate(
            PU_prefabes[Random.Range(0, PU_prefabes.Length)],
            spawnPoints[Random.Range(0, spawnPoints.Length)].position,
            Quaternion.identity
            );
        }

        spawnInterval = Random.Range(minTimeSpawn, maxTimeSpawn);
        Invoke(nameof(SpawnPowerUps), spawnInterval);
    }

    public void SetIsPowerUpActive(bool isActive, string powerUpName)
    {
        isPowerUpActive = isActive;

        StartCoroutine(PowerUpDuration(powerUpName));
    }

    private IEnumerator PowerUpDuration(string powerUpName)
    {
        float elapsedTime = 10f;
        activePowerUp.gameObject.SetActive(true);

        while (elapsedTime >= 0f)
        {
            elapsedTime -= Time.deltaTime;
            activePowerUp.text = powerUpName + ": " + ((int)elapsedTime).ToString();
            yield return null;
        }

        isPowerUpActive = false;
        activePowerUp.gameObject.SetActive(false);
    }
}
