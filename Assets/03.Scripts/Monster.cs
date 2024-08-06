using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Monster : MonoBehaviour
{
    [SerializeField]
    AudioSource walkSource, runSource;
    public MonsterPlayerInSight inSight;
    byte floor = 1;
    byte pos = 1;

    LayerMask layerMask;

    Transform destination;
    Transform playerTrans, playerRayTrans;
    [SerializeField]
    Transform sightTrans;

    public bool isTrackingForever;
    public bool trackingPlayer;
    bool ready;

    public Animator animator;
    NavMeshAgent navAgent;

    private void Awake()
    {
        playerTrans = GameManager.instance.playerObj.transform;
        playerRayTrans = playerTrans.GetChild(0);

        navAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        if (sightTrans != null) inSight = sightTrans.GetComponent<MonsterPlayerInSight>();

        destination = GameManager.instance.GetMonsterDestination(floor, pos);

        layerMask = GameManager.instance.layerMask;
        if (GameManager.instance.monsterObj != null) Destroy(GameManager.instance.monsterObj);
        GameManager.instance.monsterObj = gameObject;
    }

    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return new WaitForSeconds(0.5f);

        ready = true;
        StartCoroutine(MonsterControl());
    }

    private void OnEnable()
    {
        GetComponent<Animator>().enabled = true;
        GetComponent<NavMeshAgent>().enabled = true;
        if (isTrackingForever = GameManager.instance.isTrackingInB1 || GameManager.instance.isTrackingInRoof)
        {
            GameManager.instance.isTrackingInB1 = false;
            GameManager.instance.isTrackingInRoof = false;

            animator.SetBool("Run", true);
            runSource.Play();

        }
        walkSource.Play();
    }

    IEnumerator MonsterControl()
    {
        WaitForSeconds seconds = new WaitForSeconds(0.1f);
        
        while(true)
        {
            if (ready)
            {
                if (!isTrackingForever)
                {
                    if (!trackingPlayer)
                    {
                        if ((destination.position - transform.position).sqrMagnitude <= 0.01f)
                        {
                            byte tmp = (byte)Random.Range(1, 4);
                            if (tmp == floor)
                            {
                                byte posTmp = (byte)Random.Range(0, 3);
                                for (; pos == posTmp;) posTmp = (byte)Random.Range(0, 3);

                                pos = posTmp;
                                destination = GameManager.instance.GetMonsterDestination(floor, pos);
                            }
                            else
                            {
                                floor = tmp;
                                destination = GameManager.instance.GetMonsterDestination(floor, pos);
                            }
                        }
                        else
                        {
                            navAgent.SetDestination(destination.position);
                        }
                    }
                    else
                    {
                        RaycastHit hit;
                        Physics.Raycast(sightTrans.position, playerRayTrans.position - sightTrans.position, out hit, 10, layerMask);

                        if (hit.collider == null || hit.collider.gameObject.layer != 3)
                        {
                            inSight.MissPlayer();
                        }
                        navAgent.SetDestination(playerTrans.position);
                    }

                }
                else
                {
                    navAgent.SetDestination(playerTrans.position);
                }
            }

            yield return seconds;
        }
    }

    public bool DidMonsterSeePlayer()
    {
        RaycastHit hit;
        Physics.Raycast(sightTrans.position, playerRayTrans.position - sightTrans.position, out hit, 10, layerMask);
        if (hit.collider == null) return false;
        if (hit.collider.gameObject.layer == 3)
        {
            destination = playerTrans;
            return true;
        }
        else
        {
            return false;
        }
    }

    public void MissPlayer()
    {
        byte tmp = (byte)Random.Range(1, 4);
        if (tmp == floor)
        {
            byte posTmp = (byte)Random.Range(0, 3);
            for (; pos == posTmp;) posTmp = (byte)Random.Range(0, 3);

            pos = posTmp;
            destination = GameManager.instance.GetMonsterDestination(floor, pos);
        }
        else
        {
            floor = tmp;
            destination = GameManager.instance.GetMonsterDestination(floor, pos);
        }
    }

    public void Walk()
    {
        if (runSource.isPlaying)
        {
            StopCoroutine(TrackingCount());
            runSource.Stop();
        }
    }

    public void Run()
    {
        if (!runSource.isPlaying && !isTrackingForever)
        {
            StartCoroutine(TrackingCount());
            GameManager.instance.Surprised();
            runSource.Play();
        }
    }

    IEnumerator TrackingCount()
    {
        yield return new WaitForSeconds(30);

        if (!isTrackingForever) trackingPlayer = false;
    }

    public void GetPlayer()
    {
        if (!ready) return;
        GameManager.instance.MonsterGetPlayer();
    }
}
