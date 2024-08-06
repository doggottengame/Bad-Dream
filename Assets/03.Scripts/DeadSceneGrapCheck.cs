using UnityEngine;

public class DeadSceneGrapCheck : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 3)
        {
            other.gameObject.transform.parent.SetParent(transform);
            other.gameObject.transform.parent.GetComponent<Player>().enabled = false;
            other.gameObject.transform.parent.GetComponent<Rigidbody>().useGravity = false;
        }
    }
}
