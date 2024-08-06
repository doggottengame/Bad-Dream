using System.Collections.Generic;
using UnityEngine;

public class ExitDoor : MonoBehaviour
{
    Animator animator;
    AudioSource audioSource;
    [SerializeField]
    AudioClip openClip, openSuccessClip, openFailedClip;

    bool locked = true;

    [SerializeField]
    byte needKeyNum;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    public void TryDoorOpen(ref List<byte> keyV)
    {
        if (locked)
        {
            foreach (byte i in keyV)
            {
                if (i == needKeyNum)
                {
                    locked = false;
                    audioSource.PlayOneShot(openSuccessClip);
                    break;
                }
            }
            if (locked) audioSource.PlayOneShot(openFailedClip);
        }
        else
        {
            Destroy(GetComponent<BoxCollider>());
            audioSource.PlayOneShot(openClip);
            animator.SetTrigger("Open");
        }
    }
}
