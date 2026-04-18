using UnityEngine;

public class OrbitAroundPlayer : MonoBehaviour
{
    private Transform playerTransform;

    public float orbitSpeed = 1.5f;
    public float orbitRadiusDistance = 2.5f;

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void FixedUpdate()
    {
        if (playerTransform != null)
        {
            // Calculate the desired position on the orbit
            Vector3 offset = new Vector3(Mathf.Cos(Time.time * orbitSpeed) * orbitRadiusDistance, Mathf.Sin(Time.time * orbitSpeed) * orbitRadiusDistance, 0);
            transform.position = playerTransform.position + offset;
        }
    }
}
