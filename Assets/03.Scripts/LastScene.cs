using UnityEngine;
using UnityEngine.SceneManagement;

public class LastScene : MonoBehaviour
{
    void AnimFinished()
    {
        SceneManager.LoadScene("Title");
    }
}
