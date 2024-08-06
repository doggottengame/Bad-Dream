using UnityEngine;

public class DeadSceneElevator_2 : MonoBehaviour
{
    [SerializeField]
    GameObject deadScene;
    [SerializeField]
    Transform monster;
    Transform playerTrans, monsterPos;
    [SerializeField]
    AudioSource brokeSource;

    bool waitFinished;

    private void Awake()
    {
        playerTrans = GameManager.instance.playerObj.transform;
        monsterPos = monster.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (!waitFinished)
        {
            Vector3 tmp = playerTrans.position;
            monster.position = tmp;
        }
        else
        {
            Vector3 tmp = monsterPos.position;
            tmp.y -= 0.1f;
            monsterPos.position = tmp;
            if (monsterPos.localPosition.y < -3)
            {
                GameObject playerObj = GameManager.instance.playerObj;
                playerObj.SetActive(false);
                Instantiate(deadScene, playerObj.transform.position, playerObj.transform.rotation);
                Destroy(gameObject);
            }
        }
    }

    void LookPlayer()
    {
        waitFinished = true;
        GameManager.instance.playerObj.SetActive(false);
        //playerTrans.GetChild(0).GetChild(0).localEulerAngles = new Vector3(-89, 0, 0);
    }

    void WaitFinished()
    {
        brokeSource.Play();
        brokeSource.GetComponent<ScreamSource>().enabled = true;
    }
}
