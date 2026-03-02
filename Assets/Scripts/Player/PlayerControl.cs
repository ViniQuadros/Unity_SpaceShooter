using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] private GameObject laserBean;
    public Transform firePoint;
    public GameManager gameManager;

    private bool isInstaKillActive = false;

    private void Update()
    {
        if (Input.GetMouseButtonDown((int) MouseButton.Left))
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
        gameManager.ShowResults();
        Destroy(gameObject);
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
