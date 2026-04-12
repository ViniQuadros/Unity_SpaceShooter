using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlanetData : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite[] planetSprites; // assign variants in Inspector
    [SerializeField] private float minScale = 0.5f;
    [SerializeField] private float maxScale = 2.5f;

    private Light2D planetLight;

    private void Start()
    {
        planetLight = GetComponentInChildren<Light2D>();
        if (planetLight == null)
            Debug.LogWarning("PlanetData: No Light2D found. Lighting effects will be missing.");
    }

    public void Randomize(int seed)
    {
        Random.State prev = Random.state;
        Random.InitState(seed);

        // Random values for each planet
        spriteRenderer.sprite = planetSprites[Random.Range(0, planetSprites.Length)];
        spriteRenderer.color = Random.ColorHSV(0f, 1f, 0.5f, 1f, 0.95f, 1f);
        float scale = Random.Range(minScale, maxScale);
        transform.localScale = Vector3.one * scale;

        //Ajust light based on scale
        planetLight.color = spriteRenderer.color * 1.2f;
        planetLight.intensity = Mathf.Lerp(0.5f, 2f, (scale - minScale) / (maxScale - minScale));
        planetLight.pointLightOuterRadius = scale * 2f;
        planetLight.pointLightInnerRadius = scale;

        //Ajust gravity based on scale
        PlanetGravity gravity = GetComponent<PlanetGravity>();
        float strength = Mathf.Lerp(0.5f, 2f, (scale - minScale) / (maxScale - minScale));
        gravity.SetGravityProperties(strength, scale * 2f);

        Random.state = prev;
    }
}