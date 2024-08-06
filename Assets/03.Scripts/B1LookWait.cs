using UnityEngine;

public class B1LookWait : MonoBehaviour
{
    [SerializeField]
    Transform monster;
    Transform player;

    private void Start()
    {
        player = GameManager.instance.playerObj.transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 screenPoint = Camera.main.WorldToViewportPoint(monster.position);
        if (screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1
            || (player.position - monster.position).sqrMagnitude > 9)
        {
            GameManager.instance.Surprised();
            GameManager.instance.ChasingLoop();
            GameManager.instance.isTrackingInB1 = true;
            monster.GetComponent<Animator>().enabled = true;
            monster.transform.SetParent(null);
            GameManager.instance.monsterObj = monster.gameObject;
            monster.GetComponent<Monster>().enabled = true;
            Destroy(gameObject);
        }
    }
}