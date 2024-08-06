using UnityEngine;

public class InClearScene : MonoBehaviour
{
    [SerializeField]
    Animator animator;
    bool waitInput;
    [SerializeField]
    GameObject monsterAnimObj;

    // Update is called once per frame
    void Update()
    {
        if (!waitInput) return;
        animator.SetTrigger("Set");
    }

    void AnimFinished()
    {
        waitInput = true;
    }

    void LastAnimFinished()
    {
        monsterAnimObj.SetActive(true);
        Destroy(gameObject);
    }
}
