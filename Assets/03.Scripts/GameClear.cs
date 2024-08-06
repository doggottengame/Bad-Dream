using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameClear : MonoBehaviour
{
    [SerializeField]
    GameObject clearObj;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 3)
        {
            Instantiate(clearObj, GameManager.instance.playerObj.transform.position, GameManager.instance.playerObj.transform.rotation, GameManager.instance.playerObj.transform);
            Destroy(GameManager.instance.monsterObj);
        }
    }
}
