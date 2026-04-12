using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public PlayerStats playerStats;

    [Header("Laser Settings")]
    public GameObject laserBean;
    public Transform firePoint;

    private GameManager gameManager;
    private string currentScene;

    private bool isInstaKillActive = false;
    private float bonusMovementSpeed = 0f;
    private float reducedFireRate = 0f;

    private PlayerStats.PlayerMode currentMode;
    private PlayerStats.PlayerState currentState = PlayerStats.PlayerState.Idle;

    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        currentScene = gameManager.GetCurrentScene();
        currentMode = currentScene == "Classic Mode" ? PlayerStats.PlayerMode.Classic : PlayerStats.PlayerMode.Roguelike;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown((int) MouseButton.Left))
        {
            if (currentState != PlayerStats.PlayerState.Dead && currentState == PlayerStats.PlayerState.Idle)
            {
                Instantiate(laserBean, firePoint.position, firePoint.rotation);
                AudioManager.audioManagerInstance.PlayLaserShoot();
                StartCoroutine(ControlFireRate());
            }
        }

        if (currentMode == PlayerStats.PlayerMode.Roguelike)
        {
            float moveX = Input.GetAxis("Horizontal");
            float moveY = Input.GetAxis("Vertical");

            float finalMovementSpeed = playerStats.baseMovementSpeed + bonusMovementSpeed;
            Vector3 move = new Vector3(moveX, moveY, 0f) * finalMovementSpeed * Time.deltaTime;

            transform.Translate(move);
        }
    }

    void FixedUpdate()
    {
        //Spaceship follows the mouse
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePosition - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90f));
    }

    public void Die()
    {
        currentState = PlayerStats.PlayerState.Dead;
        gameManager.ShowResults();
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }

    public void ActivateInstaKill(bool activate)
    {
        isInstaKillActive = activate;
    }

    public bool IsInstaKillActive()
    {
        return isInstaKillActive;
    }

    private IEnumerator ControlFireRate()
    {
        currentState = PlayerStats.PlayerState.Shooting;
        float finalFireRate = playerStats.baseFireRate - reducedFireRate;
        yield return new WaitForSeconds(finalFireRate);
        currentState = PlayerStats.PlayerState.Idle;
    }

    public void ReduceFireRate(float value)
    {
        reducedFireRate = Mathf.Max(0f, reducedFireRate + value);
    }
}
