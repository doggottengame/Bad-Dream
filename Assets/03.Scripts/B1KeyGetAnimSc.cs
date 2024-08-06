using UnityEngine;

public class B1KeyGetAnimSc : MonoBehaviour
{
    [SerializeField]
    Transform monster, player;
    [SerializeField]
    Light flash;

    private void Awake()
    {
       flash.enabled = GameManager.instance.playerSc.IsFlashOn();
    }

    void Surprise()
    {
        //GameManager.instance.Grabed();
    }

    void KeyGetAnimFinished()
    {
        GameManager.instance.KeyGetAnimFinished(player);
        monster.GetComponent<Animator>().enabled = true;
        monster.transform.SetParent(null);
        GameManager.instance.monsterObj = monster.gameObject;
        monster.GetComponent<Monster>().enabled = true;
        Destroy(gameObject);
    }
}
