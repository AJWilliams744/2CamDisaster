using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        //TODO - DEATH!!
        SceneManager.LoadScene(2);
    }
}
