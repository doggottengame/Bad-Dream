using UnityEngine;

public class ScreamSource : MonoBehaviour
{
    AudioSource audioSource;

    private void Awake()
    {
        transform.SetParent(null);
        DontDestroyOnLoad(gameObject);
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (!audioSource.isPlaying) Destroy(gameObject);
    }
}
