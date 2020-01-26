using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] DoorHandler doorHandle;
    private bool WonGame;

    private void OnTriggerEnter(Collider other)
    {
        //TODO - DEATH!!
        if(other.gameObject.tag == "Wall") SceneManager.LoadScene(5);        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (WonGame && (collision.gameObject.tag == "Fire" || collision.gameObject.tag == "Finish"))
        {
            SceneManager.LoadScene(6);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            HandleDoorOpen();
        }
        if (Input.GetKeyDown(KeyCode.Escape)) SceneManager.LoadScene(5);
    }

    private void HandleDoorOpen()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 5))
        {
            GameObject hitObj = hit.collider.gameObject;
            if (hitObj.tag != "Finish") return;

            if (doorHandle.AddNumber(hitObj.GetComponentInParent<DoorAnimator>().TriggerDoor()))
            {
               
                WonGame = true;
                
            }    
            
        }
            
    }

}
