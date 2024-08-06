using UnityEngine;

public class DoorLockPassword : MonoBehaviour
{
    public void PasswordCorrect()
    {
        Destroy(gameObject);
    }
}
