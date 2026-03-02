using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource audioSource;

    public AudioClip laserShoot;
    public AudioClip asteroidExplosion;

    public static AudioManager audioManagerInstance { get; private set; }

    void Awake()
    {
        if (audioManagerInstance != null && audioManagerInstance != this)
        {
            Destroy(gameObject);
            return;
        }
        audioManagerInstance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayLaserShoot()
    {
        audioSource.PlayOneShot(laserShoot, 0.7f);
    }

    public void PlayAsteroidExplosion()
    {
        audioSource.PlayOneShot(asteroidExplosion);
    }
}
