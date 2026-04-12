using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ShipBoost : MonoBehaviour
{
    [Header("Slider Infos")]
    public Slider boostSlider;
    public Image fillImage;
    public float boostForce = 5f;

    [Header("Boost Animation")]
    public SpriteRenderer boostSprite;

    private float regenerationDelay = 2f;
    private float reduceRate = 0.3f;
    private float increaseRate = 0.1f;
    private bool isBoosting = false;
    private bool isRegenerating = false; // ← guard flag
    private Rigidbody2D rb;

    void Start()
    {
        boostSlider.maxValue = 1f;
        boostSlider.value = 1f;
        fillImage.color = Color.blue;
        boostSprite.enabled = false;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && boostSlider.value > 0)
        {
            isBoosting = true;
            boostSprite.enabled = true;
            StopCoroutine(RegenerateBoost()); // cancel regen if boosting again
            isRegenerating = false;
        }

        if (Input.GetKey(KeyCode.LeftShift) && isBoosting)
        {
            if (boostSlider.value > 0)
            {
                boostSlider.value -= reduceRate * Time.deltaTime;
                rb.AddForce(transform.up * boostForce, ForceMode2D.Force);
            }
            else
            {
                // Force stop if boost runs out mid-hold
                isBoosting = false;
                boostSprite.enabled = false;
            }

            UpdateSliderColor();
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            isBoosting = false;
            boostSprite.enabled = false;
        }

        if (!isBoosting && !isRegenerating && boostSlider.value < boostSlider.maxValue)
        {
            StartCoroutine(RegenerateBoost());
        }
    }

    private IEnumerator RegenerateBoost()
    {
        isRegenerating = true;

        yield return new WaitForSeconds(regenerationDelay);

        while (boostSlider.value < boostSlider.maxValue && !isBoosting)
        {
            boostSlider.value += increaseRate * Time.deltaTime;
            boostSlider.value = Mathf.Clamp(boostSlider.value, 0f, boostSlider.maxValue);
            UpdateSliderColor();
            yield return null;
        }

        isRegenerating = false;
    }

    private void UpdateSliderColor()
    {
        if (boostSlider.value <= 0.2f)
            fillImage.color = Color.red;
        else if (boostSlider.value <= 0.5f)
            fillImage.color = Color.yellow;
        else
            fillImage.color = Color.blue;
    }

    public void ReduceConsumption(float modifier)
    {
        reduceRate = reduceRate * (1 - modifier);
    }
}