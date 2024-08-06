using UnityEngine;

public class DeadSceneElevator_1 : MonoBehaviour
{
    [SerializeField]
    GameObject deadScene;
    [SerializeField]
    AudioSource brokeSource;

    void BrokeDoor()
    {
        brokeSource.Play();
        brokeSource.GetComponent<ScreamSource>().enabled = true;
    }

    void AnimFinished()
    {
        GameObject playerObj = GameManager.instance.playerObj;
        playerObj.SetActive(false);
        Instantiate(deadScene, playerObj.transform.position, playerObj.transform.rotation);
        Destroy(gameObject);
    }
}
