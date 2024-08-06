using UnityEngine;

public class RooftopKeyGetSetter : KeyGet
{
    [SerializeField]
    GameObject rooftopKeyGetPrefab;

    public override void GetKey()
    {
        GameManager.instance.isTrackingInB1 = false;
        GameManager.instance.isTrackingInRoof = true;
        Destroy(GameManager.instance.monsterObj);
        Instantiate(rooftopKeyGetPrefab);
        GameManager.instance.Chasing();
        Destroy(gameObject);
    }
}
