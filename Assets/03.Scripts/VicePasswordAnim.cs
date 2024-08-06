using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class VicePasswordAnim : MonoBehaviour
{
    Transform monster;

    bool outOfRoom;
    bool timeToMonster;

    private void Awake()
    {
        monster = transform.GetChild(0);
        monster.SetParent(null);
        monster.position = new Vector3(6.7f, 0, -11.25f);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log($"{monster.name}");
        if (outOfRoom)
        {
            Vector3 screenPoint = Camera.main.WorldToViewportPoint(monster.position);
            if (timeToMonster || 
                (screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1))
            {
                monster.GetComponent<Monster>().enabled = true;
                monster.GetComponent<NavMeshAgent>().enabled = true;
                monster.GetComponent<Monster>().inSight.FoundPlayer();
                StopCoroutine(PlayerOut());
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 3 && !outOfRoom)
        {
            outOfRoom = true;
            StartCoroutine(PlayerOut());
        }
    }

    IEnumerator PlayerOut()
    {
        yield return new WaitForSeconds(1);

        timeToMonster = true;
    }
}
