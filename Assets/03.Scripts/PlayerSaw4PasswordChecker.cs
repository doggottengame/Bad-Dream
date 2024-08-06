using System.Collections;
using UnityEngine;

public class PlayerSaw4PasswordChecker : MonoBehaviour
{
    private void Awake()
    {
        StartCoroutine(PlayerSeeCheck());
    }

    IEnumerator PlayerSeeCheck()
    {
        WaitForSeconds seconds = new WaitForSeconds(0.02f);

        while (true)
        {
            Vector3 screenPoint = Camera.main.WorldToViewportPoint(transform.position);
            if (screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1)
            {
                GameManager.instance.SawOtherNumbers();
                break;
            }

            yield return seconds;
        }
        Destroy(this);
    }
}
