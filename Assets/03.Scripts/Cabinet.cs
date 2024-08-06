using UnityEngine;

public class Cabinet : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 3)
        {
            GameManager.instance.playerSc.GetInCabinet();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 3)
        {
            GameManager.instance.playerSc.GetOutCabinet();
        }
    }
}
