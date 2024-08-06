using UnityEngine;

public class B1KeyGet : MonoBehaviour
{
    [SerializeField]
    Transform player;
    [SerializeField]
    Light flash;

    private void Awake()
    {
        flash.enabled = GameManager.instance.playerSc.IsFlashOn();
    }
    void KeyGetAnimFinished()
    {
        GameManager.instance.KeyGetAnimFinished(player);
        Destroy(gameObject);
    }
}
