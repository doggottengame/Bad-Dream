using System.Collections;
using UnityEngine;

public class DeadSceneCabinet_1 : MonoBehaviour
{
    [SerializeField]
    Transform rayStartTrans, witchTrans;
    Transform playerRayTrans;
    [SerializeField]
    GameObject deadScene;
    GameObject playerObj;
    [SerializeField]
    AudioSource screamSource;
    bool playerOpened;

    // Start is called before the first frame update
    void Awake()
    {
        playerObj = GameManager.instance.playerObj;
        playerRayTrans = playerObj.transform.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (!playerOpened)
        {
            RaycastHit hit;
            Physics.Raycast(rayStartTrans.position, playerRayTrans.position - rayStartTrans.position, out hit);
            if (hit.collider == null) return;
            if (hit.collider.gameObject.layer == 3)
            {
                playerOpened = true;

                StartCoroutine(Delay());
            }
        }
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(0.2f);

        screamSource.Play();
        screamSource.GetComponent<ScreamSource>().enabled = true;
        playerObj.SetActive(false);
        Instantiate(deadScene, playerObj.transform.position, playerObj.transform.rotation);
        Destroy(gameObject);
    }
}
