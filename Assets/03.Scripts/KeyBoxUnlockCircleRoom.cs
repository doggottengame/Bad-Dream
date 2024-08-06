using UnityEngine;

public class KeyBoxUnlockCircleRoom : MonoBehaviour
{
    KeyBox keyBox;

    [SerializeField]
    Animator screenAnim;

    private void Awake()
    {
        keyBox = GetComponent<KeyBox>();
        keyBox.keyboxUnlockAction = () =>
        {
            screenAnim.SetTrigger("Set");
        };
    }
}
