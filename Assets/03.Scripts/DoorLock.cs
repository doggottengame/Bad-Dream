using System.Collections.Generic;
using UnityEngine;

public class DoorLock : MonoBehaviour
{
    [SerializeField]
    sbyte needKeyNum;

    public bool UnlockTry(ref List<byte> keyListV)
    {
        if (needKeyNum < 0) return false;
        foreach (byte i in keyListV)
        {
            if (i == needKeyNum)
            {
                Destroy(gameObject);
                return true;
            }
        }

        return false;
    }
}
