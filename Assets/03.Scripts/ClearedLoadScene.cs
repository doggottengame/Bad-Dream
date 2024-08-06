using UnityEngine;
using UnityEngine.SceneManagement;

public class ClearedLoadScene : MonoBehaviour
{
    void DeadSceneFinished()
    {
        SceneManager.LoadScene("ClearScene");
    }
}
