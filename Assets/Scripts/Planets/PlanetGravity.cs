using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PlanetGravity : MonoBehaviour
{
    private CapsuleCollider2D gravityArea;
    private float gravityStrength = 10f;

    void Start()
    {
        gravityArea = GetComponent<CapsuleCollider2D>();

        gravityStrength = gravityArea.size.x * Random.Range(2, 3);
        Debug.Log(gameObject.name + " " + transform.localScale.magnitude);
    }

    void FixedUpdate()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, gravityArea.size.x);
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Player"))
            {
                Vector2 direction = transform.position - collider.transform.position;
                collider.GetComponent<Rigidbody2D>().AddForce(direction.normalized * gravityStrength);
            }
        }
    }
}
