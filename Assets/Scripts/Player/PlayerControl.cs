using Unity.VisualScripting;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [Header("Laser Settings")]
    [SerializeField] private GameObject laserBean;
    public Transform firePoint;

    [Header("Movement Settings")]
    public float moveSpeed = 5f;

    private GameManager gameManager;
    private bool isInstaKillActive = false;
    private string currentScene;

    private enum PlayerMode
    {
        Classic,
        Roguelike
    }
    private PlayerMode currentMode;

    private enum PlayerState
    {
        Idle,
        Moving,
        Shooting,
        Dead
    }
    private PlayerState currentState = PlayerState.Idle;

    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        currentScene = gameManager.GetCurrentScene();
        currentMode = currentScene == "Classic Mode" ? PlayerMode.Classic : PlayerMode.Roguelike;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown((int) MouseButton.Left) && currentState != PlayerState.Dead)
        {
            Instantiate(laserBean, firePoint.position, firePoint.rotation);
            AudioManager.audioManagerInstance.PlayLaserShoot();
        }

        if (currentMode == PlayerMode.Roguelike)
        {
            float moveX = Input.GetAxis("Horizontal");
            float moveY = Input.GetAxis("Vertical");

            Vector3 move = new Vector3(moveX, moveY, 0f) * moveSpeed * Time.deltaTime;

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
        currentState = PlayerState.Dead;
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
}
