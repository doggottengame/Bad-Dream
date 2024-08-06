using UnityEngine;

public class ElevatorButton : MonoBehaviour
{
    [SerializeField]
    byte buttonNum; //0: b1, 1: 1, 2: 2, 3: 3, 4: 4,
                   //5: open, 6: close, 7: elevator call
    [SerializeField]
    AudioClip clickClip;
    [SerializeField]
    Elevator elevator;

    public AudioClip Clicked()
    {
        switch (buttonNum)
        {
            case 0:
            case 1:
            case 2:
            case 3:
            case 4:
                elevator.FloorClicked(buttonNum);
                return clickClip;
            case 5:
                elevator.DoorOpenClicked();
                return clickClip;
            case 6:
                elevator.DoorCloseClicked();
                return clickClip;
            default:
                return null;
        }
    }
}
