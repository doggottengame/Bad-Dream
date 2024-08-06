using System;
using System.Collections;
using UnityEngine;

public class Clock : MonoBehaviour
{
    [SerializeField]
    Transform stickHour, stickMinute, stickSecond;

    private void Start()
    {
        stickHour.localEulerAngles = new Vector3(0, 0, -60);

        StartCoroutine(ClockControl());
    }

    IEnumerator ClockControl()
    {
        WaitForSeconds seconds = new WaitForSeconds(1);

        while(true)
        {
            byte /*h,*/ m, s;
            s = byte.Parse(DateTime.Now.ToString("ss"));
            m = byte.Parse(DateTime.Now.ToString("mm"));
            //h = byte.Parse(DateTime.Now.ToString("hh"));

            stickSecond.localEulerAngles = new Vector3(0, 0, s * 6);
            stickMinute.localEulerAngles = new Vector3(0, 0, m * 6);
            //stickHour.localEulerAngles = new Vector3(0, 0, h * 30);

            yield return seconds;
        }
    }
}
