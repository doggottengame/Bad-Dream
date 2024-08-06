using TMPro;
using UnityEngine;

public class RandPassword : MonoBehaviour
{
    string password;
    [SerializeField]
    TMP_Text passwordTxt;

    // Start is called before the first frame update
    void Start()
    {
        password = $"{Random.Range(0, 10)}{Random.Range(0, 10)}{Random.Range(0, 10)}{Random.Range(0, 10)}";
        passwordTxt.text = password;
    }
}
