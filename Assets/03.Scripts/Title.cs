using UnityEngine;

public class Title : MonoBehaviour
{
    [SerializeField]
    GameObject monster;
    bool loaded;

    private void Start()
    {
        int setWidth = 960;
        int setHeight = 540;

        Screen.SetResolution(setWidth, setHeight, true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown && !Input.GetKeyDown(KeyCode.Escape) && !loaded)
        {
            loaded = true;
            monster.SetActive(true);
        }
    }
}
