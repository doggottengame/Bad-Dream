using NavKeypad;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Transform camTrans;
    [SerializeField]
    Light flash;
    GameObject flashArea;
    [SerializeField]
    Animator camAnim;
    [SerializeField]
    AudioSource crackingAnim;
    [SerializeField]
    AudioSource etcAudioSource;
    [SerializeField]
    AudioClip doorUnlockFailed, doorUnlockSuccess, buttonClicked,
        getSomething;
    List<byte> keyList = new List<byte>();
    Rigidbody rb;
    Vector3 vel;
    private float hRot;
    private float rotateSpeed = 100f, moveSpeed = 1f, camSpeed = 1f;
    float xRotateMove, xRotate;
    bool isMoving, isRotating;

    bool inCabinet, cabinetDoorClosed;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        //rb.maxLinearVelocity = 2.0f;
        flashArea = flash.transform.GetChild(0).gameObject;

        keyList.Add(0);

        StartCoroutine(MoveControl());
    }

    private void Update()
    {
        Click();
    }

    IEnumerator MoveControl()
    {
        WaitForSeconds seconds = new WaitForSeconds(0.01f);

        while (true)
        {
            if (!GameManager.instance.GamePaused) Move();
            
            yield return seconds;
        }
    }

    void Move()
    {
        vel = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;

        isMoving = (vel.sqrMagnitude != 0f);

        hRot = Input.GetAxis("Mouse X");
        isRotating = (hRot != 0f);

        xRotateMove = -Input.GetAxis("Mouse Y") * Time.deltaTime * 200;

        xRotate = xRotate + xRotateMove;
        xRotate = Mathf.Clamp(xRotate, -90, 90); // 위, 아래 고정

        camTrans.localEulerAngles = new Vector3(xRotate, 0, 0);
    }

    public bool IsFlashOn()
    {
        return flash.enabled;
    }

    void Click()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            flash.enabled = !flash.enabled;
            flashArea.SetActive(flash.enabled);
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            camAnim.speed = 2.5f * camSpeed;
            moveSpeed = 2.5f;
        }
        else
        {
            camAnim.speed = 1.5f * camSpeed;
            moveSpeed = 1.5f;
        }

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 2))
            {
                if (hit.collider != null)
                {
                    try
                    {
                        switch (hit.collider.gameObject.tag)
                        {
                            case "Door":
                                etcAudioSource.PlayOneShot(hit.collider.gameObject.GetComponent<Door>().DoorAction());
                                if (inCabinet) cabinetDoorClosed = hit.collider.gameObject.GetComponent<Animator>().GetBool("Open");
                                break;

                            case "ElevatorButton":
                                hit.collider.gameObject.GetComponent<ElevatorButton>().Clicked();
                                etcAudioSource.PlayOneShot(buttonClicked);
                                break;

                            case "DoorLock":
                                if (hit.collider.gameObject.GetComponent<DoorLock>().UnlockTry(ref keyList))
                                {
                                    etcAudioSource.PlayOneShot(doorUnlockSuccess);
                                }
                                else
                                {
                                    etcAudioSource.PlayOneShot(doorUnlockFailed);
                                }
                                break;

                            case "Key":
                                try
                                {
                                    byte keyNum = byte.Parse(hit.collider.gameObject.name);
                                    keyList.Add(keyNum);
                                    GameManager.instance.AddItemList($"Key {keyNum}");
                                    etcAudioSource.PlayOneShot(getSomething);
                                    KeyGet tmp;
                                    if (hit.collider.gameObject.TryGetComponent<KeyGet>(out tmp))
                                        tmp.GetKey();
                                    else
                                        Destroy(hit.collider.gameObject);
                                }
                                catch { }
                                break;

                            case "KeypadButton":
                                hit.collider.gameObject.GetComponent<KeypadButton>().PressButton();
                                break;

                            case "FlashLight":
                                flash.gameObject.SetActive(true);
                                flash.enabled = true;
                                GameManager.instance.AddItemList("Flash");
                                etcAudioSource.PlayOneShot(getSomething);
                                Destroy(hit.collider.gameObject);
                                break;

                            case "Note":
                                GameManager.instance.AddItemList("Note");
                                crackingAnim.Play();
                                Destroy(hit.collider.gameObject);
                                break;

                            case "Curtain":
                                hit.collider.gameObject.GetComponent<Animator>().SetTrigger("Set");
                                hit.collider.gameObject.transform.parent.GetComponent<AudioSource>().Play();
                                GameManager.instance.PlayerSawlastNumber();
                                break;

                            case "Exit":
                                hit.collider.gameObject.GetComponent<ExitDoor>().TryDoorOpen(ref keyList);
                                break;
                        }
                    }
                    catch { }
                }
            }
        }
    }

    private void FixedUpdate()
    {
        if (isMoving)
        {
            rb.MovePosition(rb.position + transform.TransformDirection(vel) * moveSpeed * Time.fixedDeltaTime);
        }
        camAnim.SetBool("Step", isMoving);

        if (isRotating)
        {
            float rotAngle = hRot * rotateSpeed * Time.deltaTime;
            rb.rotation = Quaternion.AngleAxis(rotAngle, Vector3.up) * rb.rotation; // 좌우 회전
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 7)
        {
            camSpeed = 2;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == 7)
        {
            camSpeed = 1;
        }
    }

    public void GetInCabinet()
    {
        inCabinet = true;
    }

    public void GetOutCabinet()
    {
        inCabinet = false;
        cabinetDoorClosed = false;
    }

    public bool GetPlayerDeadInCabinet()
    {
        if (inCabinet && !cabinetDoorClosed)
        {
            return true;
        }
        return false;
    }
}
