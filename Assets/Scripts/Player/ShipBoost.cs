using UnityEngine;
using UnityEngine.UI;

public class ShipBoost : MonoBehaviour
{
    public Slider boostSlider;
    public Image fillImage;
    public float boostForce = 10f;

    private float reduceRate = 0.05f;
    private float increaseRate = 0.1f;
    private bool isBoosting = false;
    private Rigidbody2D rb;

    void Start()
    {
        boostSlider.maxValue = 1f;
        boostSlider.value = 1f;
        fillImage.color = Color.blue; // Initial color for full boost

        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            isBoosting = true;
        }

        if (Input.GetKey(KeyCode.LeftShift) && isBoosting)
        {
            boostSlider.value -= reduceRate * Time.deltaTime;

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
