using UnityEngine;

public class Step : MonoBehaviour
{
    [SerializeField]
    AudioSource stepAudioSource;

    void Steping()
    {
        stepAudioSource.Play();
    }
}
