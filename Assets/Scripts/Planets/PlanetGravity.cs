using UnityEngine;

public class PlanetGravity : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Entered gravity field of " + gameObject.name);
    }
}
