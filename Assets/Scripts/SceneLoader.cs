using UnityEngine.Video;
using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;

public class SceneLoader : MonoBehaviour
{
    private bool wait = false;
    public VideoPlayer VideoPlayer; // Drag & Drop the GameObject holding the VideoPlayer component
   
    void Start()
    {
        Cursor.visible = false;
        StartCoroutine(MinWait());
        VideoPlayer.loopPointReached += LoadScene;
    }
    void LoadScene(VideoPlayer vp)
    {
        SceneManager.LoadScene(1);
    }

    private void Update()
    {
        
        if (Input.anyKeyDown && wait)
        {
            SceneManager.LoadScene(1);
        }
    }

    private IEnumerator MinWait()
    {
        wait = false;
        yield return new WaitForSeconds(2);
        wait = true;
    }
}
