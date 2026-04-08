using UnityEngine;

public class Parallax : MonoBehaviour
{
    [System.Serializable]
    public class ParallaxLayer
    {
        public Transform layerTransform;
        [Range(0f, 1f)] public float parallaxFactor; // 0 = locked, 1 = moves with camera
    }

    [SerializeField] private ParallaxLayer[] layers;
    private Transform cam;
    private Vector3 lastCamPos;

    void Start()
    {
        cam = Camera.main.transform;
        lastCamPos = cam.position;
    }

    void LateUpdate()
    {
        Vector3 delta = cam.position - lastCamPos;

        foreach (var layer in layers)
            layer.layerTransform.position += new Vector3(delta.x * layer.parallaxFactor,
                                                         delta.y * layer.parallaxFactor, 0);
        lastCamPos = cam.position;
    }
}