using UnityEngine;

public class DeadSceneCabinet_0 : MonoBehaviour
{
    [SerializeField]
    GameObject deadScene;
    [SerializeField]
    AudioSource screamSource, tearSource;
    GameObject playerObj;

    private void Start()
    {
        playerObj = GameManager.instance.playerObj;

        RaycastHit hit;
        Physics.Raycast(transform.GetChild(0).position, playerObj.transform.GetChild(0).position - transform.GetChild(0).position, out hit);
        if (hit.collider == null) return;
        if (hit.collider.gameObject.layer == 16)
        {
            Destroy(hit.collider.gameObject.GetComponent<Door>());
        }
    }

    void TearDoor()
    {
        tearSource.Play();
    }

    void DeadSceneAnimFinished()
    {
        screamSource.Play();
        screamSource.GetComponent<ScreamSource>().enabled = true;
        playerObj.SetActive(false);
        Instantiate(deadScene, playerObj.transform.position, playerObj.transform.rotation);
        Destroy(gameObject);
    }
}
