using System.Collections;
using UnityEngine;

public class Television : MonoBehaviour
{
    [SerializeField]
    Material material;

    Color[] colors;
    [SerializeField]
    LayerMask layerMask;
    Transform playerTrans;
    AudioSource audioSource;

    private void Awake()
    {
        colors = new Color[3]
        {
            Color.red, Color.green, Color.blue
        };
        playerTrans = GameManager.instance.playerObj.transform;
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(Chzzk());
    }

    IEnumerator Chzzk()
    {
        WaitForSeconds seconds = new WaitForSeconds(0.02f);

        int count = 0;

        while (true)
        {
            if (count >= 50)
            {
                material.SetColor("_EmissionColor", colors[Random.Range(0, 3)]);

                audioSource.volume = (playerTrans.position.y > 3
                    && playerTrans.position.y < 5) ? 1 : 0;

                count = 0;
            }

            yield return seconds;
            count++; 
        }
    }
}
