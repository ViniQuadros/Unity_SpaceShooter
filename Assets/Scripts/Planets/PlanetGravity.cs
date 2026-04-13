using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PlanetGravity : MonoBehaviour
{
    private CapsuleCollider2D gravityArea;
    public float gravityStrength = 10f;

    void Start()
    {
        gravityArea = GetComponent<CapsuleCollider2D>();
    }

    void FixedUpdate()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, gravityArea.size.x);
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Player"))
            {
                Vector2 direction = transform.position - collider.transform.position;
                float distance = direction.magnitude;

                collider.GetComponent<Rigidbody2D>().AddForce(direction.normalized * gravityStrength);
            }
        }
    }

    public void SetGravityProperties(float scale)
    {
        gravityArea.size = new Vector2(scale * 5f, scale * 5f);
        gravityStrength = scale * scale * 10f;                 
    }
}
