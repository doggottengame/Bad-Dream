using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndSceneAnim1 : MonoBehaviour
{
    [SerializeField]
    GameObject lastCam;

    void DeadSceneFinished()
    {
        lastCam.SetActive(true);
        Destroy(gameObject);
    }
}
