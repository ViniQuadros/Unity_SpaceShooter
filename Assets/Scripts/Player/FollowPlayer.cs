using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player;
    public Vector3 offset = new Vector3(0, 0, -10);

    private void LateUpdate()
    {
        Vector3 targetPosition = player.position + offset;
        transform.position = targetPosition;
    }
}
