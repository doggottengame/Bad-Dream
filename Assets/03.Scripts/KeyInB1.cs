using UnityEngine;

public class KeyInB1 : KeyGet
{
    [SerializeField]
    GameObject[] type1, type2;

    [SerializeField]
    KeyInB1 otherKey;
    [SerializeField]
    bool keyType;
    public bool playerGet;

    public override void GetKey()
    {
        if (otherKey.playerGet)
        {
            GameManager.instance.isTrackingInB1 = true;
            Destroy(GameManager.instance.monsterObj);

            Instantiate(keyType ? type1[Random.Range(0, 2)] :
                type2[Random.Range(0, 2)]);
        }
        else
        {
            playerGet = true;
        }
        Destroy(gameObject);
    }
}
 