using System.Collections;
using UnityEngine;

public class MonsterPlayerInSight : MonoBehaviour
{
    Monster monster;
    Player player;
    public bool chasing;

    private void Awake()
    {
        monster = transform.parent.GetComponent<Monster>();
        player = GameManager.instance.playerSc;
    }

    public void FoundPlayer()
    {
        if (monster.DidMonsterSeePlayer())
        {
            chasing = true;
            monster.animator.SetBool("Run", true);
            monster.trackingPlayer = true;
            monster.Run();
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (!monster.enabled) return;
        if (other.gameObject.layer == 3)
        {
            if (monster.DidMonsterSeePlayer())
            {
                chasing = true;
                monster.animator.SetBool("Run", true);
                monster.trackingPlayer = true;
                monster.Run();
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (!monster.enabled) return;
        if (other.gameObject.layer == 13)
        {
            if (monster.DidMonsterSeePlayer() && !chasing)
            {
                chasing = true;
                monster.animator.SetBool("Run", true);
                monster.trackingPlayer = true;
                monster.Run();
            }
        }
    }

    public void MissPlayer()
    {
        if (!chasing) return;
        if (player.IsFlashOn()) return;
        chasing = false;

        StartCoroutine(ChasingMissDelay());
    }

    public void OnTriggerExit(Collider other)
    {
        MissPlayer();
    }

    IEnumerator ChasingMissDelay()
    {
        yield return new WaitForSeconds(3);

        monster.animator.SetBool("Run", false);
        monster.trackingPlayer = false;
        monster.Walk();
        monster.MissPlayer();
    }
}
