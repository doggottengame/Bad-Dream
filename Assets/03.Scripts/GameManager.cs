using NavKeypad;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool GamePaused;

    public bool firstGame = true;

    public GameObject playerObj, monsterObj;
    public Player playerSc;
    Rigidbody playerRb;
    [SerializeField]
    Animator camAnim;
    Animator sceneAnim;

    [SerializeField]
    TMP_Text conversationTxt;
    string[] startConversations;
    string[] shakeConversations;
    string[] startAgainConversations;

    [SerializeField]
    GameObject[] passwordObjs;

    [SerializeField]
    TMP_Text[] passwordTxt, passwordOneSideTxt;

    public short[] password = new short[4];

    [SerializeField]
    Keypad[] keyboxPads;

    public bool isTrackingInRoof, isTrackingInB1;

    bool onTalking, waitForEnter;
    bool enterInputed;

    public List<string> itemList = new List<string>();

    [SerializeField]
    GameObject window, pointer;

    [SerializeField]
    GameObject[] deadSceneCabinet, deadSceneElevator;
    [SerializeField]
    GameObject deadSceneNormal, lastNumScene;

    [SerializeField]
    Transform f1_1, f1_2, f1_3, f2_1, f2_2, f2_3,
        f3_1, f3_2, f3_3;
    public Transform[][] destinations = new Transform[3][];

    public LayerMask layerMask;

    [SerializeField]
    AudioSource bgmAudioSource, chasingAudioSource, chasingLoopAudioSource, surpriseAudioSource,
        laughAudioSource;

    [SerializeField]
    AudioClip[] bgm;

    private void Awake()
    {
        instance = this;

        foreach (GameObject objs in passwordObjs)
        {
            objs.SetActive(false);
        }

        destinations[0] = new Transform[3] { f1_1, f1_2, f1_3 };
        destinations[1] = new Transform[3] { f2_1, f2_2, f2_3 };
        destinations[2] = new Transform[3] { f3_1, f3_2, f3_3 };
    }

    // Start is called before the first frame update
    void Start()
    {
        playerRb = playerObj.GetComponent<Rigidbody>();
        sceneAnim = GetComponent<Animator>();

        startConversations = new string[]
        {
            "What...",
            "Where am I?"
        };

        shakeConversations = new string[]
        {
            "Classroom...?",
            "...",
            "Why is it so dark?",
            "I need some light."
        };

        startAgainConversations = new string[]
        {
            "What...",
            "Dream...?"
        };

        if (firstGame) //처음 시작했을 때
        {
            password[0] = (short)UnityEngine.Random.Range(1000, 10000); //칠판
            password[1] = (short)UnityEngine.Random.Range(1000, 10000); //공책
            password[2] = (short)UnityEngine.Random.Range(1000, 10000); //4군데로 한 자리 씩 퍼져 있음
            password[3] = (short)UnityEngine.Random.Range(1000, 10000); //3-2 교실 교탁에 있음

            passwordTxt[0].text = $"{password[0]}";
            //passwordTxt[1].text = $"{password[1]}";
            passwordTxt[2].text = $"{password[3]}";

            passwordOneSideTxt[0].text = $"{password[2] / 1000}";
            passwordOneSideTxt[1].text = $"{password[2] % 1000 / 100}";
            passwordOneSideTxt[2].text = $"{password[2] % 1000 % 100 / 10}";
            passwordOneSideTxt[3].text = $"{password[2] % 1000 % 100 % 10}";

            keyboxPads[0].SetPassword(password[0]);
            keyboxPads[1].SetPassword(120852);
            keyboxPads[2].SetPassword(password[1]);
            keyboxPads[3].SetPassword(password[2]);
            keyboxPads[4].SetPassword(password[3]);

            sceneAnim.SetInteger("SceneAnim", 0);
        }
        else //시작했던 게임을 다시 했을 때
        {
            password[0] = (short)UnityEngine.Random.Range(1000, 10000); //칠판
            password[1] = (short)UnityEngine.Random.Range(1000, 10000); //공책
            password[2] = (short)UnityEngine.Random.Range(1000, 10000); //4군데로 한 자리 씩 퍼져 있음
            password[3] = (short)UnityEngine.Random.Range(1000, 10000); //3-2 교실 교탁에 있음

            passwordTxt[0].text = $"{password[0]}";
            //passwordTxt[1].text = $"{password[1]}";
            passwordTxt[2].text = $"{password[3]}";

            passwordOneSideTxt[0].text = $"{password[2] / 1000}";
            passwordOneSideTxt[1].text = $"{password[2] % 1000 / 100}";
            passwordOneSideTxt[2].text = $"{password[2] % 1000 % 100 / 10}";
            passwordOneSideTxt[3].text = $"{password[2] % 1000 % 100 % 10}";

            keyboxPads[0].SetPassword(password[0]);
            keyboxPads[1].SetPassword(120852);
            keyboxPads[2].SetPassword(password[1]);
            keyboxPads[3].SetPassword(password[2]);
            keyboxPads[4].SetPassword(password[3]);

            sceneAnim.SetInteger("SceneAnim", 3);
        }

        firstGame = false;

        bgmAudioSource.PlayOneShot(bgm[UnityEngine.Random.Range(0, bgm.Length)]);
    }

    private void Update()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        if (!bgmAudioSource.isPlaying)
        {
            bgmAudioSource.PlayOneShot(bgm[UnityEngine.Random.Range(0, bgm.Length)]);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!window.activeSelf)
            {
                GamePaused = true;
                playerSc.enabled = false;
                window.SetActive(true);
                pointer.SetActive(false);
            }
            else
            {
                GamePaused = false;
                playerSc.enabled = true;
                window.SetActive(false);
                pointer.SetActive(true);
            }
        }

        if (GamePaused || !onTalking || !waitForEnter) return;

        if (Input.GetKeyDown(KeyCode.Return))
        {
            waitForEnter = false;
            enterInputed = true;
        }
    }

    public void Laugh()
    {
        laughAudioSource.Play();
    }

    public void MonsterGetPlayer()
    {
        Destroy(monsterObj);

        if (playerSc.GetPlayerDeadInCabinet())
        {
            //GameManager.instance.DeadSceneInLocker((byte)Random.Range(0, 3));
            //0: 락커 문을 뜯고 잡아감 (문을 열 수 없음)
            //1: 앉은 채로 락커 문을 열때까지 기다림
            //2: 락커 문이 닫힌 상태에서 문을 뚫고 갑자기 튀어나옴 (문을 열 수 없음)

            byte deadSceneType = 0;//(byte)UnityEngine.Random.Range(0, 3);

            if (playerObj.transform.position.y > 2)
            {
                Instantiate(deadSceneCabinet[deadSceneType], playerObj.transform.position, Quaternion.identity);
            }
            else
            {
                Instantiate(deadSceneCabinet[deadSceneType], playerObj.transform.position, Quaternion.Euler(0, playerObj.transform.position.z > 0 ? -90 : 90, 0));
            }
        }
        else
        {
            playerObj.SetActive(false);
            Instantiate(deadSceneNormal, playerObj.transform.position, playerObj.transform.rotation);
        }
    }

    public void PlayerSawlastNumber()
    {
        Destroy(monsterObj);

        Instantiate(lastNumScene/*, playerObj.transform.position, playerObj.transform.rotation, playerObj.transform.GetChild(0).GetChild(0)*/);
    }

    public Transform GetMonsterDestination(int floorV, int posV)
    {
        return destinations[floorV - 1][posV];
    }

    public void AddItemList(string strV)
    {
        itemList.Add(strV);

        if (strV == "Note")
        {
            passwordObjs[0].SetActive(true);
            passwordObjs[1].SetActive(true);
            passwordObjs[2].SetActive(true);
            passwordObjs[3].SetActive(false);
        }
    }

    byte childMass = 3;

    public void SawOtherNumbers()
    {
        if (--childMass == 0)
        {
            passwordObjs[3].SetActive(true);
        }
    }

    public void DeadInElevator(Transform elevatorTrans, Transform doorTrans)
    {
        byte randNum = (byte)UnityEngine.Random.Range(0, deadSceneElevator.Length);
        Instantiate(deadSceneElevator[randNum], elevatorTrans);

        if (randNum == 0)
        {
            Destroy(elevatorTrans.GetChild(7).gameObject);
            Destroy(elevatorTrans.GetChild(8).gameObject);
            Destroy(doorTrans.GetChild(2).gameObject);
            Destroy(doorTrans.GetChild(3).gameObject);
        }
    }

    public void Surprised()
    {
        surpriseAudioSource.Play();
    }

    public void Chasing()
    {
        chasingAudioSource.Play();
    }

    public void ChasingLoop()
    {
        chasingLoopAudioSource.Play();
    }

    void StartAnimFinished()
    {
        StartCoroutine(Talking(startConversations, () =>
        {
            sceneAnim.SetInteger("SceneAnim", 1);
        }));
    }

    void ShakingHeadFinished()
    {
        StartCoroutine(Talking(shakeConversations, () =>
        {
            sceneAnim.SetInteger("SceneAnim", 2);
        }));
    }

    void StartAgainAnimFinished()
    {
        StartCoroutine(Talking(startAgainConversations, () =>
        {
            sceneAnim.SetInteger("SceneAnim", 2);
        }));
    }

    void GetUpFinished()
    {
        StartCoroutine(Talking(null, () =>
        {
            playerObj.transform.SetParent(null);
            playerRb.constraints = RigidbodyConstraints.FreezeRotation;
            playerSc.enabled = true;
            camAnim.enabled = true;
            sceneAnim.enabled = false;
        }));
    }

    public void KeyGetAnimFinished(Transform playerV)
    {
        playerObj.transform.position = playerV.position;
        playerObj.transform.rotation = playerV.rotation;
        playerObj.transform.SetParent(null);
        playerRb.constraints = RigidbodyConstraints.FreezeRotation;
        playerSc.enabled = true;
        camAnim.enabled = true;
        sceneAnim.enabled = false;
        playerObj.SetActive(true);
    }

    IEnumerator Talking(string[] conversationV, Action actV)
    {
        if (conversationV != null)
        {
            WaitForSeconds seconds = new WaitForSeconds(0.1f);

            int count = 0;
            onTalking = true;

            while (!GamePaused && count < conversationV.Length)
            {
                int sentenceV = 1;

                while (!GamePaused && sentenceV <= conversationV[count].Length)
                {
                    conversationTxt.text = conversationV[count].Substring(0, sentenceV);
                    sentenceV++;

                    yield return seconds;
                }

                waitForEnter = true;

                conversationTxt.text += "\n[Enter]";

                while (!enterInputed)
                {
                    yield return seconds;
                }

                enterInputed = false;

                count++;
            }

            waitForEnter = true;

            while (!enterInputed)
            {
                yield return seconds;
            }

            enterInputed = false;

            conversationTxt.text = null;
        }

        if (actV != null)
        {
            actV();
        }
    }
}
