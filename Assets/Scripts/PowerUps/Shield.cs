using System.Collections;
using UnityEngine;

public class Shield : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private bool isShieldActive = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void LoseShield()
    {
        if (isShieldActive)
        {
            isShieldActive = false;
            spriteRenderer.enabled = false;
            StartCoroutine(ReactivateShield());
        }
    }

    private IEnumerator ReactivateShield()
    {
        yield return new WaitForSeconds(20f);
        isShieldActive = true;
        spriteRenderer.enabled = true;
    }

    public bool IsShieldActive()
    {
        return isShieldActive;
    }
}
