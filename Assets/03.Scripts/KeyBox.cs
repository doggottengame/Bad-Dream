using System;
using UnityEngine;

public class KeyBox : MonoBehaviour
{
    public Action keyboxUnlockAction;

    public void PasswordCorrect()
    {
        if (keyboxUnlockAction != null) keyboxUnlockAction();
        Destroy(gameObject);
    }
}
