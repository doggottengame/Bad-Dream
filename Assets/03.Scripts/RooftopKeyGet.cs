using UnityEngine;
using UnityEngine.AI;

public class RooftopKeyGet : MonoBehaviour
{
    [SerializeField]
    Monster monster;
    [SerializeField]
    NavMeshAgent agent;

    void FinishAnim()
    {
        GameManager.instance.monsterObj = monster.gameObject;
        monster.transform.SetParent(null);
        monster.enabled = true;
        agent.enabled = true;
        Destroy(gameObject);
    }
}
