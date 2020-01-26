using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ControlLoader : MonoBehaviour
{
    private bool wait = false;
    public int scene;
    public GameObject ContinueHide;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(MinWait());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown && wait)
        {
            SceneManager.LoadScene(scene);
        }
    }

    private IEnumerator MinWait()
    {
        wait = false;
        yield return new WaitForSeconds(1);
        wait = true;
        ContinueHide.SetActive(false);
    }
}
