using Unity.VisualScripting;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] private GameObject laserBean;
    public Transform firePoint;
    public GameManager gameManager;

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
    }

    void FixedUpdate()
    {
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
