using UnityEngine;

public class Shield : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private bool isShieldActive = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
