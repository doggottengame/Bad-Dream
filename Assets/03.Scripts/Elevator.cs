using System.Collections;
using TMPro;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    Rigidbody rb;
    AudioSource audioSource;
    [SerializeField]
    AudioClip arriveAlarmClip, openClip, closeClip;
    [SerializeField]
    Transform stoper;
    [SerializeField]
    Animator[] floorDoors;
    [SerializeField]
    TMP_Text[] floorShowInFloors; //Ãþ¿¡ ÀÖ´Â Ãþ Ç¥½Ã
    [SerializeField]
    TMP_Text floorShowTxt; //¿¤¸®º£ÀÌÅÍ¿¡ ÀÖ´Â Ãþ Ç¥½Ã
    Animator animator;
    byte nowFloor, purposeFloor;
    bool canMove = true;
    bool doorMoving;
    bool elevatorMove;
    bool up;
    bool doorOpened;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.maxLinearVelocity = 1;
        rb.constraints = RigidbodyConstraints.FreezeAll;
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }

    public void FloorClicked(byte floorV)
    {
        if (doorMoving || !canMove) return;

        if (!doorOpened && !elevatorMove)
        {
            if (nowFloor > floorV)
            {
                purposeFloor = floorV;
                stoper.localPosition = new Vector3(28, (purposeFloor - 2) * 4 + 0.4f, -7.5f);
                up = false;
                elevatorMove = true;
                rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
            }
            else if (nowFloor < floorV)
            {
                purposeFloor = floorV;
                stoper.localPosition = new Vector3(28, (purposeFloor - 1) * 4, -7.5f);
                up = true;
                elevatorMove = true;
                rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
            }
            else
            {
                doorOpened = true;

                StartCoroutine(DoorOpenDelay());
            }
        }
    }
    
    public void DoorOpenClicked()
    {
        if (doorMoving || !canMove) return;

        if (!doorOpened && !elevatorMove)
        {
            doorOpened = true;
            StartCoroutine(DoorOpenDelay());
        }
    }

    public void DoorCloseClicked()
    {
        if (doorMoving || !canMove) return;

        if (!elevatorMove)
        {
            doorMoving = true;
            animator.SetTrigger("Close");
        }
    }

    void DoorOpened()
    {
        doorMoving = false;
    }

    void CloseDoor()
    {
        audioSource.PlayOneShot(closeClip);

        floorDoors[nowFloor].SetTrigger("Close");
    }

    void DoorClosed()
    {
        doorOpened = false;
        doorMoving = false;

        if (GameManager.instance.playerObj.transform.position.x >= 24.1f &&
            GameManager.instance.monsterObj.GetComponent<Monster>().isTrackingForever)
        {
            if (Mathf.Abs(GameManager.instance.monsterObj.transform.position.y - GameManager.instance.playerObj.transform.position.y) < 0.5f &&
                GameManager.instance.monsterObj.transform.position.x >= 20)
            {
                canMove = false;
                GameManager.instance.DeadInElevator(transform, floorDoors[nowFloor].transform);
            }
        }
    }

    private void FixedUpdate()
    {
        if (!elevatorMove || !canMove) return;
        if (up)
        {
            rb.AddRelativeForce(Vector3.up * 500);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("ElevatorStoper"))
        {
            if (!doorOpened)
            {
                doorOpened = true;
                rb.constraints = RigidbodyConstraints.FreezeAll;
                elevatorMove = false;
                up = false;

                StartCoroutine(DoorOpenDelay());
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("FloorChecker"))
        {
            nowFloor = byte.Parse(other.name);
            if (nowFloor == 0)
            {
                floorShowTxt.text = "B1";
            }
            else
            {
                floorShowTxt.text = $"{nowFloor}";
            }

            foreach (TMP_Text text in floorShowInFloors)
            {
                text.text = floorShowTxt.text;
            }
        }
    }

    IEnumerator DoorOpenDelay()
    {
        audioSource.PlayOneShot(arriveAlarmClip);
        doorMoving = true;

        WaitForSeconds seconds = new WaitForSeconds(0.1f);

        float count = 0;

        while(!GameManager.instance.GamePaused && count < 0.5f)
        {
            yield return seconds;

            count += 0.1f;
        }

        audioSource.PlayOneShot(openClip);

        animator.SetTrigger("Open");
        floorDoors[nowFloor].SetTrigger("Open");
    }
}
