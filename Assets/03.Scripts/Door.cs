using UnityEngine;

public class Door : MonoBehaviour
{
    Animator animator;
    bool onDoorAnim;
    [SerializeField]
    AudioClip openClip, closeClip;
    [SerializeField]
    GameObject target;
    GameObject obj;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public AudioClip DoorAction()
    {
        if (onDoorAnim) return null;
        onDoorAnim = true;

        if (animator.GetBool("Open"))
        {
            animator.SetBool("Open", false);
            animator.SetTrigger("Set");
            return closeClip;
        }
        else
        {
            animator.SetBool("Open", true);
            animator.SetTrigger("Set");
            return openClip;
        }
    }

    void DoorAnimationFinished()
    {
        onDoorAnim = false;
    }
}
