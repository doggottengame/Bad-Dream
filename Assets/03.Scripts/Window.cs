using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Window : MonoBehaviour
{
    List<string> itemList;

    byte windowNum = 0;

    [SerializeField]
    GameObject winPrefab_Key, winPrefab_Note, winPrefab_FlashLight;
    GameObject winObj;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (windowNum > 0)
            {
                SetWindow(--windowNum);
            }
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (windowNum < itemList.Count - 1)
            {
                SetWindow(++windowNum);
            }
        }
    }

    private void OnEnable()
    {
        itemList = GameManager.instance.itemList;

        SetWindow(windowNum = 0);
    }

    void SetWindow(int listNum)
    {
        if (itemList.Count == 0) return;
        if (winObj != null) Destroy(winObj);
        switch (itemList[listNum])
        {
            case "Flash":
                winObj = Instantiate(winPrefab_FlashLight, transform);
                break;

            case "Key 1":
                winObj = Instantiate(winPrefab_Key, transform);
                winObj.transform.GetChild(2).GetComponent<TMP_Text>().text =
                    "Office 1";
                break;

            case "Key 2":
                winObj = Instantiate(winPrefab_Key, transform);
                winObj.transform.GetChild(2).GetComponent<TMP_Text>().text =
                    "Office 2";
                break;

            case "Key 3":
                winObj = Instantiate(winPrefab_Key, transform);
                winObj.transform.GetChild(2).GetComponent<TMP_Text>().text =
                    "1-3";
                break;

            case "Key 4":
                winObj = Instantiate(winPrefab_Key, transform);
                winObj.transform.GetChild(2).GetComponent<TMP_Text>().text =
                    "Circle 2-1";
                break;

            case "Key 5":
                winObj = Instantiate(winPrefab_Key, transform);
                winObj.transform.GetChild(2).GetComponent<TMP_Text>().text =
                    "Cunsulting 1-1";
                break;

            case "Key 6":
                winObj = Instantiate(winPrefab_Key, transform);
                winObj.transform.GetChild(2).GetComponent<TMP_Text>().text =
                    "Vice Office";
                break;

            case "Key 7":
                winObj = Instantiate(winPrefab_Key, transform);
                winObj.transform.GetChild(2).GetComponent<TMP_Text>().text =
                    "Out"; //≈ª√‚ ø≠ºË 1
                break;

            case "Key 8":
                winObj = Instantiate(winPrefab_Key, transform);
                winObj.transform.GetChild(2).GetComponent<TMP_Text>().text =
                    "Elevator";
                break;

            case "Key 9":
                winObj = Instantiate(winPrefab_Key, transform);
                winObj.transform.GetChild(2).GetComponent<TMP_Text>().text =
                    "Escape"; //≈ª√‚ ø≠ºË 2
                break;

            case "Note":
                winObj = Instantiate(winPrefab_Note, transform);
                winObj.transform.GetChild(2).GetComponent<TMP_Text>().text =
                    $"{GameManager.instance.password[1]}";
                break;
        }
    }
}
