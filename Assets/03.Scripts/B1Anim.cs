using UnityEngine;

public class B1Anim : MonoBehaviour
{
    [SerializeField]
    Transform monster;

    private void Awake()
    {
        GameManager.instance.Surprised();
    }

    void KeyGetAnimFinished()
    {
        GameManager.instance.ChasingLoop();
        monster.GetComponent<Animator>().enabled = true;
        monster.transform.SetParent(null);
        GameManager.instance.monsterObj = monster.gameObject;
        monster.GetComponent<Monster>().enabled = true;
        Destroy(gameObject);
    }
}
