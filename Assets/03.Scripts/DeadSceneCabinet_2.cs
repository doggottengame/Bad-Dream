using UnityEngine;

public class DeadSceneCabinet_2 : MonoBehaviour
{
    [SerializeField]
    GameObject deadScene;
    GameObject playerObj;
    [SerializeField]
    Transform rayStartTrans, lightTrans;
    [SerializeField]
    AudioSource screamSource, tearSource;

    // Start is called before the first frame update
    void Awake()
    {
        playerObj = GameManager.instance.playerObj;
        lightTrans.position = playerObj.transform.position;
        RaycastHit hit;
        Physics.Raycast(rayStartTrans.position, playerObj.transform.GetChild(0).position - rayStartTrans.position, out hit);
        if (hit.collider == null) return;
        if (hit.collider.gameObject.layer == 16)
        {
            Destroy(hit.collider.gameObject.GetComponent<Door>());
        }
    }

    void DeadSceneFinished()
    {
        screamSource.Play();
        tearSource.Play();
        screamSource.GetComponent<ScreamSource>().enabled = true;
        playerObj.SetActive(false);
        Instantiate(deadScene, playerObj.transform.position, playerObj.transform.rotation);
        Destroy(gameObject);
    }
}
