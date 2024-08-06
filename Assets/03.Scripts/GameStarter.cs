using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStarter : MonoBehaviour
{
    [SerializeField]
    GameObject blockScreen, audioSource;

    void DeadSceneFinished()
    {
        SceneManager.LoadScene("PlayScene");
        blockScreen.SetActive(true);
        Destroy(audioSource);
        Destroy(gameObject);
    }
}
