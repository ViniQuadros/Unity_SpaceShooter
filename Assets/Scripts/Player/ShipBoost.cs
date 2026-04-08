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

    private float reduceRate = 0.3f;
    private float increaseRate = 0.1f;
    private bool isBoosting = false;
    private Rigidbody2D rb;

    void Start()
    {
        boostSlider.maxValue = 1f;
        boostSlider.value = 1f;
        fillImage.color = Color.blue; // Initial color for full boost
        boostSprite.enabled = false;

        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            isBoosting = true;
            boostSprite.enabled = true; // Show boost animation
        }

        if (Input.GetKey(KeyCode.LeftShift) && isBoosting)
        {
            if (boostSlider.value > 0)
            {
                boostSlider.value -= reduceRate * Time.deltaTime;
            }

            //Visual feedback for boost level
            if (boostSlider.value <= 0.5 && boostSlider.value > 0.2)
            {
                fillImage.color = Color.yellow;
            }
            if (boostSlider.value <= 0.2)
            {
                fillImage.GetComponent<Image>().color = Color.red;
            }

            rb.AddForce(transform.up * boostForce, ForceMode2D.Force);
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            isBoosting = false;
            boostSprite.enabled = false; // Hide boost animation
        }

        if (!isBoosting)
        {
            boostSlider.value += increaseRate * Time.deltaTime;

            if (boostSlider.value >= 0.2 && boostSlider.value < 0.5)
            {
                fillImage.GetComponent<Image>().color = Color.yellow;
            }
            if (boostSlider.value >= 0.5)
            {
                fillImage.color = Color.blue;
            }
        }
    }
}
