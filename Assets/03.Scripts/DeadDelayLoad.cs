using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeadDelayLoad : MonoBehaviour
{
    private IEnumerator Start()
    {
        yield return new WaitForSeconds(2f);

        SceneManager.LoadScene("PlayScene");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
