using UnityEngine;

public class ConstantSpin : MonoBehaviour
{
    private float spinSpeed;
    private Vector3 spinDirection;

    void Start()
    {
        spinDirection = Random.value > 0.5f ? Vector3.forward : Vector3.back; //Right or left spin
        spinSpeed = Random.Range(5f, 20f);
    }

    void Update()
    {
        transform.Rotate(spinDirection, spinSpeed * Time.deltaTime);
    }
}
