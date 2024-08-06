using UnityEngine;
using UnityEngine.SceneManagement;

public class DeadScene : MonoBehaviour
{
    void DeadSceneFinished()
    {
        SceneManager.LoadScene("DeadDelay");
    }
}
