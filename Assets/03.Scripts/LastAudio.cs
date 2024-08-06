using UnityEngine;

public class LastAudio : MonoBehaviour
{
    [SerializeField]
    AudioSource audioSource;
    [SerializeField]
    GameObject LastAnim;

    void AudioPlay()
    {
        audioSource.Play();
    }

    void AnimFinished()
    {
        LastAnim.SetActive(true);
        Destroy(gameObject.transform.parent.gameObject);
    }
}
