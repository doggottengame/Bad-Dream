using UnityEngine;

public class MonsterPlayerInGrap : MonoBehaviour
{
    Monster monster;
    AudioSource audioSource;

    private void Awake()
    {
        monster = transform.parent.GetComponent<Monster>();
        audioSource = transform.parent.GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 3) monster.GetPlayer();
        else if (other.gameObject.layer == 12)
        {
            if (other.gameObject.CompareTag("Door") && other.gameObject.name == "door01_2")
            {
                audioSource.PlayOneShot(other.gameObject.GetComponent<Door>().DoorAction());
            }
        }
    }
}
